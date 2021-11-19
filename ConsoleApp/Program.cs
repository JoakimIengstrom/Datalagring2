using System;
using DataLayer.Backend;
using DataLayer.Data;

namespace ConsoleApp
{    
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

                    if (customerOptions == "1") { boxesLeftToBuyUI(); returnNote(); }
                    if (customerOptions == "2") { listSoldBoxesUI(); returnNote(); }
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

                    if (restaurantOptions == "1") { ListVisuals.ListSpecificResaurantSalesUI(); returnNote(); }
                    if (restaurantOptions == "2") { RestaurantBackend.DeleteOldFoodBox(); returnNote(); }
                    if (restaurantOptions == "3") { RestaurantBackend.ChangePriceOnFoodBox(); returnNote(); }
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
                    if (adminOptions == "3") { AddThingsToLists.AddRestaurantUI(); returnNote(); }
                    if (adminOptions == "4") { ChangeListsInfo.DeleteResturantUI(); returnNote(); }
                    if (adminOptions == "5") { AddThingsToLists.SpecificRestaurantAddUI(); returnNote(); }
                    if (adminOptions == "6") { ListVisuals.CustomerListUI(); returnNote(); }
                    if (adminOptions == "7") { ChangeListsInfo.DeleteCustomerUI(); returnNote(); }
                    if (adminOptions == "8") { ChangeListsInfo.ChangeEmailUI(); returnNote(); }
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


        private static void boxesLeftToBuyUI()
        {
            UserBackend userBackend = new UserBackend();
            foreach (var foodBox in userBackend.ListFoodBoxesLeftToBuy())
            {
                Console.WriteLine($" Restaurant: {foodBox.Restaurant.RestaurantName}, Category: {foodBox.BoxCategory}, BoxName: {foodBox.BoxName},  {foodBox.Price}:- ");
            }
        }

        private static void listSoldBoxesUI()
        {
            UserBackend userBackend = new UserBackend();
            foreach (var foodbox in userBackend.ListSoldFoodBoxes())
            {
                Console.WriteLine($"Restaurant: {foodbox.Restaurant.RestaurantName}, Customer name: {foodbox.Order.Customer.FullName}, Food Box: {foodbox.BoxName}, Deliver made: {foodbox.Order.DeliveryDate}");
            }
        }

        private static void returnNote()
        {
            Console.WriteLine("\n Press any key to exit...");
            Console.ReadKey();
            Console.Clear();
        }

               
    }
}

            