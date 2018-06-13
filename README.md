# Azure Data Lake Store Binding for Azure Functions

The following binding can be used on Azure Functions v1 and v2. 

## Instructions

To the output binding add the following attribute

```c#
[DataLakeStore(AccountFQDN = @"fqdn", ApplicationId = @"applicationid", ClientSecret = @"clientsecret", TenantID = @"tentantid")]out DataLakeStoreOutput dataLakeStoreOutput
```

To use the input binding simple add 'FileName' to bring in a specific file

```c#
[DataLakeStore(AccountFQDN = @"fqdn", ApplicationId = @"applicationid", ClientSecret = @"clientsecret", TenantID = @"tentantid", FileName = "/mydata/testfile.txt")]Stream myfile
```

## Binding Requirements 

1. [Azure Data Lake Store](https://azure.microsoft.com/en-us/services/data-lake-store/)
2. Setup [Service to Service Auth](https://docs.microsoft.com/en-us/azure/data-lake-store/data-lake-store-service-to-service-authenticate-using-active-directory) using Azure AD
3. [Azure Functions and Webjobs tools](https://marketplace.visualstudio.com/items?itemName=VisualStudioWebandAzureTools.AzureFunctionsandWebJobsTools) extension 
4. Add the application settings noted below. 

### local.settings.json expected content
```
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "AzureWebJobsDashboard": "UseDevelopmentStorage=true",
    "fqdn": "<FQDN for your Azure Lake Data Store>",
    "tentantid": "<Azure Active Directory Tentant for Authentication>",
    "clientsecret": "<Azure Active Directory Client Secret>",
    "applicationid": "<Azure Active Directory Application ID>",
    "blobconn": "<Azure Blobg Storage Connection String for testing Blob Trigger>"
  }
}
```

## License

This project is under the benevolent umbrella of the [.NET Foundation](http://www.dotnetfoundation.org/) and is licensed under [the MIT License](https://github.com/Azure/azure-webjobs-sdk/blob/master/LICENSE.txt)


## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.microsoft.com.

When you submit a pull request, a CLA-bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., label, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
