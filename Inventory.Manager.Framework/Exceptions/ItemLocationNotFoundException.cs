namespace Inventory.Manager.Framework.Exceptions
{
    [Serializable]
    internal class ItemLocationNotFoundException : Exception
    {
        public ItemLocationNotFoundException(string? message) : base(message)
        {
        }
    }
}