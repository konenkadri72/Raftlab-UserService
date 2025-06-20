using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Raftlab.Core.ProjectEnums;
using Raftlab.Service.BaseResponse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Raftlab.Web.ActionAttributes
{
    public class ResponseAttributes : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {


            if (!context.ModelState.IsValid)
            {
                List<MessageData> modelErrors = new List<MessageData>();
                foreach (var modelStateKey in context.ModelState.Keys)
                {
                    var modelStateVal = context.ModelState[modelStateKey];
                    foreach (var modelError in modelStateVal.Errors)
                    {
                        modelErrors.Add(new MessageData()
                        {
                            Title = modelStateKey,
                            Message = modelError.ErrorMessage,
                            Type = MessageTypeEnum.Error.ToString()
                        });
                    }
                }

                context.Result = new OkObjectResult(new Response<string>(modelErrors));
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {

            var objectResult = context.Result as ObjectResult;
            if (objectResult != null)
            {
                if (context.Result is BadRequestObjectResult)
                {
                    if (objectResult.Value is SerializableError)
                    {
                        List<MessageData> modelErrors = new List<MessageData>();
                        var data = objectResult.Value as SerializableError;
                        foreach (KeyValuePair<string, object> keyValuePair in data)
                        {
                            var modelStateKey = keyValuePair.Key;
                            foreach (var error in (string[])keyValuePair.Value)
                            {
                                modelErrors.Add(new MessageData()
                                {
                                    Title = modelStateKey,
                                    Message = error,
                                    Type = MessageTypeEnum.Error.ToString()
                                });
                            }
                        }

                        context.Result = new OkObjectResult(new Response<string>(modelErrors));
                    }
                    else if (!objectResult.Value.GetType().IsGenericTypeDefinition)
                    {
                        context.Result = new OkObjectResult(new Response<string>(objectResult.Value.ToString(), objectResult.Value.ToString()));
                    }
                    else if (objectResult.Value.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Response<>)))
                    {

                        context.Result = new OkObjectResult(new Response<string>(objectResult.Value.GetType().GetProperty("Errors").GetValue(objectResult.Value, null) as List<MessageData>));
                    }
                    else
                    {
                        context.Result = new OkObjectResult(new Response<string>("Error", ResponseTypeEnum.Fail.ToString()));
                    }
                }
                else
                {
                    if (objectResult.Value.GetType().IsGenericType && objectResult.Value.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Response<>)))
                    {
                        context.Result = new OkObjectResult(objectResult.Value);
                    }
                    else
                    {

                        context.Result = new OkObjectResult(new Response<object>(objectResult.Value));

                    }
                }
            }
        }

        private static string GetJoinedValue(string[] value)
        {
            if (value != null)
                return string.Join(",", value);

            return null;
        }

        private static async Task<IDictionary<string, string>> GetBodyParameters(HttpRequest request)
        {
            var dictionary = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);

            if (request.ContentType != "application/json" && request.ContentType != "application/json-patch+json")
            {
                var formCollectionTask = await request.ReadFormAsync();

                foreach (var pair in formCollectionTask)
                {
                    var value = GetJoinedValue(pair.Value);
                    dictionary.Add(pair.Key, value);
                }
            }
            else
            {
                using (var stream = new MemoryStream())
                {
                    //request.Body.CopyToG(stream);

                    request.EnableBuffering();
                    // Read the stream as text
                    var bodyAsText = await new System.IO.StreamReader(request.Body).ReadToEndAsync();
                    // Set the position of the stream to 0 to enable rereading
                    request.Body.Position = 0;
                    //var result = Encoding.UTF8.GetString(stream.ToArray());
                    var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(bodyAsText);
                    if (dict != null)
                    {
                        foreach (var pair in dict)
                        {
                            string value = (pair.Value is string)
                                ? Convert.ToString(pair.Value)
                                : JsonSerializer.Serialize(pair.Value);
                            dictionary.Add(pair.Key, value);
                        }
                    }
                }
            }

            return dictionary;
        }

        /// <inheritdoc />
        public override async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (next == null)
                throw new ArgumentNullException(nameof(next));
            this.OnActionExecuting(context);
            if (context.Result != null)
                return;

            var nextContext = await next();
            this.OnActionExecuted(nextContext);

            if (!nextContext.ModelState.IsValid)
            {

            }

            if (nextContext.Exception != null)
            {
                //var body = await GetBodyParameters(context.HttpContext.Request);

                nextContext.Result = new OkObjectResult(new Response<string>("Something went wrong.", "Please refresh your page and try again."));
                nextContext.Exception = null;
            }
        }
    }
}
