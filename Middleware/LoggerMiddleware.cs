namespace ProductDemo.Middleware;

public class LoggerMiddleware
{
    private readonly RequestDelegate _next;

    public LoggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        Console.WriteLine("========== REQUEST START ==========");
        
        Console.WriteLine($"Method: {context.Request.Method}");
        Console.WriteLine($"Path: {context.Request.Path}");
        Console.WriteLine($"Time: {DateTime.Now}");

        await _next(context);

        Console.WriteLine($"Response Status: {context.Response.StatusCode}");

        Console.WriteLine("========== REQUEST END ==========");
    }
}