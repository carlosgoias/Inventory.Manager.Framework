namespace Inventory.Manager.Framework.Exceptions
{
    [Serializable]
    internal class WarehouseAlreadyIntializedException : Exception
    {
        public WarehouseAlreadyIntializedException(string? message) : base(message)
        {
        }
    }
}