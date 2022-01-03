#!/bin/sh
#Make sure this file is saved with LF line endings (not CRLF)
#Open this file in VSCode and look in the bottom right corner
set -x

aws dynamodb create-table \
    --table-name test \
    --attribute-definitions AttributeName=Key,AttributeType=S AttributeName=Code,AttributeType=S \
    --key-schema AttributeName=Key,KeyType=HASH AttributeName=Code,KeyType=RANGE \
    --provisioned-throughput ReadCapacityUnits=10,WriteCapacityUnits=5 \
    --endpoint-url http://localstack:4566
