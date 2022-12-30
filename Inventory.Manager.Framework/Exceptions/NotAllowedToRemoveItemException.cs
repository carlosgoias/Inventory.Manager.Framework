namespace Inventory.Manager.Framework.Exceptions
{
    [Serializable]
    internal sealed class NotAllowedToRemoveItemException : Exception
    {
        public NotAllowedToRemoveItemException(string? message) : base(message)
        {
        }
    }
}