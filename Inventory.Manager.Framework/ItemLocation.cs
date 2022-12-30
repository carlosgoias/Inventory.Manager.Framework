namespace Inventory.Manager.Framework
{
    public sealed class ItemLocation
    {
        public ItemLocation(int corridor, int stand)
        {
            Corridor = corridor;
            Stand = stand;
        }

        public int Corridor { get; private set; }
        public int Stand { get; private set; }
    }
}