namespace Inventory.Manager.Framework.WebUI
{
    using Microsoft.AspNetCore.Http;

    internal interface IHttpRequestHandler
    {
        Task<bool> HandleAsync(HttpRequest httpRequest, HttpResponse httpResponse, RequestDelegate next);
    }
}