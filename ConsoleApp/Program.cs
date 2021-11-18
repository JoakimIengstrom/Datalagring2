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
                Console.WriteLine(  "Datalagring part 2 \n");
                Console.WriteLine(  "Choose option");                  
                Console.WriteLine(  $"\n 1: Customers");
                Console.WriteLine(  $"\n 2: Restaurants");
                Console.WriteLine(  $"\n 3: Admin");
                Console.WriteLine(  $"\n 4: Exit");

                Console.Write("\n #: ");
                string option = Console.ReadLine();

                Console.Clear();
                Console.WriteLine();
                
                while (option == "1")
                {
                    Console.WriteLine();
                    Console.WriteLine(  $"Customers" +
                                        $"\n1: List of FoodBoxes left to buy //skall vara med" +
                                        $"\n2: List of sold FoodBoxes " +
                                        $"\n3: List FoodCategory " +
                                        $"\n4: Buy Foodbox //skall vara med" +
                                        $"\n5: CustomerHistory " +
                                        $"\n6: Return ");

                    Console.Write("\n¤: ");
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
                    Console.WriteLine(  $"Restaurant" +
                                        $"\n1: List sales of Bohus Pizza //skall vara med" +
                                        $"\n2: Delete old FoodBox" +
                                        $"\n3: Change price on FoodBoxes " +
                                        $"\n4: Add New FoodBoxes //skall vara med" +
                                        $"\n5: Return ");

                    Console.Write("\n¤: ");
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

                    Console.WriteLine($"Admin" +
                                        $"\n1: Reset Database \"OBS! dangerous\" //skall vara med" +
                                        $"\n2: List Restaurants //skall vara med" +
                                        $"\n3: Add Restaurants //skall vara med" +
                                        $"\n4: Delete Restaurants " +
                                        $"\n5: Add Specific Restaurant " +
                                        $"\n6: List Customers //skall vara med" +
                                        $"\n7: Delete Customer //skall vara med" +
                                        $"\n8: Change Email " +
                                        $"\n9: Total sales from FoodRescue " +
                                        $"\n10: Return ");

                    Console.Write("\n¤: ");
                    string adminOptions = Console.ReadLine();
                    Console.Clear();

                    if (adminOptions == "1") { AdminBackend.PrepDatabase(); returnNote(); }
                    if (adminOptions == "2") { ListVisuals.ResturantListUI(); returnNote(); }
                    if (adminOptions == "3") { AdminBackend.AddRestaurant(); returnNote(); }
                    if (adminOptions == "4") { DeleteResturantUI(); returnNote(); }
                    if (adminOptions == "5") { AdminBackend.AddSpecificRestaurant(); returnNote(); }
                    if (adminOptions == "6") { ListVisuals.CustomerListUI(); returnNote(); }
                    if (adminOptions == "7") { AdminBackend.DeleteCustomer(); returnNote(); }
                    if (adminOptions == "8") { ChangeEmailUI(); returnNote(); }
                    if (adminOptions == "9") { Console.WriteLine($"Total sales from FoodRescue: { AdminBackend.TotalFoodRescueSales() }:-"); ; returnNote(); }
                    if (adminOptions == "10") { break; }

                }

                while (option == "4")
                {
                    Console.WriteLine("Are you sure you want to exit?: Press y: ");
                    Console.WriteLine("Want to return?: Press any key! \n");
                    option = Console.ReadLine();

                    if (option == "y")
                    {
                        Console.WriteLine("Thank for using FoodRescue!");
                        Program.returnNote();   
                        return;
                    }                    
                    
                    break;

                }
            }
        }

        public static void SpecificRestaurantAdd()
        {
            foreach (var restaurants in AdminBackend.ListRestaurants())
            {
                Console.WriteLine("Here you see the liste, press any key to add a new Restaurant. WillamsFood\n");
                returnNote();
            }

            


            foreach (var restaurants in AdminBackend.ListRestaurants())
            {
                Console.WriteLine($"Restaurant: {restaurants.RestaurantName}," +
                                  $"\nCity: {restaurants.City}, " +
                                  $"\nhone Number: {restaurants.Phonenumber}\n");
            }

        }










        public static void DeleteResturantUI()
        {
            foreach (var restaurants in AdminBackend.ListRestaurants())
            {
                Console.WriteLine(
                    $"ID: {restaurants.RestaurantID } / {restaurants.RestaurantName}  ");
            }

            Console.WriteLine("\nWant to continue? Press y to continue \n");

            string option = Console.ReadLine().ToLower();

            if (option == "y")
            {
                Console.Write("\nPick a restaurant to delete, typ ID: ");

                int restaurantID = Convert.ToInt32(Console.ReadLine());
                                
                foreach (var restaurants in AdminBackend.ListRestaurants())
                {
                    Console.WriteLine(
                        $"\nID: {restaurants.RestaurantID } / {restaurants.RestaurantName}  ");
                }
            }            
        }

        public static void ChangeEmailUI()
        {
            foreach (var customer in AdminBackend.ListCustomers())
            {
                Console.WriteLine(
                    $"ID: {customer.ID} - Customer: {customer.FullName}");
            }

            Console.Write("Choose user by ID: \n");

            var customerID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Type in new email: \n");

            string newEmail = Console.ReadLine();

            var uppdatedCustomer = AdminBackend.ChangeEmail(customerID, newEmail);
              
            Console.WriteLine($"Customer: " + uppdatedCustomer.FullName + ", " +
                                $"\nNew email: " + uppdatedCustomer.Email + "\n");
        }

        private static void returnNote()
        {
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
            Console.Clear();
        }

        
    }
}

            