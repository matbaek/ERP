using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RawProduct
    {
        //Property
        public string Name { get; set; }
        public double Weight { get; set; }
        public DateTime DateOfPurchase { get; set; }

        //Metods
        public bool AddRawProduct(string name, double weight, DateTime dateOfPurchase)
        {
            return false;
        }

        public bool EditRawProduct(string name, double weight, DateTime dateOfPurchase)
        {
            return false;
        }

        public bool DeleteRawProduct()
        {
            return false;
        }

        public bool DisplayRawProducts()
        {
            return false;
        }

        public bool DisplayRawProduct()
        {
            return false;
        }
    }
}
