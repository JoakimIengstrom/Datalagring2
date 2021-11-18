using DataLayer.Data;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Backend
{
    public class AdminBackend
    {
        public static void PrepDatabase()
        {
            using var ctx = new AdminDbContext();

            ctx.Database.EnsureDeleted();
            Console.WriteLine("Database is Deleted");
            ctx.Database.EnsureCreated();
            Console.WriteLine("Database is Created");
            ctx.Seed();
            Console.WriteLine("Database is Seeded\n");
        }

        public static List<Customer> ListCustomers()
        {
            using var ctx = new AdminDbContext();

            return ctx.Customers.ToList();
        }

        public static List<Restaurant> ListRestaurants()
        {
            using var ctx = new AdminDbContext();

            return ctx.Restaurants.ToList();

        }

        public static void DeleteCustomer()
        {
            using var ctx = new AdminDbContext();

            foreach (var customer in ctx.Customers)
            {
                Console.WriteLine(
                    $"ID: {customer.ID} / Customer: {customer.FullName}");
            }

            Console.WriteLine("\nHave to delete a customer without orders. ");
            Console.WriteLine("\nWant to continue? Press y to continue ");
            string option = Console.ReadLine();

            if (option == "y")
            {
                Console.WriteLine("\nType in ID to delete: \n"); //Vet inte varför men har inget username på mina customers ännu, lägger till det om jag får till det i tid.
                
                int chosenCustomer = Convert.ToInt32(Console.ReadLine());
                var customer = ctx.Customers
                                        .Include(c => c.order)
                                        .Where(c => c.ID == chosenCustomer)
                                        .FirstOrDefault();
                
                if (customer.order.Count == 0)
                {
                    ctx.Customers.Remove(ctx.Customers.Find(chosenCustomer));
                    ctx.SaveChanges();
                    Console.WriteLine("\n" + customer.FullName + " Is deleted");
                }
                else
                {
                    customer.FullName = "Deleted"; customer.PassWord = "Deleted"; customer.PassReg = DateTime.Today; //Ändrar inte email för att den är unik och vill kunna kontakta kunden evenetuellt. 
                    ctx.SaveChanges();
                    Console.WriteLine("\n" + customer.FullName + " Is changed and inaktive");
                }
            }
        }

        

        // Lägg till valfri restaurang via console appen "tillfälligt" eftersom databasen återskapas varje körning
        public static void AddRestaurant()
        {
            using var ctx = new AdminDbContext();

            //Create and save a new restaurant
            Console.Write("Enter restaurant name: ");
            var restaurantName = Console.ReadLine();

            Console.Write("Enter a city for the restaurant: ");
            var city = Console.ReadLine();

            Console.Write("Enter a phone number to the restaurant: ");
            var phonenumber = Console.ReadLine();

            var newRestaurant = new Restaurant { RestaurantName = restaurantName, City = city, Phonenumber = phonenumber };
            ctx.Restaurants.Add(newRestaurant);
            ctx.SaveChanges();

            Console.WriteLine($"You have added a new Restaurant. Press any key to see the new list.");
            Console.ReadLine();

            Console.WriteLine("\nAll restaurants in the database:");
            foreach (var restaurants in ctx.Restaurants)
            {
                Console.WriteLine($"\nRestaurant: {restaurants.RestaurantName} " +
                                    $"\nCity: {restaurants.Phonenumber} " +
                                    $"\nPhonenumber: {restaurants.City} ");
            }            
        }

        //Här läggs restaurangen "William" till
        public static void AddSpecificRestaurant()
        {
            AdminBackend.ListRestaurants();

            Console.WriteLine("Here you see the liste, press any key to add a new Restaurant. WillamsFood\n");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Console.Clear();

            using var ctx = new AdminDbContext();

            var restaurantNew = new Restaurant { RestaurantName = "WilliamsFood", City = "Älvängen", Phonenumber = "0303-548354" };
            ctx.Restaurants.Add(restaurantNew);

            ctx.SaveChanges();

            Console.WriteLine("\nAll restaurants in the database:");
            foreach (var restaurants in ctx.Restaurants)
            {
                Console.WriteLine($"Restaurant: {restaurants.RestaurantName}," +
                                  $"\nCity: {restaurants.City}, " +
                                  $"\nhone Number: {restaurants.Phonenumber}\n");
            }           

        }

        public static void DeleteRestaurants(int restaurantID)
        {
            using var ctx = new AdminDbContext();

            Restaurant restaurant = ctx.Restaurants.Find(restaurantID);

            if (restaurant != null)
            {
                ctx.Restaurants.Remove(restaurant);

                ctx.SaveChanges();
            }
        }            

        public static Customer ChangeEmail(int customerID, string newEmail)
        {
            using var ctx = new AdminDbContext();
       
            var customer = ctx.Customers.Find(customerID);

            customer.Email = newEmail;
            ctx.SaveChanges();

            return customer;
                        
        }

        public static decimal TotalFoodRescueSales()
        {
            using var ctx = new AdminDbContext();
            
            var moneyQuery = ctx.FoodBoxes
                .Select(c => new
                {
                    p = c.Price,
                    oID = c.Order,
                })
                .Where(c => c.oID != null)
                .Sum(c => c.p);

            return moneyQuery;            
           
        }

    }
    
}