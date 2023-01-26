dotnet lambda deploy-function --profile tmhprodnew -fn F1ListGroups -frole F1Role
dotnet lambda invoke-function --profile tmhprodnew F1ListGroups -p '{"arguments":{"itemId":"62948"}}'