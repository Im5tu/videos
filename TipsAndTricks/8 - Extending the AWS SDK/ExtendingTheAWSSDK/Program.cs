using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon.Runtime.Internal;

RuntimePipelineCustomizerRegistry.Instance.Register(new AWSActivityPipelineCustomizer());

using var client = new AmazonDynamoDBClient(new AmazonDynamoDBConfig { ServiceURL = "http://localhost:4566" });

foreach (var table in (await client.ListTablesAsync()).TableNames)
    Console.WriteLine("Found: " + table);


internal sealed class AWSActivityPipelineCustomizer : IRuntimePipelineCustomizer
{
    public string UniqueName { get; } = nameof(AWSActivityPipelineCustomizer);

    public void Customize(Type serviceClientType, RuntimePipeline pipeline)
    {
        if (!typeof(AmazonServiceClient).IsAssignableFrom(serviceClientType))
            return;

        pipeline.AddHandlerAfter<EndpointResolver>(new AWSActivityPipelineHandler());
    }
}

internal sealed class AWSActivityPipelineHandler : PipelineHandler
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
