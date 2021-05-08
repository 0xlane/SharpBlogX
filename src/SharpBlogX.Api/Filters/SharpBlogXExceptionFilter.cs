using SharpBlogX.Extensions;
using SharpBlogX.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SharpBlogX.Api.Filters
{
    public class SharpBlogXExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                var result = new BlogResponse();
                result.IsFailed(context.Exception.Message);

                context.Result = new ContentResult()
                {
                    Content = result.SerializeToJson(),
                    StatusCode = StatusCodes.Status200OK
                };

                context.ExceptionHandled = true;
            }
        }
    }
}