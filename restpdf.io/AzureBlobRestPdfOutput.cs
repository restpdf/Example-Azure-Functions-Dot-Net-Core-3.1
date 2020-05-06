using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace restpdf
{
    /*
    This example functions shows how to generate PDF's in Azure Functions and output them directly to Azure BLOB storage from the RestPdf.io API.
    */
    public static class respdfnative
    {
        [FunctionName("AzureBlobRestPdfOutput")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest httpRequest,
        ILogger log)
        {
            // The API key can be generated from your dashboard at https://app.restpdf.io
            var ApiKey = "";
            // The Azure storage account name you want to save the generated PDF to.
            var TargetAzureBlobAcccountName = "";
            // The Azure storage account key that corresponds with the Azure storage account name you specified above.
            var TargetAzureBlobAcccountKey = "";
            // The container name you want to save the generated PDF to.
            var TargetAzureBlobContainerName = "restpdf"; 

            var RequestBodyParameters = new
            {
                output = "azure_blob",
                url = "https://www.google.co.uk",
                azure_blob = new
                {
                    account_name = TargetAzureBlobAcccountName,
		            account_key = TargetAzureBlobAcccountKey,
		            container_name = TargetAzureBlobContainerName
                }
            };

            var RequestBody = new StringContent(JsonSerializer.Serialize(RequestBodyParameters), Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-KEY", ApiKey);

                using (var result = client.PostAsync("https://api.restpdf.io/v1/pdf", RequestBody).Result)
                {
                    if (result.IsSuccessStatusCode)
                    {
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
