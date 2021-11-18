using System;
using DataLayer.Backend;
using DataLayer.Data;

namespace ConsoleApp
{
    /*
    Jag har i detta projektet valt att göra det som ett consolprogram, hade kunnat göra det med frontendclasser för att göra det mer generiskt. 
    Detta har jag inte hunnit göra ännu, men tycker detta för mig är en lagom nivå just nu.      
    */


    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n------------------------------------");
                Console.WriteLine(" Datalagring del 2 - Joakim Engström");
                Console.WriteLine("------------------------------------");
                Console.WriteLine($" 1: Customers");
                Console.WriteLine($"\n 2: Restaurants");
                Console.WriteLine($"\n 3: Admin");
                Console.WriteLine($"\n 4: Exit");
                Console.WriteLine("------------------------------------");
                Console.Write(" Choose option : ");

                string option = Console.ReadLine();

                Console.Clear();

                while (option == "1")
                {
                    Console.WriteLine("\n------------------------------------");
                    Console.WriteLine($"Customers" +
                                        $"\n------------------------------------" +
                                        $"\n 1: List of FoodBoxes left to buy " +
                                        $"\n 2: List of sold FoodBoxes " +
                                        $"\n 3: List FoodCategory " +
                                        $"\n 4: Buy Foodbox " +
                                        $"\n 5: CustomerHistory " +
                                        $"\n 6: Return " +
                                        $"\n------------------------------------");


                    Console.Write(" Choose option : ");
                    string customerOptions = Console.ReadLine();
                    Console.Clear();

                    if (customerOptions == "1") { UserBackend.ListFoodBoxesLeftToBuy(); returnNote(); }
                    if (customerOptions == "2") { UserBackend.ListSoldFoodBoxes(); returnNote(); }
                    if (customerOptions == "3") { UserBackend.ShowFoodType(); returnNote(); }
                    if (customerOptions == "4") { UserBackend.BuyFoodBox(); returnNote(); }
                    if (customerOptions == "5") { UserBackend.CustomerHistory(); returnNote(); }
                    if (customerOptions == "6") { break; }
                }

                while (option == "2")
                {
                    Console.WriteLine("\n------------------------------------");
                    Console.WriteLine($"Restaurant" +
                                        $"\n------------------------------------" +
                                        $"\n 1: List sales of Bohus Pizza " +
                                        $"\n 2: Delete old FoodBox" +
                                        $"\n 3: Change price on FoodBoxes " +
                                        $"\n 4: Add New FoodBoxes " +
                                        $"\n 5: Return " +
                                        $"\n------------------------------------");

                    Console.Write(" Choose option : ");
                    string restaurantOptions = Console.ReadLine();
                    Console.Clear();


                    if (restaurantOptions == "1") { RestaurantBackend.ListSpecificResaurantSales(); returnNote(); }
                    if (restaurantOptions == "2") { RestaurantBackend.DeleteOldFoodBox(); returnNote(); }
                    if (restaurantOptions == "3") { RestaurantBackend.ChangePriceOnFoodBox(); returnNote(); } //Kolla genom och skriv om!
                    if (restaurantOptions == "4") { RestaurantBackend.AddMoreFoodBoxes(); returnNote(); }
                    if (restaurantOptions == "5") { break; }

                }

                while (option == "3")
                {
                    AdminBackend adminBackend = new AdminBackend();
                    Console.WriteLine("\n------------------------------------");
                    Console.WriteLine($"Admin" +
                                        $"\n------------------------------------" +
                                        $"\n 1: Reset Database \"OBS! dangerous\" " +
                                        $"\n 2: List Restaurants " +
                                        $"\n 3: Add Restaurants " +
                                        $"\n 4: Delete Restaurants " +
                                        $"\n 5: Add Specific Restaurant " +
                                        $"\n 6: List Customers " +
                                        $"\n 7: Delete Customer " +
                                        $"\n 8: Change Email " +
                                        $"\n 9: Total sales from FoodRescue " +
                                        $"\n 10: Return " +
                                        $"\n------------------------------------");

                    Console.Write(" Choose option : ");
                    string adminOptions = Console.ReadLine();
                    Console.Clear();

                    if (adminOptions == "1") { AdminBackend.PrepDatabase(); returnNote(); }
                    if (adminOptions == "2") { ListVisuals.ResturantListUI(); returnNote(); }
                    if (adminOptions == "3") { AddRestaurantUI(); returnNote(); }
                    if (adminOptions == "4") { DeleteResturantUI(); returnNote(); }
                    if (adminOptions == "5") { SpecificRestaurantAdd(); returnNote(); }
                    if (adminOptions == "6") { ListVisuals.CustomerListUI(); returnNote(); }
                    if (adminOptions == "7") { DeleteCustomerUI(); returnNote(); }
                    if (adminOptions == "8") { ChangeEmailUI(); returnNote(); }
                    if (adminOptions == "9") { Console.WriteLine($"Total sales from FoodRescue: { adminBackend.TotalFoodRescueSales() }:-"); ; returnNote(); }
                    if (adminOptions == "10") { break; }

                }

                while (option == "4")
                {
                    Console.WriteLine(" Are you sure you want to exit?: Press y: ");
                    Console.WriteLine(" Want to return?: Press any key! \n");
                    Console.Write(" Choose option : ");

                    option = Console.ReadLine();

                    if (option == "y")
                    {
                        Console.WriteLine("\n Thank for using FoodRescue!");
                        returnNote();
                        return;
                    }

                    break;

                }
            }
        }

        // Lägg till valfri restaurang via console appen "tillfälligt" eftersom databasen återskapas varje körning
        public static void AddRestaurantUI()
        {
            using var ctx = new AdminDbContext();

            //Create and save a new restaurant
            Console.Write("Enter restaurant name: ");
            var restaurantName = Console.ReadLine();

            Console.Write("Enter a city for the restaurant: ");
            var city = Console.ReadLine();

            Console.Write("Enter a phone number to the restaurant: ");
            var phonenumber = Console.ReadLine();

            AdminBackend adminBackend = new AdminBackend();

            adminBackend.AddRestaurant(restaurantName, city, phonenumber);
            
            
            Console.WriteLine($"You have added a new Restaurant. Press any key to see the new list.");
            Console.ReadLine();

            Console.WriteLine("\nAll restaurants in the database:");

            foreach (var restaurants in AdminBackend.ListRestaurants())
            {
                Console.WriteLine($"\nRestaurant: {restaurants.RestaurantName} " +
                                    $"\nCity: {restaurants.Phonenumber} " +
                                    $"\nPhonenumber: {restaurants.City} ");
            }
        }

        public static void SpecificRestaurantAdd()
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

        public static void DeleteResturantUI()
        {
            foreach (var restaurants in AdminBackend.ListRestaurants())
            {
                Console.WriteLine(
                    $"ID: {restaurants.RestaurantID } / {restaurants.RestaurantName}  ");
            }

            Console.WriteLine("\n Want to continue and delete a restaurant?");
            Console.Write(" Press y to continue : ");

            string option = Console.ReadLine().ToLower();

            if (option == "y")
            {
                Console.Write("\n Pick a restaurant to delete, typ ID: ");

                int restaurantID = Convert.ToInt32(Console.ReadLine());

                AdminBackend.DeleteRestaurants(restaurantID);

                foreach (var restaurants in AdminBackend.ListRestaurants())
                {
                    Console.WriteLine(
                        $"\n ID: {restaurants.RestaurantID } / {restaurants.RestaurantName}  ");
                }
            }
        }

        public static void ChangeEmailUI()
        {
            foreach (var customer in AdminBackend.ListCustomers())
            {
                Console.WriteLine(
                    $" ID: {customer.ID} - Customer: {customer.FullName}");
            }

            Console.Write(" Choose user by ID: ");

            var customerID = Convert.ToInt32(Console.ReadLine());

            Console.Write(" Type in new email: \n");

            string newEmail = Console.ReadLine();

            var uppdatedCustomer = AdminBackend.ChangeEmail(customerID, newEmail);

            Console.WriteLine($" Customer: " + uppdatedCustomer.FullName + ", " +
                                $"\n New email: " + uppdatedCustomer.Email + "\n");
        }

        private static void returnNote()
        {
            Console.WriteLine("\n Press any key to exit...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void DeleteCustomerUI()
        {
            using var ctx = new AdminDbContext();
            AdminBackend adminBackend = new AdminBackend();

            ListVisuals.adminCustomerList();

            Console.WriteLine("\nHave to delete a customer without orders. ");
            Console.WriteLine("\nWant to continue? Press y to continue ");
            string option = Console.ReadLine();

            if (option == "y")
            {
                Console.WriteLine("\nType in ID to delete: \n"); //Vet inte varför men har inget username på mina customers ännu, lägger till det om jag får till det i tid.

                int chosenCustomer = Convert.ToInt32(Console.ReadLine());

                var customer = adminBackend.DeleteCustomer(chosenCustomer);

                if (customer == null)
                {
                    Console.WriteLine("\n Customer Is deleted");

                    ListVisuals.adminCustomerList();

                }


                else
                {
                    Console.WriteLine("\n Customer changed and inaktive");

                    ListVisuals.adminCustomerList();

                }

            }

        }

        
    }
}

            