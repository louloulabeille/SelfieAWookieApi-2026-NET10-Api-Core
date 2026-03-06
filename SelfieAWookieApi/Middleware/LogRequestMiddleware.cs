namespace SelfieAWookieApi.Middleware
{
    public class LogRequestMiddleware (RequestDelegate? next, ILogger<LogRequestMiddleware>? logger)
    {
        #region private Fields
        private RequestDelegate? _next = next;
        private ILogger<LogRequestMiddleware>? _logger = logger;
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
