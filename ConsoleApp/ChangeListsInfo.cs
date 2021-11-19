using DataLayer.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ChangeListsInfo
    {
        public static void ChangeEmailUI()
        {
            foreach (var customer in AdminBackend.ListCustomers())
            {
                Console.WriteLine($" ID: {customer.ID} - Customer: {customer.FullName}");
            }

            Console.Write(" Choose user by ID: ");

            var customerID = Convert.ToInt32(Console.ReadLine());

            Console.Write(" Type in new email: \n");

            string newEmail = Console.ReadLine();

            var uppdatedCustomer = AdminBackend.ChangeEmail(customerID, newEmail);

            Console.WriteLine($" Customer: " + uppdatedCustomer.FullName + ", " +
                                $"\n New email: " + uppdatedCustomer.Email + "\n");
        }

        public static void DeleteCustomerUI()
        {
            AdminBackend adminBackend = new AdminBackend();

            ListVisuals.adminCustomerList();

            Console.WriteLine("\n Have to delete a customer without orders. ");
            Console.Write("\n Want to continue? Press y to continue: ");
            string option = Console.ReadLine();

            if (option == "y")
            {
                Console.Write("\n Type in ID to delete: ");

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
                    Console.WriteLine($"\n ID: {restaurants.RestaurantID } / {restaurants.RestaurantName}  ");
                }
            }
        }
    }
}
