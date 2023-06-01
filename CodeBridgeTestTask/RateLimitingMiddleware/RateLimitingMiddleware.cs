namespace CodeBridgeTestTask.RateLimitingMiddleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly int _maxRequests;
        private readonly TimeSpan _timeSpan;
        private static readonly Queue<DateTime> RequestTimestamps = new Queue<DateTime>();

        public RateLimitingMiddleware(RequestDelegate next, int maxRequests, TimeSpan timeSpan)
        {
            _next = next;
            _maxRequests = maxRequests;
            _timeSpan = timeSpan;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var now = DateTime.UtcNow;

            while (RequestTimestamps.Count > 0 && now - RequestTimestamps.Peek() > _timeSpan)
            {
                RequestTimestamps.Dequeue();
            }

            if (RequestTimestamps.Count > _maxRequests)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Too many requests.");
                return;
            }

            RequestTimestamps.Enqueue(now);
            await _next(context);
        }
    }
}
