using System;
using System.Linq;
using DataLayer.Data;
using DataLayer.Model;


namespace DataLayer.Backend
{
    public class UserBackend
    {
        // visar alla köpta matlådor
        public static void ListSoldFoodBoxes()
        {
            using var ctx = new AdminDbContext();

            var querys = ctx.FoodBoxes
                .Select(c => new
                {
                    bn = c.BoxName,
                    oID = c.Order,
                    dd = c.Order.DeliveryDate,
                    fn = c.Order.Customer.FullName,
                    rn = c.Restaurant.RestaurantName,

                })
                .Where(c => c.oID != null);

            foreach (var f in querys)
            {
                Console.WriteLine( $"Restaurant: {f.rn}, Customer name: {f.fn}, Food Box: {f.bn}, Deliver made: {f.dd}");
            }
        }

        // lista på alla osålda matlådor, sorterade på pris, lägst först (valde att sortera det per restaurant först och inom dem pris)

        public static void ListFoodBoxesLeftToBuy()
        {
            using var ctx = new AdminDbContext();
                         
            var querys = ctx.FoodBoxes
                .Select(c => new
                {
                    rn = c.Restaurant.RestaurantName,
                    bc = c.BoxCategory,
                    bn = c.BoxName,
                    p = c.Price,
                    oID = c.Order,
                })
                .OrderBy(c => c.rn)
                .ThenBy(c => c.p)
                .Where(c => c.oID == null);

            foreach (var f in querys)
            {

                Console.WriteLine($" Restaurant: {f.rn}, Category: {f.bc}, BoxName: {f.bn},  {f.p}:- ");
            }
        }
        
        // köp en angiven matlåda som är null(inte såld)
        public static void BuyFoodBox()
        {
            using var ctx = new AdminDbContext();

            foreach (var customer in ctx.Customers)
            {
                Console.WriteLine($"ID: {customer.ID}, Customer: {customer.FullName} ");
            }
            Console.Write("Choose cuter by ID: ");
            var customerBuy = ctx.Customers.Find(Convert.ToInt32(Console.ReadLine()));

            Console.Clear();

            Console.WriteLine("Welcome " + customerBuy.FullName + "!\n");
            Console.WriteLine("Here is your list of Restaurants\n");

            foreach (var restaurantList in ctx.Restaurants)
            {
                Console.WriteLine($" ID: {restaurantList.RestaurantID} - City: {restaurantList.City} - Restaurant: {restaurantList.RestaurantName} ");
            }

            while (true)
            {
                Console.Write("\n Pick a restaurant, type ID: ");
                var restaurant = ctx.Restaurants.Find(Convert.ToInt32(Console.ReadLine()));

                var foodQuerys = ctx.FoodBoxes
                    .Select(c => new
                    {
                        rn = c.Restaurant.RestaurantName,
                        bc = c.BoxCategory,
                        bn = c.BoxName,
                        bID = c.FoodBoxID,
                        rID = c.Restaurant.RestaurantID,
                        p = c.Price,
                        oID = c.Order,
                    })
                    .OrderBy(c => c.p).ThenBy(c => c.bc)
                    .Where(c => c.rID == restaurant.RestaurantID && c.oID == null);

                if (foodQuerys.Any())
                {
                    foreach (var f in foodQuerys)
                    {
                        Console.WriteLine($" ID: {f.bID}, Restaurant: {f.rn}, Category: {f.bc}, BoxName: {f.bn}, {f.p}:- ");
                    }

                    Console.Write("\nPick a box, type ID: ");
                    var foodBoxChoice = ctx.FoodBoxes.Find(Convert.ToInt32(Console.ReadLine()));

                    var order = new Order() { DeliveryDate = DateTime.Now, Customer = customerBuy };

                    foodBoxChoice.Order = order;

                    ctx.SaveChanges();

                    Console.Clear();
                    Console.WriteLine(customerBuy.FullName + " have bought a " + foodBoxChoice.BoxName + " from " + restaurant.RestaurantName + "!\n");

                    return;
                }
                else
                {
                    Console.WriteLine("\nYour choice have no boxes left today ");
                    Console.WriteLine("\nPress enter to choose fron list, or write exit to Exit. ");
                    Console.Write("Option: ");                    

                    string option = Console.ReadLine().ToLower();
                    if (option == "exit")
                    {
                        return;
                    }
                }
            }                     
        }          

        // visar all köphistorik för en specifik användare
        public static void CustomerHistory()
        {
            using var ctx = new AdminDbContext();

            foreach (var customer in ctx.Customers)
            {
                Console.WriteLine($" Name: {customer.FullName} ");
            }

            Console.WriteLine();    
            Console.Write("Write the name you want to check: ");
            var customerChoice = Console.ReadLine();                   
                        
            var querys = ctx.FoodBoxes
                .Select(c => new
                {
                    fp = c.Price,
                    bn = c.BoxName,
                    oID = c.Order,
                    dd = c.Order.DeliveryDate,
                    fn = c.Order.Customer.FullName,
                    rn = c.Restaurant.RestaurantName,
                })
                .Where(c => c.fn == customerChoice && c.oID != null);
            foreach (var f in querys)
            {

                Console.WriteLine(
                                    $"\nFullName: {f.fn}, " +
                                    $"\nFood Box: {f.bn}, " +
                                    $"\nDelivery date: {f.dd} " +
                                    $"\nfrom restaurant: {f.rn}, " +
                                    $"\nprice {f.fp}:- ");
            }
        }
        public static void ShowFoodType()
        {
            using var ctx = new AdminDbContext();

            // Lista vad det finns för sorts mat. 
            var querys = ctx.FoodBoxes
                .Select(c => new
                {
                    rn = c.Restaurant.RestaurantName,
                    bc = c.BoxCategory,
                    bn = c.BoxName,
                    p = c.Price,
                })
                .OrderBy(c => c.bc);
            foreach (var f in querys)
            {
                Console.WriteLine($" Category: {f.bc}, BoxName: {f.bn}, Restaurant: {f.rn}, {f.p}:- ");
            }            
        }
    }
}