using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Product
    {
        //Property
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public DateTime DateOfPackaging { get; set; }
        public DateTime DateOfExpiration { get; set; }

        //Metodes
        public bool AddProduct(string name, double weight, double price, double amount,
                                DateTime dateOfPackaging, DateTime dateOfExpiration)
        {
            return false;
        }

        public bool EditProduct(string name, double weight, double price, double amount,
                                DateTime dateOfPackaging, DateTime dateOfExpiration)
        {
            return false;
        }

        public bool DeleteProduct()
        {
            return false;
        }

        public bool DisplayProducts()
        {
            return false;
        }

        public bool DisplayProduct()
        {
            return false;
        }
    }
}