namespace Inventory.Manager.Framework.WebUI.Handlers
{
    using System.Text;
    using System.Text.RegularExpressions;
    using Microsoft.AspNetCore.Http;

    internal sealed class GetIndexPageHandler : WebUIRequestHandlerBase
    {
        private static readonly string[] resourcePath = new[] { "/{0}", "/{0}/", "/{0}/index.html" };

        public GetIndexPageHandler() : base(resourcePath)
        {
        }

        protected override HttpMethod HttpMethod => HttpMethod.GET;

        protected override async Task HandleRequestAsync(HttpRequest httpRequest, HttpResponse httpResponse, RequestDelegate next)
        {
            var path = httpRequest.Path.Value;
            var httpContext = httpRequest.HttpContext;

            if (Regex.IsMatch(path, $"^/?{Regex.Escape(WebUIOptions.RoutePrefix)}/?$", RegexOptions.IgnoreCase))
            {
                // Use relative redirect to support proxy environments
                var relativeIndexUrl = string.IsNullOrEmpty(path) || path.EndsWith("/")
                    ? "index.html"
                    : $"{path.Split('/').Last()}/index.html";

                RespondWithRedirect(httpContext.Response, relativeIndexUrl);
            }

            if (Regex.IsMatch(path, $"^/{Regex.Escape(WebUIOptions.RoutePrefix)}/?index.html$", RegexOptions.IgnoreCase))
            {
                await this.RespondWithIndexHtmlAsync(httpContext.Response, next).ConfigureAwait(false);
            }
        }

        private static void RespondWithRedirect(HttpResponse httpResponse, string location)
        {
            if (!httpResponse.HasStarted)
            {
                httpResponse.StatusCode = 301;
                httpResponse.Headers["Location"] = location;
            }
        }

        private IDictionary<string, string> GetIndexArguments()
        {
            return new Dictionary<string, string>
            {
                { "%(DocumentTitle)", WebUIOptions.DocumentTitle },
                { "%(HeadContent)", WebUIOptions.HeadContent }
            };
        }

        private async Task RespondWithIndexHtmlAsync(HttpResponse httpResponse, RequestDelegate next)
        {
            if (!httpResponse.HasStarted)
            {
                httpResponse.StatusCode = 200;
                httpResponse.ContentType = "text/html;charset=utf-8";

                var originalBody = httpResponse.Body;

                using (var stream = WebUIOptions.IndexStream())
                {
                    httpResponse.Body = stream;
                    await next(httpResponse.HttpContext).ConfigureAwait(false);

                    using (var reader = new StreamReader(stream))
                    {
                        var body = await reader.ReadToEndAsync().ConfigureAwait(false);

                        var responseTextBuilder = new StringBuilder(body);

                        foreach (var entry in this.GetIndexArguments())
                        {
                            responseTextBuilder.Replace(entry.Key, entry.Value);
                        }

                        var byteArray = Encoding.UTF8.GetBytes(responseTextBuilder.ToString());
                        using (var newStream = new MemoryStream(byteArray))
                        {
                            httpResponse.Body = originalBody;
                            newStream.Seek(0, SeekOrigin.Begin);
                            await newStream.CopyToAsync(httpResponse.Body).ConfigureAwait(false);
                        }
                    }
                }
            }
        }
    }
}