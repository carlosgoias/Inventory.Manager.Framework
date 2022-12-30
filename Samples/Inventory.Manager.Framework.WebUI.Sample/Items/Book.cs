namespace Inventory.Manager.Framework.WebUI.Sample.Items
{
    using Inventory.Manager.Framework;

    internal sealed class Book : IItem
    {
        public Book(string description, int id, string name)
        {
            Description = description;
            Id = id;
            Name = name;
        }

        public string Description { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public string Type
        {
            get
            {
                return nameof(Book);
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}