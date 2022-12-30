namespace Inventory.Manager.Framework.WebUI.Handlers
{
    using System.Net;
    using Microsoft.AspNetCore.Http;

    internal sealed class GetItemsHandler : WebUIRequestHandlerBase
    {
        private static readonly string[] resourcePath = new[] { "/{0}/api/v1/items" };
        private readonly IManager manager;

        public GetItemsHandler(IManager manager) : base(resourcePath)
        {
            this.manager = manager;
        }

        protected override HttpMethod HttpMethod => HttpMethod.GET;

        protected override Task HandleRequestAsync(HttpRequest httpRequest, HttpResponse httpResponse, RequestDelegate next)
        {
            try
            {
                var items = this.manager.GetItems().Where(d => d.Item1 is not null);

                return this.WriteResponseAsync(httpResponse, items, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return this.WriteExceptionResponseAsync(httpResponse, ex);
            }
        }
    }
}