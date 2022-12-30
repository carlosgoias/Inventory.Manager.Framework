namespace Inventory.Manager.Framework
{
    public sealed class Settings
    {
        public Settings(int maxItemsOnWharehouse, int minItemsOnWharehouse)
        {
            MaxItemsOnWharehouse = maxItemsOnWharehouse;
            MinItemsOnWharehouse = minItemsOnWharehouse;
        }

        public int MaxItemsOnWharehouse { get; set; }
        public int MinItemsOnWharehouse { get; set; }
    }
}