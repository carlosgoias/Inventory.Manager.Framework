namespace Inventory.Manager.Framework
{
    public interface IWharehouse
    {
        void Add(IItem item, ItemLocation itemLocation);

        void AddInitialSpace(IEnumerable<ItemLocation> itemLocations);

        IReadOnlyList<(IItem?, ItemLocation)> GetItems();

        void Remove(IItem item, ItemLocation itemLocation);
    }
}