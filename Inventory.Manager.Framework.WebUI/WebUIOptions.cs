namespace Inventory.Manager.Framework.WebUI
{
    using System.Reflection;

    internal static class WebUIOptions
    {
        /// <summary>
        /// Gets title for the Web UI
        /// </summary>
        public static string DocumentTitle => "Inventory Manager";

        /// <summary>
        /// Gets additional content to place in the head of the Web UI
        /// </summary>
        public static string HeadContent => "";

        /// <summary>
        /// Gets a Stream function for retrieving the Web UI
        /// </summary>
        public static Func<Stream?> IndexStream => () => typeof(WebUIOptions).GetTypeInfo().Assembly
            .GetManifestResourceStream("Inventory.Manager.Framework.WebUI.index.html");

        /// <summary>
        /// Gets or set route prefix for accessing the Web UI
        /// </summary>
        public static string RoutePrefix => "inventory";
    }
}