
using System;

namespace ProductOrderingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // =========================================================
            // ORDER 1: Domestic Customer (Nigeria)
            // =========================================================
            Address address1 = new Address("123 Digital Drive", "Seattle", "WA", "USA");
            Customer customer1 = new Customer("Jackson Vance", address1);
            Order order1 = new Order(customer1);

            // Add products to Order 1
            order1.AddProduct(new Product("Mechanical Keyboard", "KEY-882", 89.99, 1));
            order1.AddProduct(new Product("Ergonomic Mouse", "MSE-401", 45.50, 2));
            order1.AddProduct(new Product("USB-C Cable (6ft)", "CBL-012", 9.99, 3));

            // =========================================================
            // ORDER 2: International Customer (Canada)
            // =========================================================
            Address address2 = new Address("456 Maple Leaf Sq", "Toronto", "ON", "Canada");
            Customer customer2 = new Customer("Bob Harrison", address2);
            Order order2 = new Order(customer2);

            // Add products to Order 2
            order2.AddProduct(new Product("4K UltraWide Monitor", "MON-991", 349.99, 1));
            order2.AddProduct(new Product("Noise Canceling Headphones", "AUD-550", 120.00, 1));


            // =========================================================
            // DISPLAY RESULTS
            // =========================================================
            Console.WriteLine("==================================================");
            Console.WriteLine("             ORDER PROCESSING REPORT              ");
            Console.WriteLine("==================================================\n");

            // Display Order 1
            Console.WriteLine(">>> PROCESSING ORDER #1 <<<");
            Console.WriteLine(order1.GetShippingLabel());
            Console.WriteLine(order1.GetPackingLabel());
            Console.WriteLine($"Total Invoice Cost: ${order1.CalculateTotalCost():F2}");
            Console.WriteLine("\n--------------------------------------------------\n");

            // Display Order 2
            Console.WriteLine(">>> PROCESSING ORDER #2 <<<");
            Console.WriteLine(order2.GetShippingLabel());
            Console.WriteLine(order2.GetPackingLabel());
            Console.WriteLine($"Total Invoice Cost: ${order2.CalculateTotalCost():F2}");
            Console.WriteLine("==================================================");
        }
    }
}