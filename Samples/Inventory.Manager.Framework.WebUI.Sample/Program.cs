namespace Inventory.Manager.Framework.WebUI.Sample
{
    using Inventory.Manager.Framework.WebUI;
    using Inventory.Manager.Framework.WebUI.Sample.Items;
    using Inventory.Manager.Framework.WebUI.Sample.Repositories;
    using Microsoft.Extensions.DependencyInjection;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var warehouseRepository = new WarehouseRepository();
            var settings = new Settings(10, 5);
            var manager = new Manager(warehouseRepository, settings);
            manager.AddInitialSpace(3, 4);

            manager.AddItem(new Book("Sensational", 1, "A New Story of Our Senses"), new ItemLocation(0, 0));
            manager.AddItem(new Book("The Ghost at the Feast", 2, "America and the Collapse of World Order, 1900-1941"), new ItemLocation(0, 1));
            manager.AddItem(new Book("The Lost Future", 3, "And How to Reclaim It"), new ItemLocation(0, 2));
            manager.AddItem(new Book("The Creative Act", 4, "A Way of Being "), new ItemLocation(0, 3));

            manager.AddItem(new Chair("Vignet", 5, "Wood made"), new ItemLocation(1, 1));
            manager.AddItem(new Chair("Kana", 6, "Living room"), new ItemLocation(1, 2));

            manager.AddItem(new Computer("iMac", 7, "Silver 27'"), new ItemLocation(2, 1));
            manager.AddItem(new Computer("HP", 8, "Black 13'"), new ItemLocation(2, 2));
            manager.AddItem(new Computer("Asus", 9, "Black 14'"), new ItemLocation(2, 3));

            builder.Services.AddSingleton<IManager>(manager);
            // Add services to the container.
            builder.Services.AddRazorPages();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production
                // scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseInventoryManagerWebUI(manager);

            app.Run();
        }
    }
}