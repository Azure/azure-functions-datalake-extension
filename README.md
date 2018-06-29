# Azure Data Lake Store Binding for Azure Functions

The following binding can be used with Azure Functions v2 C# Class Library. 

## Instructions

Clone repo and add a reference to the *WebJobs.Extensions.DataLake* project. 

```c#
using Microsoft.Azure.WebJobs.Extensions.DataLake;
```
#### Output Binding
Add the following attributes that include the account FQDN, ApplicationId, Client Secret and Tenant Id.

```c#
[DataLakeStore(
  AccountFQDN = @"fqdn", 
  ApplicationId = @"applicationid", 
  ClientSecret = @"clientsecret", 
  TenantID = @"tentantid")]out DataLakeStoreOutput dataLakeStoreOutput
```
View a [sample function](samples/DataLakeExtensionSamples/OutputFromBlob.cs) using output binding.

#### Input Binding
Add *FileName* property to retrieve a specific file from your Datalake Store.

```c#
[DataLakeStore(
  AccountFQDN = @"fqdn", 
  ApplicationId = @"applicationid",
  ClientSecret = @"clientsecret",
  TenantID = @"tentantid",
  FileName = "/mydata/testfile.txt")]Stream myfile
```
View a [sample function](samples/DataLakeExtensionSamples/InputSample.cs) using input binding.

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
## End to End Testing

If you wish to run and or make modifications to the E2E testing you will need to create an appsettings.json  with all the required settings. Use the standard format for values instead of the functions formatting. 

### E2E appsettings.json expected content
```
{
  "AzureWebJobsStorage": "UseDevelopmentStorage=true",
  "AzureWebJobsDashboard": "UseDevelopmentStorage=true",
  "fqdn": "<FQDN>",
  "tentantid": "<Tentant ID>",
  "clientsecret": "<Client Secret>",
  "applicationid": "<Application ID>",
  "blobconn": "<Blob Storage Connection string for Trigger>"
}
```

## License

This project is under the benevolent umbrella of the [.NET Foundation](http://www.dotnetfoundation.org/) and is licensed under [the MIT License](https://github.com/Azure/azure-webjobs-sdk/blob/master/LICENSE.txt)

## Contributing

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
