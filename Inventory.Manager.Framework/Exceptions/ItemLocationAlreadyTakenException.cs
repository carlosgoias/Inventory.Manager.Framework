namespace Inventory.Manager.Framework.Exceptions
{
    [Serializable]
    internal sealed class ItemLocationAlreadyTakenException : Exception
    {
        public ItemLocationAlreadyTakenException(string? message) : base(message)
        {
        }
    }
}