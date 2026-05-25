using System;
using System.Collections.Generic;
using System.Text;

namespace ProductOrderingSystem
{
    public class Order
    {
        // Private member variables
        private List<Product> _products;
        private Customer _customer;

        // Constructor
        public Order(Customer customer)
        {
            _customer = customer;
            _products = new List<Product>();
        }

        // Method to add a product to the order
        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        // Method to calculate total order price including conditional shipping
        public double CalculateTotalCost()
        {
            double totalProductCost = 0;
            
            foreach (Product product in _products)
            {
                totalProductCost += product.GetTotalCost();
            }

            // Apply conditional shipping flat rates
            double shippingCost = _customer.LivesInUsa() ? 5.00 : 35.00;

            return totalProductCost + shippingCost;
        }

        // Method to generate the Packing Label
        public string GetPackingLabel()
        {
            StringBuilder label = new StringBuilder();
            label.AppendLine("--- PACKING LABEL ---");
            foreach (Product product in _products)
            {
                label.AppendLine($"Item: {product.GetName()} [ID: {product.GetProductId()}] x {product.GetQuantity()}");
            }
            return label.ToString();
        }

        // Method to generate the Shipping Label
        public string GetShippingLabel()
        {
            StringBuilder label = new StringBuilder();
            label.AppendLine("--- SHIPPING LABEL ---");
            label.AppendLine(_customer.GetName());
            label.AppendLine(_customer.GetFormattedAddress());
            return label.ToString();
        }
    }
}