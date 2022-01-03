using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon.Runtime.Internal;

RuntimePipelineCustomizerRegistry.Instance.Register(new AWSPipelineCustomization());

using var client = new AmazonDynamoDBClient(new AmazonDynamoDBConfig
{
    ServiceURL = "http://localhost:4566"
});

foreach (var table in (await client.ListTablesAsync()).TableNames)
    Console.WriteLine("Found: " + table);

internal sealed class AWSPipelineCustomization : IRuntimePipelineCustomizer
{
    public string UniqueName { get; } = nameof(AWSPipelineCustomization);

    public void Customize(Type type, RuntimePipeline pipeline)
    {
        if (!typeof(AmazonServiceClient).IsAssignableFrom(type))
            return;

        foreach (var handler in pipeline.Handlers)
            Console.WriteLine("Handler: " + handler.GetType().Name);

        pipeline.AddHandlerAfter<EndpointResolver>(new AWSPipelineHandler());
    }
}

internal sealed class AWSPipelineHandler : PipelineHandler
{
    public override Task<T> InvokeAsync<T>(IExecutionContext executionContext)
    {
        Console.WriteLine("Executing: " + executionContext.RequestContext.RequestName);
        return base.InvokeAsync<T>(executionContext);
    }

    public override void InvokeSync(IExecutionContext executionContext)
    {
        Console.WriteLine("Executing: " + executionContext.RequestContext.RequestName);
        base.InvokeSync(executionContext);
    }
}
