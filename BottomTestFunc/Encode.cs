using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BottomTextTranslator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace BottomTextFunc
{
    public class Encode
    {
        private readonly ILogger<Encode> _logger;

        public Encode(ILogger<Encode> log)
        {
            _logger = log;
        }

        [FunctionName("Encode")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "message", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "the message to encode in bottomtext in base64/utf8")]
        [OpenApiParameter(name: "keytype", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "the keyboard type to encode in")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string message = req.Query["message"];
            string keytype = req.Query["keytype"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            dynamic data = JsonConvert.DeserializeObject(requestBody);
            message = message ?? data?.message;
            keytype = keytype ?? data?.keytype;

            message = Encoding.UTF8.GetString(Convert.FromBase64String(message));

            if(message.ToLower().Equals("coffee"))
            {
                var result = new ObjectResult("beep beep im a teapot i cant make coffee :(");
                result.StatusCode = StatusCodes.Status418ImATeapot;
                return result;
            }

            var encodedmessage = BottomText.Encode(message, keytype);

            return new OkObjectResult(encodedmessage);
        }
    }


}

