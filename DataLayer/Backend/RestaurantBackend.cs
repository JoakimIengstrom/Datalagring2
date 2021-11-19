using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Data;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;


namespace DataLayer.Backend
{
    public class RestaurantBackend
    {
        // en metod för att få en lista över alla sålda matlådor för ett restaurang objekt
        
        public static List<FoodBox> ListSpecificResaurantSales()
        {
            using var ctx = new AdminDbContext();

            // visar alla köpta matlådor för restaurangen "Bohus Pizza"
            var salesFromRestaurant = ctx.FoodBoxes
                .Include(c => c.Order)
                .ThenInclude(c => c.Customer)
                .Include(c => c.Restaurant)                
                .Where(c => c.Restaurant.RestaurantName == "BohusPizza" && c.Order != null);

            return salesFromRestaurant.ToList();
            
        }

        // Låter personal på restaurangen radera valfri matlåda, tex en matlåda som gått ut och inte blivit såld.
        public static void DeleteOldFoodBox()
        {
            using var ctx = new AdminDbContext();

            var querys = ctx.FoodBoxes
                .Select(c => new
                {
                    bn = c.BoxName,
                    bID = c.FoodBoxID,
                    oID = c.Order,
                    bb = c.BestBefore,
                    rn = c.Restaurant.RestaurantName,
                })
                .Where(c => c.bb < DateTime.Today);

            Console.WriteLine("Check for old FoodBoxes:");
            foreach (var f in querys)
            {
                Console.WriteLine(
                    $"BestBefore: {f.bb.ToShortDateString()}, Restaurant: {f.rn}, Box name: {f.bn}, Food Box ID: {f.bID}, ");
            }


            Console.Write("\nWrite box ID to delete a FoodBox: ");
            ctx.FoodBoxes.Remove(ctx.FoodBoxes.Find(Convert.ToInt32(Console.ReadLine())));
            ctx.SaveChanges();            
        }

        public static void AddMoreFoodBoxes()
        {
            using var ctx = new AdminDbContext();

            AdminBackend.ListRestaurants();

            Console.Write("\nEnter restaurant name: ");
            var boxName = Console.ReadLine();

            Console.Write("Enter a city for the restaurant: ");
            var boxCategory = Console.ReadLine();

            Console.Write("Enter a phone number to the restaurant: ");
            var price = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter number of days it is for sale: ");
            var bestBefore = DateTime.Today + TimeSpan.FromDays(Convert.ToInt32(Console.ReadLine()));

            foreach (var restaurantList in ctx.Restaurants)
            {
                Console.WriteLine($"Restaurant: {restaurantList.RestaurantName} " +
                                    $"\nCity: {restaurantList.Phonenumber} " +
                                    $"\nPhonenumber: {restaurantList.City} \n");
            }

            Console.Write("Enter restaurant ID: ");
            var restaurantID = Convert.ToInt32(Console.ReadLine());
            var restaurant = ctx.Restaurants
                .Find(restaurantID);
               
            var newFoodBoxes = new FoodBox { BoxName = boxName, BoxCategory = boxCategory, Price = price, BestBefore = bestBefore, Restaurant = restaurant, Order = null };
            ctx.SaveChanges();
        }

        public static void ChangePriceOnFoodBox()
        {
            using var ctx = new AdminDbContext();

            AdminBackend.ListRestaurants();

            Console.Write("\nChoose the Restaurant you want to work with. ID: ");

            int resturantChoice = Convert.ToInt32(Console.ReadLine());
            var restaurant = ctx.Restaurants
                                        .Include(c => c.foodBox)
                                        .Where(c => c.RestaurantID == resturantChoice)
                                        .FirstOrDefault();            

            Console.WriteLine(" List of boxes in: " + restaurant.RestaurantName);
                        
            foreach (var x in restaurant.foodBox)
            {                
                Console.WriteLine("\n ID: " + x.FoodBoxID + " BoxName: " + x.BoxName + " Price: " + x.Price + ";- " + " BestBefore" + x.BestBefore);
            }

            Console.Write("\nFoodbox by ID: ");

            Console.Write("\nPick the box you want to change: ");
            var oldPrice = ctx.FoodBoxes.Find(Convert.ToInt32(Console.ReadLine()));
            
            Console.Write("Category: "+ oldPrice.BoxCategory + " Type: " + oldPrice.BoxName + ", " + "Price: " +
                          oldPrice.Price + ":-" + "\n");

            Console.WriteLine("\nPress enter to continue\n");
            Console.ReadKey();

            var price = ctx.FoodBoxes.Find(2);

            Console.Write("New price: ");
            price.Price = Convert.ToInt32(Console.ReadLine());
            ctx.SaveChanges();

            Console.Write("Food box: " + price.BoxName + ", " + "New price: " +
                          price.Price + ":-" + "\n" + "\n");
                       
        }
    }
}