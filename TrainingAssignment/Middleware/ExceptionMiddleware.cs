namespace TrainingAssignment
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Customize the response based on the exception
            // For example, you can set the response status code and return an error message
            _logger.LogError(exception, "An exception occurred during request processing.");

            if (exception is HttpRequestException)
            {
                // Handle HttpRequestException
                context.Response.StatusCode = 400;
                context.Response.Redirect("/Error/HttpError");
            }
            else if (exception is NotImplementedException)
            {
                context.Response.StatusCode = 400;
                context.Response.Redirect("/Error/NotImplementedError");
            }
            else
            {
                // Handle other exceptions
                context.Response.StatusCode = 400;
                context.Response.Redirect("/Error/Error");
            }
        }
    }
}
