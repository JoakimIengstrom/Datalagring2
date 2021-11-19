using DataLayer.Backend;
using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ListVisuals
    {
        //Några metorder som visar olika delar av databasen med lite olika formateringar, beroende på syftet. 

        public static void ResturantListUI()
        {
            foreach (var restaurant in AdminBackend.ListRestaurants())
            {
                Console.WriteLine(  $"ID: {restaurant.RestaurantID} " +
                                    $"\nRestaurant: {restaurant.RestaurantName} " +
                                    $"\nCity: {restaurant.Phonenumber} " +
                                    $"\nPhonenumber: {restaurant.City} ");
            }
        }

        public static void CustomerListUI()
        {
            foreach (var customer in AdminBackend.ListCustomers())
            {
                Console.WriteLine(  $"Name: {customer.FullName}, Password: {customer.PassWord}, Email: {customer.Email}");
            }
        }

        public static void adminCustomerList()
        {
            foreach (var customer in AdminBackend.ListCustomers())
            {
                Console.WriteLine(  $"\n ID: {customer.ID} / Customer: {customer.FullName}");
            }
        }

        public static void ListSpecificResaurantSalesUI()
        {
            using var ctx = new AdminDbContext();

            foreach (var salesFromRestaurant in RestaurantBackend.ListSpecificResaurantSales())
            {
                Console.WriteLine(
                                    $" Restaurant: { salesFromRestaurant.Restaurant.RestaurantName }" +
                                    $", Customer name: {salesFromRestaurant.Order.Customer.FullName}" +
                                    $", Food Box: {salesFromRestaurant.BoxName}" +
                                    $", Deliviery made: {salesFromRestaurant.Order.DeliveryDate.ToShortDateString()}");
            }
        }
    }
}
