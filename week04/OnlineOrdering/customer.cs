using System;

namespace ProductOrderingSystem
{
    public class Customer
    {
        // Private member variables
        private string _name;
        private Address _address;

        // Constructor
        public Customer(string name, Address address)
        {
            _name = name;
            _address = address;
        }

        // Getter for Name (needed for the shipping label)
        public string GetName()
        {
            return _name;
        }

        // Method that delegates the USA location check to the Address class
        public bool LivesInUsa()
        {
            return _address.IsInUsa();
        }

        // Method to get the formatted address string from the Address class
        public string GetFormattedAddress()
        {
            return _address.GetFullAddressString();
        }
    }
}