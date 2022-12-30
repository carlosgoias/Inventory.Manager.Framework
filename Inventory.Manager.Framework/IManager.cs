namespace Inventory.Manager.Framework
{
    public interface IManager
    {
        void AddItem(IItem item, ItemLocation itemLocation);

        public void ChangeSettings(KeyValuePair<string, string> setting);

        IReadOnlyList<(IItem, ItemLocation)> GetItems();

        public IDictionary<string, string> GetSettings();

        void RemoveItem(IItem item, ItemLocation itemLocation);
    }
}