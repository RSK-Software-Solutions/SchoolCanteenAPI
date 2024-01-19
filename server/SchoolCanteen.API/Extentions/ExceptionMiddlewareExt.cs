using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace SchoolCanteen.API.Extentions;

public static class ExceptionMiddlewareExt
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
    {
        app.UseExceptionHandler(error =>
        {
            error.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    logger.LogError($"Error: {contextFeature.Error}");
                    //await context.Response.WriteAsync(new CustomResponse()
                    //{
                    //    StatusCode =context.Response.StatusCode.ToString(),
                    //    MessageProcessingHandler = "Internal Server Error."
                    //}.ToString());
                }
            });
        });
    }
}
