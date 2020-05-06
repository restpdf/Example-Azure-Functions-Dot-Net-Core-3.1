# RestPdf.io  - Example-Azure-Functions-C#

## Overview
The example Azure Functions in this GitHub repository show you how to generate PDF's in Azure Functions using the RestPdf.io API that converts HTML to PDF's.


## Azure Functions
#### AzureBlobNativeOutputBinding

This example function shows how to generate PDF's in Azure Functions and output them to Azure BLOB storage using an output binding.

To use this example Azure Function the following variables will need to be filled out.

```C
// The API key can be generated from your dashboard at https://app.restpdf.io
var ApiKey = "";
```

#### AzureBlobRestPdfOutput

This example functions shows how to generate PDF's in Azure Functions and output them directly to Azure BLOB storage from the RestPdf.io API.

To use this example Azure Function the following variables will need to be filled out.

```C
// The API key can be generated from your dashboard at https://app.restpdf.io
var ApiKey = "";
// The Azure storage account name you want to save the generated PDF to.
var TargetAzureBlobAcccountName = "";
// The Azure storage account key that corresponds with the Azure storage account name you specified above.
var TargetAzureBlobAcccountKey = "";
// The container name you want to save the generated PDF to.
var TargetAzureBlobContainerName = "restpdf"; 
```

## More Information
The sandbox Azure Functions runs your code in has some limitiations which affect your ability to generate PDF's becasue of Win32k.sys (User32/GDI32) Restrictions. For the sake of radical attack surface area reduction, the sandbox prevents almost all of the Win32k.sys APIs from being called, which practically means that most of User32/GDI32 system calls are blocked. For most applications this is not an issue since most Azure Web Apps do not require access to Windows UI functionality (they are web applications after all).

However one common pattern that is affected is PDF file generation. There are multiple libraries used to convert HTML to PDF. Many Windows/.NET specific versions leverage IE APIs and therefore leverage User32/GDI32 extensively. These APIs are largely blocked in the sandbox (regardless of plan) and therefore these frameworks do not work in the sandbox.

RestPdf.io has been designed to get around these limitations. 


## FAQ
* <b>Where can I get an API key?</b> You can generate an API key by signing up to RestPdf.io API at https://app.restpdf.io where you will get 50 PDF free PDF conversions a month. If you need more than 50 conversions a month, we offer monthly paid plans.

## Contact Information
- www.restpdf.io
- info@restpdf.io