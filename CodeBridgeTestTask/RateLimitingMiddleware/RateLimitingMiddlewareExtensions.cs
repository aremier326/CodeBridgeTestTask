namespace CodeBridgeTestTask.RateLimitingMiddleware
{
    public static class RateLimitingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRateLimitingMiddleware(this IApplicationBuilder app,
            int maxRequests, TimeSpan timeSpan)
        {
            return app.UseMiddleware<RateLimitingMiddleware>(maxRequests, timeSpan);
        }
    }
}
