namespace Inventory.Manager.Framework
{
    public interface IManager
    {
        void AddItem(IItem item, ItemLocation itemLocation);

        void ChangeSettings(KeyValuePair<string, string> setting);

        IReadOnlyList<(IItem?, ItemLocation)> GetItems();

        IDictionary<string, string> GetSettings();

        void RemoveItem(IItem item, ItemLocation itemLocation);
    }
}