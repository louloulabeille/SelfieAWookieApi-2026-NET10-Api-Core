namespace SelfieAWookieApi.Middleware
{
    public class LogRequestMiddleware (RequestDelegate? next, ILogger<LogRequestMiddleware>? logger)
    {
        #region private Fields
        private readonly RequestDelegate? _next = next;
        private readonly ILogger<LogRequestMiddleware>? _logger = logger;
        #endregion

        #region contruct

        #endregion

        #region method
        public async Task Invoke(HttpContext context)
        {
            this._logger?.LogDebug(context.Request.Path.Value);
            this._logger?.LogDebug(context.Request.QueryString.Value);

            await this._next!(context);
        }

        #endregion
    }
}
