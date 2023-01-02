namespace Inventory.Manager.Framework.WebUI.Handlers
{
    using System.Net;
    using Microsoft.AspNetCore.Http;

    internal sealed class GetSettingsHandler : WebUIRequestHandlerBase
    {
        private static readonly string[] resourcePath = new[] { "/{0}/api/v1/settings" };
        private readonly IManager manager;

        public GetSettingsHandler(IManager manager) : base(resourcePath)
        {
            this.manager = manager;
        }

        protected override HttpMethod HttpMethod => HttpMethod.GET;

        protected override Task HandleRequestAsync(HttpRequest httpRequest, HttpResponse httpResponse, RequestDelegate next)
        {
            try
            {
                var settings = this.manager.GetSettings();

                return this.WriteResponseAsync(httpResponse, settings, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return this.WriteExceptionResponseAsync(httpResponse, ex);
            }
        }
    }
}