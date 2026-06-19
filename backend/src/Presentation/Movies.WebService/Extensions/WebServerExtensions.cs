namespace Movies.WebService.Extensions;

/// <summary>
/// Extension methods that configure the Kestrel web server.
/// </summary>
public static class WebServerExtensions
{
    /// <summary>
    /// Configures Kestrel server timeouts to protect against slow or idle connections.
    /// </summary>
    public static WebApplicationBuilder ConfigureWebServer(this WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel((context, options) =>
        {
            var timeoutSeconds = context.Configuration.GetValue("WebServer:RequestTimeoutSeconds", 30);

            // RequestHeadersTimeout: Maximum time to receive complete HTTP request headers per request
            // Protects against slowloris attacks where clients slowly send header data
            options.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(timeoutSeconds);

            // KeepAliveTimeout: Maximum time server waits for next request on idle connection
            // Prevents idle connections from consuming server resources indefinitely
            options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(1);
        });

        return builder;
    }
}
