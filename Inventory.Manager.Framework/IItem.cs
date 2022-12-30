namespace Inventory.Manager.Framework
{
    public interface IItem
    {
        public string Description { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}