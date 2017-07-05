using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using CleenApi.Exceptions;

namespace CleenApi.Web.Handlers
{
  public class CleenApiExceptionHandler : ExceptionHandler
  {
    public override void Handle(ExceptionHandlerContext context)
    {
      if (context.Exception is IEntityNotFoundException)
      {
        context.Result = new TextPlainErrorResult(context.Request,
                                                  HttpStatusCode.NotFound,
                                                  context.Exception.Message);
      }
      else if (context.Exception is IEntityProcessingException)
      {
        context.Result = new TextPlainErrorResult(context.Request,
                                                  HttpStatusCode.InternalServerError,
                                                  context.Exception.Message);
      }
      else
      {
        base.Handle(context);
      }
    }

    private class TextPlainErrorResult : IHttpActionResult
    {
      private readonly HttpStatusCode statusCode;

      private readonly HttpRequestMessage request;

      private readonly string content;

      public TextPlainErrorResult(HttpRequestMessage requestMessage, HttpStatusCode code, string message)
      {
        request = requestMessage;
        statusCode = code;
        content = message;
      }

      public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
      {
        return Task.FromResult(new HttpResponseMessage(statusCode)
          {
            Content = new StringContent(content),
            RequestMessage = request
          });
      }
    }
  }
}