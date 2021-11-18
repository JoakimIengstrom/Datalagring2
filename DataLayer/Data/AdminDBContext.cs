using System;
using System.Diagnostics;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataLayer.Data
{
    public class AdminDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<FoodBox> FoodBoxes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasIndex(b => b.Email)
                .IsUnique();           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .LogTo(m => Debug.WriteLine(m), LogLevel.Information)
                    .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=FoodRescue"
                    );
            }
        }

        public void Seed()
        {
            using var ctx = new AdminDbContext();

            var customers = new Customer[]
            {
                new() {FullName = "Joakim",     PassWord = "pwd123", PassReg = DateTime.Today - TimeSpan.FromDays(12),  Email = "joakim@gmail.com"},
                new() {FullName = "Anna-Märta", PassWord = "pwd234", PassReg = DateTime.Today - TimeSpan.FromDays(10),  Email = "anna_marta@gmail.com"},
                new() {FullName = "Veronica",   PassWord = "pwd345", PassReg = DateTime.Today - TimeSpan.FromDays(7),   Email = "veronica@gmail.com"},
                new() {FullName = "Kim",        PassWord = "pwd456", PassReg = DateTime.Today - TimeSpan.FromDays(7),   Email = "kim@gmail.com"},
                new() {FullName = "Theo",       PassWord = "pwd567", PassReg = DateTime.Today - TimeSpan.FromDays(6),   Email = "theo@gmail.com"},
                new() {FullName = "Pia",        PassWord = "pwd678", PassReg = DateTime.Today - TimeSpan.FromDays(4),   Email = "pia@gmail.com"},
                new() {FullName = "Poya",       PassWord = "pwd789", PassReg = DateTime.Today - TimeSpan.FromDays(3),   Email = "poya@gmail.com"},
                new() {FullName = "Björn",      PassWord = "pwd890", PassReg = DateTime.Today - TimeSpan.FromDays(1),   Email = "björn@gmail.com"},
                new() {FullName = "Remove",     PassWord = "Passed", PassReg = DateTime.Today - TimeSpan.FromDays(25),  Email = "jondoe@gmail.com"},
            };

            ctx.Customers.AddRange(customers);
            ctx.SaveChanges();

            var restaurants = new Restaurant[]
            {
                new() {RestaurantName = "BohusPizza",   City = "Bohus",     Phonenumber = "0302-283550"},
                new() {RestaurantName = "BohusSushi",   City = "Bohus",     Phonenumber = "0302-230607"},
                new() {RestaurantName = "OliviasFood",  City = "Kungälv",   Phonenumber = "0303-324305"},
                new() {RestaurantName = "Kinan",        City = "Göteborg",  Phonenumber = "031-5686597"},
                new() {RestaurantName = "Thaien",       City = "Älvängen",  Phonenumber = "0302-532554"},
                new() {RestaurantName = "DaMille",      City = "Älvängen",  Phonenumber = "0302-535543"},
                new() {RestaurantName = "OldCorner",    City = "Göteborg",  Phonenumber = "031-4432467"},
                new() {RestaurantName = "Haket",        City = "Göteborg",  Phonenumber = "031-5463245"},
                new() {RestaurantName = "Mantram",      City = "Kungälv",   Phonenumber = "0303-864564"},
                new() {RestaurantName = "Vi-Vet",       City = "Göteborg",  Phonenumber = "031-3214524"},
                new() {RestaurantName = "Test",         City = "Göteborg",  Phonenumber = "031-1234567"},
            };

            ctx.Restaurants.AddRange(restaurants);
            ctx.SaveChanges();

            var orders = new Order[]
            {
                new() { DeliveryDate = DateTime.Now - TimeSpan.FromHours(14), Customers = customers [0] },
                new() { DeliveryDate = DateTime.Now - TimeSpan.FromHours(9),  Customers = customers [1] },
                new() { DeliveryDate = DateTime.Now + TimeSpan.FromHours(2),  Customers = customers [1] },
                new() { DeliveryDate = DateTime.Now + TimeSpan.FromHours(3),  Customers = customers [2] },
                new() { DeliveryDate = DateTime.Now + TimeSpan.FromHours(5),  Customers = customers [3] },
                new() { DeliveryDate = DateTime.Now + TimeSpan.FromHours(7),  Customers = customers [4] },
                new() { DeliveryDate = DateTime.Now + TimeSpan.FromHours(8),  Customers = customers [5] },
                new() { DeliveryDate = DateTime.Now + TimeSpan.FromHours(9),  Customers = customers [6] },
                new() { DeliveryDate = DateTime.Now + TimeSpan.FromHours(2),  Customers = customers [0] },
            };

            ctx.Orders.AddRange(orders);
            ctx.SaveChanges();

            var foodBoxes = new FoodBox[]
            {
                new() {BoxName = "Pizza",   BoxCategory = "Vegan",      Price = 55, BestBefore = DateTime.Today + TimeSpan.FromDays(1), Restaurant = restaurants [0], Order = orders [0] },
                new() {BoxName = "Sushi",   BoxCategory = "Fish",       Price = 65, BestBefore = DateTime.Today + TimeSpan.FromDays(2), Restaurant = restaurants [1], Order = orders [1] },
                new() {BoxName = "Sallad",  BoxCategory = "Meat",       Price = 78, BestBefore = DateTime.Today + TimeSpan.FromDays(1), Restaurant = restaurants [2], Order = orders [2] },
                new() {BoxName = "Ricebox", BoxCategory = "Meat",       Price = 59, BestBefore = DateTime.Today + TimeSpan.FromDays(2), Restaurant = restaurants [3], Order = orders [3] },
                new() {BoxName = "Fried",   BoxCategory = "Chicken",    Price = 87, BestBefore = DateTime.Today + TimeSpan.FromDays(2), Restaurant = restaurants [4], Order = orders [4] },
                new() {BoxName = "Pizza",   BoxCategory = "Vegetarian", Price = 46, BestBefore = DateTime.Today + TimeSpan.FromDays(3), Restaurant = restaurants [5], Order = orders [5] },
                new() {BoxName = "Pizza",   BoxCategory = "Vegan",      Price = 56, BestBefore = DateTime.Today + TimeSpan.FromDays(4), Restaurant = restaurants [6], Order = orders [6] },
                new() {BoxName = "Snitzel", BoxCategory = "Meat",       Price = 61, BestBefore = DateTime.Today + TimeSpan.FromDays(1), Restaurant = restaurants [7], Order = orders [7] },
                new() {BoxName = "Pizza",   BoxCategory = "Fish",       Price = 67, BestBefore = DateTime.Today + TimeSpan.FromDays(1), Restaurant = restaurants [5], Order = orders [8] },
                new() {BoxName = "Sushi",   BoxCategory = "Fish",       Price = 65, BestBefore = DateTime.Today + TimeSpan.FromDays(2), Restaurant = restaurants [1] },
                new() {BoxName = "Sallad",  BoxCategory = "Meat",       Price = 78, BestBefore = DateTime.Today + TimeSpan.FromDays(1), Restaurant = restaurants [2] },
                new() {BoxName = "Ricebox", BoxCategory = "Meat",       Price = 58, BestBefore = DateTime.Today + TimeSpan.FromDays(2), Restaurant = restaurants [3] },
                new() {BoxName = "Fried",   BoxCategory = "Chicken",    Price = 77, BestBefore = DateTime.Today + TimeSpan.FromDays(2), Restaurant = restaurants [4] },
                new() {BoxName = "Pizza",   BoxCategory = "Vegetarian", Price = 36, BestBefore = DateTime.Today + TimeSpan.FromDays(3), Restaurant = restaurants [5] },
                new() {BoxName = "Pizza",   BoxCategory = "Vegan",      Price = 46, BestBefore = DateTime.Today + TimeSpan.FromDays(4), Restaurant = restaurants [6] },
                new() {BoxName = "Snitzel", BoxCategory = "Meat",       Price = 66, BestBefore = DateTime.Today + TimeSpan.FromDays(1), Restaurant = restaurants [7] },
                new() {BoxName = "Pizza",   BoxCategory = "Vegan",      Price = 57, BestBefore = DateTime.Today + TimeSpan.FromDays(1), Restaurant = restaurants [7] },
                new() {BoxName = "Sushi",   BoxCategory = "Fish",       Price = 65, BestBefore = DateTime.Today + TimeSpan.FromDays(2), Restaurant = restaurants [6] },
                new() {BoxName = "Sallad",  BoxCategory = "Meat",       Price = 79, BestBefore = DateTime.Today + TimeSpan.FromDays(1), Restaurant = restaurants [5] },
                new() {BoxName = "Ricebox", BoxCategory = "Meat",       Price = 49, BestBefore = DateTime.Today + TimeSpan.FromDays(2), Restaurant = restaurants [4] },
                new() {BoxName = "Fried",   BoxCategory = "Chicken",    Price = 88, BestBefore = DateTime.Today + TimeSpan.FromDays(2), Restaurant = restaurants [3] },
                new() {BoxName = "Pizza",   BoxCategory = "Vegetarian", Price = 56, BestBefore = DateTime.Today + TimeSpan.FromDays(3), Restaurant = restaurants [2] },
                new() {BoxName = "Pizza",   BoxCategory = "Vegan",      Price = 36, BestBefore = DateTime.Today + TimeSpan.FromDays(4), Restaurant = restaurants [1] },
                new() {BoxName = "Snitzel", BoxCategory = "Meat",       Price = 61, BestBefore = DateTime.Today + TimeSpan.FromDays(1), Restaurant = restaurants [0] },
                new() {BoxName = "Pizza",   BoxCategory = "Vegan",      Price = 55, BestBefore = DateTime.Today + TimeSpan.FromDays(1), Restaurant = restaurants [0] },
                new() {BoxName = "Sushi",   BoxCategory = "Fish",       Price = 65, BestBefore = DateTime.Today + TimeSpan.FromDays(2), Restaurant = restaurants [1] },
                new() {BoxName = "Sallad",  BoxCategory = "Meat",       Price = 78, BestBefore = DateTime.Today + TimeSpan.FromDays(2), Restaurant = restaurants [2] },
                new() {BoxName = "Ricebox", BoxCategory = "Meat",       Price = 59, BestBefore = DateTime.Today + TimeSpan.FromDays(3), Restaurant = restaurants [3] },
                new() {BoxName = "Fried",   BoxCategory = "Chicken",    Price = 87, BestBefore = DateTime.Today + TimeSpan.FromDays(2), Restaurant = restaurants [4] },
                new() {BoxName = "Pizza",   BoxCategory = "Vegetarian", Price = 46, BestBefore = DateTime.Today + TimeSpan.FromDays(3), Restaurant = restaurants [5] },
                new() {BoxName = "Pizza",   BoxCategory = "Vegan",      Price = 56, BestBefore = DateTime.Today + TimeSpan.FromDays(1), Restaurant = restaurants [6] },
                new() {BoxName = "Snitzel", BoxCategory = "Meat",       Price = 61, BestBefore = DateTime.Today + TimeSpan.FromDays(1), Restaurant = restaurants [7] },
                new() {BoxName = "Pizza",   BoxCategory = "Vegan",      Price = 55, BestBefore = DateTime.Today - TimeSpan.FromDays(1), Restaurant = restaurants [7] },
                new() {BoxName = "Sushi",   BoxCategory = "Fish",       Price = 65, BestBefore = DateTime.Today - TimeSpan.FromDays(2), Restaurant = restaurants [6] },
                new() {BoxName = "Sallad",  BoxCategory = "Meat",       Price = 69, BestBefore = DateTime.Today - TimeSpan.FromDays(1), Restaurant = restaurants [5] },
                new() {BoxName = "Ricebox", BoxCategory = "Meat",       Price = 49, BestBefore = DateTime.Today - TimeSpan.FromDays(2), Restaurant = restaurants [4] },
                new() {BoxName = "Fried",   BoxCategory = "Chicken",    Price = 37, BestBefore = DateTime.Today - TimeSpan.FromDays(2), Restaurant = restaurants [3] },
                new() {BoxName = "Pizza",   BoxCategory = "Vegetarian", Price = 46, BestBefore = DateTime.Today - TimeSpan.FromDays(3), Restaurant = restaurants [2] },
                new() {BoxName = "Pizza",   BoxCategory = "Vegan",      Price = 56, BestBefore = DateTime.Today - TimeSpan.FromDays(3), Restaurant = restaurants [1] },
                new() {BoxName = "Snitzel", BoxCategory = "Meat",       Price = 61, BestBefore = DateTime.Today - TimeSpan.FromDays(1), Restaurant = restaurants [0] },
            };
            ctx.FoodBoxes.AddRange(foodBoxes);
            ctx.SaveChanges();


           
        }
    }
}
