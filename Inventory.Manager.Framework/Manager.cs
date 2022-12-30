namespace Inventory.Manager.Framework
{
    using Inventory.Manager.Framework.Exceptions;

    public sealed class Manager : IManager
    {
        private readonly Settings settings;
        private readonly IWharehouse wharehouse;

        public Manager(IWharehouse wharehouse, Settings settings)
        {
            this.settings = settings;
            this.wharehouse = wharehouse;
        }

        public void AddInitialSpace(int totalCorridors, int totalStands)
        {
            if (this.wharehouse.GetItems().Any())
            {
                throw new WarehouseAlreadyIntializedException("Wharehouse cannot be initiate it already have items");
            }

            var itemLocations = new List<ItemLocation>();

            for (int corridor = 0; corridor < totalCorridors; corridor++)
            {
                for (int stand = 0; stand < totalStands; stand++)
                {
                    itemLocations.Add(new ItemLocation(corridor, stand));
                }
            }

            this.wharehouse.AddInitialSpace(itemLocations);
        }

        public void AddItem(IItem item, ItemLocation itemLocation)
        {
            if (this.wharehouse.GetItems().Count(d => d.Item1 is not null) > this.settings.MaxItemsOnWharehouse)
            {
                throw new NotAllowedToAddItemException($"Not allowed to add more items than {this.settings.MaxItemsOnWharehouse}");
            }

            if (this.wharehouse.GetItems()
                .Any((i) => i.Item2.Corridor == itemLocation.Corridor
                    && i.Item2.Stand == itemLocation.Stand
                    && i.Item1 is not null))
            {
                throw new ItemLocationAlreadyTakenException($"Item location already taken Corridor: {itemLocation.Corridor} - Stand: {itemLocation.Stand}");
            }

            if (!this.wharehouse.GetItems()
                .Any((i) => i.Item2.Corridor == itemLocation.Corridor && i.Item2.Stand == itemLocation.Stand))
            {
                throw new ItemLocationNotFoundException($"Item location not found Corridor: {itemLocation.Corridor} - Stand: {itemLocation.Stand}");
            }

            this.wharehouse.Add(item, itemLocation);
        }

        public void ChangeSettings(KeyValuePair<string, string> setting)
        {
            if (int.TryParse(setting.Value, out int result))
            {
                if (setting.Key == nameof(this.settings.MaxItemsOnWharehouse))
                {
                    this.settings.MaxItemsOnWharehouse = result;
                }
                else if (setting.Key == nameof(this.settings.MinItemsOnWharehouse))
                {
                    this.settings.MinItemsOnWharehouse = result;
                }
                else
                {
                    throw new NotSupportedException($"Settings {nameof(setting.Key)} not supported yet");
                }

                return;
            }

            throw new InvalidCastException($"Settings {setting.Value} has be an int type");
        }

        public IReadOnlyList<(IItem?, ItemLocation)> GetItems()
        {
            return this.wharehouse.GetItems();
        }

        public IDictionary<string, string> GetSettings()
        {
            return new Dictionary<string, string>
            {
                { nameof(this.settings.MaxItemsOnWharehouse) , this.settings.MaxItemsOnWharehouse.ToString()},
                { nameof(this.settings.MinItemsOnWharehouse) , this.settings.MinItemsOnWharehouse.ToString()},
            };
        }

        public void RemoveItem(IItem item, ItemLocation itemLocation)
        {
            if (this.wharehouse.GetItems().Count(d => d.Item1 is not null) < this.settings.MinItemsOnWharehouse)
            {
                throw new NotAllowedToRemoveItemException($"Not allowed to remove more items than {this.settings.MinItemsOnWharehouse}");
            }

            this.wharehouse.Remove(item, itemLocation);
        }
    }
}