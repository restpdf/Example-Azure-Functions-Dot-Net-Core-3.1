using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System;

namespace restpdf
{
    public static class azurenative
    {
        /*
        This example function shows how to generate PDF's in Azure Functions and output them to Azure BLOB storage using an output binding.
        */
        [FunctionName("AzureBlobNativeOutputBinding")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest httpRequest,
        [Blob("restpdf/google.pdf", FileAccess.Write)] Stream AzureOutputBlobStream,
        ILogger log)
        {
            // The API key can be generated from your dashboard at https://app.restpdf.io
            var ApiKey = "";

            var RequestBodyParameters = new
            {
                output = "data",
                url = "https://www.google.co.uk"
            };

            var RequestBody = new StringContent(JsonSerializer.Serialize(RequestBodyParameters), Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-KEY", ApiKey);

                using (var result = client.PostAsync("https://api.restpdf.io/v1/pdf", RequestBody).Result)
                {
                    if (result.IsSuccessStatusCode)
                    {
                        var convertedHtmlToPdf = result.Content.ReadAsStreamAsync().Result;

                        convertedHtmlToPdf.Seek(0, SeekOrigin.Begin);
                        convertedHtmlToPdf.CopyTo(AzureOutputBlobStream);

                        return new OkObjectResult("");
                    }
                    else
                    {
                        return new BadRequestResult();
                    }
                }
            }
        }
    }
}
