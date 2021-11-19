using DataLayer.Backend;
using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class AddThingsToLists
    {
        // Lägg till valfri restaurang via console appen "tillfälligt" eftersom databasen återskapas varje körning
        public static void AddRestaurantUI()
        {
            using var ctx = new AdminDbContext();

            //Create and save a new restaurant
            Console.Write(" Enter restaurant name: ");
            var restaurantName = Console.ReadLine();

            Console.Write(" Enter a city for the restaurant: ");
            var city = Console.ReadLine();

            Console.Write(" Enter a phone number to the restaurant: ");
            var phonenumber = Console.ReadLine();

            AdminBackend adminBackend = new AdminBackend();

            adminBackend.AddRestaurant(restaurantName, city, phonenumber);

            Console.WriteLine($"You have added a new Restaurant. Press any key to see the new list.");
            Console.ReadLine();

            Console.WriteLine("\nAll restaurants in the database:");

            foreach (var restaurants in AdminBackend.ListRestaurants())
            {
                Console.WriteLine($"\n Restaurant: {restaurants.RestaurantName} " +
                                    $"\n City: {restaurants.Phonenumber} " +
                                    $"\n Phonenumber: {restaurants.City} ");
            }
        }

        public static void SpecificRestaurantAddUI()
        {
            foreach (var restaurants in AdminBackend.ListRestaurants())
            {
                Console.WriteLine($" Restaurant: {restaurants.RestaurantName}," +
                                  $"\n City: {restaurants.City}, " +
                                  $"\n Phone Number: {restaurants.Phonenumber}\n");

            }

            Console.WriteLine(" Here you see the liste, press any key to add a new Restaurant. WillamsFood\n");
            Console.ReadKey();

            AdminBackend.AddSpecificRestaurant("WilliamsFood", "Älvängen", "0303-548354");

            foreach (var restaurants in AdminBackend.ListRestaurants())
            {
                Console.WriteLine($" Restaurant: {restaurants.RestaurantName}," +
                                  $"\n City: {restaurants.City}, " +
                                  $"\n Phone Number: {restaurants.Phonenumber}\n");
            }
        }
    }
}
