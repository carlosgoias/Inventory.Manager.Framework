namespace Inventory.Manager.Framework.Exceptions
{
    [Serializable]
    internal sealed class NotAllowedToAddItemException : Exception
    {
        public NotAllowedToAddItemException(string? message) : base(message)
        {
        }
    }
}