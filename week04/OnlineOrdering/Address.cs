using System;

namespace ProductOrderingSystem
{
    public class Address
    {
        // Private member variables
        private string _streetAddress;
        private string _city;
        private string _stateOrProvince;
        private string _country;

        // Constructor
        public Address(string streetAddress, string city, string stateOrProvince, string country)
        {
            _streetAddress = streetAddress;
            _city = city;
            _stateOrProvince = stateOrProvince;
            _country = country;
        }

        // Method to determine if the address is in the USA
        public bool IsInUsa()
        {
            // Normalize string to handle case-insensitive comparisons
            string countryUpper = _country.ToUpper().Trim();
            return countryUpper == "USA" || countryUpper == "UNITED STATES" || countryUpper == "UNITED STATES OF AMERICA";
        }

        // Method to return the full address as a multi-line string
        public string GetFullAddressString()
        {
            return $"{_streetAddress}\n{_city}, {_stateOrProvince}\n{_country}";
        }
    }
}