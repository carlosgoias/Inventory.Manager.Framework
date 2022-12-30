namespace Inventory.Manager.Framework.WebUI.Sample.Repositories
{
    using Inventory.Manager.Framework;

    internal sealed class WarehouseRepository : IWharehouse
    {
        private readonly IDictionary<string, IItem?> warehouse;

        public WarehouseRepository()
        {
            this.warehouse = new Dictionary<string, IItem?>();
        }

        public void Add(IItem item, ItemLocation itemLocation)
        {
            this.warehouse[$"{itemLocation.Corridor}-{itemLocation.Stand}"] = item;
        }

        public void AddInitialSpace(IEnumerable<ItemLocation> itemLocations)
        {
            foreach (var itemLocation in itemLocations)
            {
                this.warehouse.Add($"{itemLocation.Corridor}-{itemLocation.Stand}", null);
            }
        }

        public IReadOnlyList<(IItem?, ItemLocation)> GetItems()
        {
            return this.warehouse.Select(i =>
            {
                var arrLocation = i.Key.Split('-');

                (IItem?, ItemLocation) item = new(i.Value, new ItemLocation(int.Parse(arrLocation[0]), int.Parse(arrLocation[1])));

                return item;
            }).ToList();
        }

        public void Remove(IItem item, ItemLocation itemLocation)
        {
            this.warehouse[$"{itemLocation.Corridor}-{itemLocation.Stand}"] = null;
        }
    }
}