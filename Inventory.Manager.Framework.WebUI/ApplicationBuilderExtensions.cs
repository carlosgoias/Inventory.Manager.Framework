using Inventory.Manager.Framework.WebUI.Handlers;
using Microsoft.AspNetCore.Builder;

namespace Inventory.Manager.Framework.WebUI
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseInventoryManagerWebUI(this IApplicationBuilder app, IManager manager)
        {
            app.UseMiddleware<WebUIMiddleware>(
                new List<IHttpRequestHandler>
                {
                    new GetIndexPageHandler(),
                    new GetSettingsHandler(manager),
                    new GetItemsHandler(manager)
                });

            return app;
        }
    }
}