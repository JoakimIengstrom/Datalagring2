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

        public Customer DeleteCustomer(int chosenCustomerID)
        {
            using var ctx = new AdminDbContext();      
            
            var customer = ctx.Customers
                                        .Include(c => c.order)
                                        .Where(c => c.ID == chosenCustomerID)
                                        .FirstOrDefault();
                
            if (customer.order.Count == 0)
            {
                ctx.Customers.Remove(ctx.Customers.Find(chosenCustomerID));
                ctx.SaveChanges();
                return null;                    
            }

            else
            {
                customer.FullName = "Deleted"; customer.PassWord = "Deleted"; customer.PassReg = DateTime.Today;  
                ctx.SaveChanges();
                return customer;   
            }            
        }        

        // Lägg till valfri restaurang via console appen "tillfälligt" eftersom databasen återskapas varje körning
        public void AddRestaurant(string restaurantName, string city, string phonenumber)
        {
            using var ctx = new AdminDbContext();            

            var newRestaurant = new Restaurant { RestaurantName = restaurantName, City = city, Phonenumber = phonenumber };
            ctx.Restaurants.Add(newRestaurant);
            ctx.SaveChanges();                     
        }

        //Här läggs restaurangen "William" till
        public static void AddSpecificRestaurant(string restaurantName, string city, string phonenumber)
        {          
            using var ctx = new AdminDbContext();

            var newRestaurant = new Restaurant { RestaurantName = restaurantName, City = city, Phonenumber = phonenumber };
            ctx.Restaurants.Add(newRestaurant);            
            ctx.SaveChanges();  
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

        public decimal TotalFoodRescueSales()
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