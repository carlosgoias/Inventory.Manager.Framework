namespace Inventory.Manager.Framework.WebUI.Sample.Items
{
    using Inventory.Manager.Framework;

    public class Chair : IItem
    {
        public Chair(string description, int id, string name)
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
                return nameof(Chair);
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}