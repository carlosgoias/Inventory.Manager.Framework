namespace Inventory.Manager.Framework.WebUI
{
    using System.Reflection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.StaticFiles;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    internal sealed class WebUIMiddleware
    {
        private readonly IEnumerable<IHttpRequestHandler> httpRequestHandlers;
        private readonly RequestDelegate next;
        private readonly StaticFileMiddleware staticFileMiddlewares;

        public WebUIMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory,
            IWebHostEnvironment hostingEnvironment,
            IEnumerable<IHttpRequestHandler> httpRequestHandlers)
        {
            this.httpRequestHandlers = httpRequestHandlers;
            this.next = next;
            this.staticFileMiddlewares = CreateStaticFileMiddleware(next, hostingEnvironment, loggerFactory, ".node_modules");
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var anyHandlerExecuted = await this.ExecuteHandlersAsync(httpContext).ConfigureAwait(false);
            if (!anyHandlerExecuted)
            {
                await this.ExecuteStaticFileMiddlewareAsync(httpContext).ConfigureAwait(true);
            }
        }

        private static StaticFileMiddleware CreateStaticFileMiddleware(RequestDelegate next,
             IWebHostEnvironment hostingEnvironment,
            ILoggerFactory loggerFactory,
            string path)
        {
            var asssembly = typeof(WebUIMiddleware).GetTypeInfo().Assembly;

            var provider = new EmbeddedFileProvider(asssembly, asssembly.GetName().Name + path);

            var staticFileOptions = new StaticFileOptions
            {
                RequestPath = string.IsNullOrEmpty(WebUIOptions.RoutePrefix) ? string.Empty : $"/{WebUIOptions.RoutePrefix}",
                FileProvider = provider,
                ServeUnknownFileTypes = true
            };

            return new StaticFileMiddleware(next, hostingEnvironment, Options.Create(staticFileOptions), loggerFactory);
        }

        private async Task<bool> ExecuteHandlersAsync(HttpContext httpContext)
        {
            var results = this.httpRequestHandlers.Select(d => d
                .HandleAsync(httpContext.Request, httpContext.Response, this.next));

            var handle = await Task.WhenAll(results).ConfigureAwait(false);

            if (handle.All(d => !d))
            {
                await this.next(httpContext).ConfigureAwait(false);
                return false;
            }
            return true;
        }

        private Task ExecuteStaticFileMiddlewareAsync(HttpContext httpContext)
        {
            return this.staticFileMiddlewares
                .Invoke(httpContext);
        }
    }
}