using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Customer
    {
        //Property
        public string CompanyName { get; set; }
        public string ProductOwner { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string Town{ get; set; }
        public string Telephone{ get; set; }

        //Metods
        public bool AddCustomer(string companyName, string productOwner, string address, 
                                string zip, string town, string telephone)
        {
            return false;
        }

        public bool EditCustomer(string companyName, string productOwner, string address,
                                string zip, string town, string telephone)
        {
            return false;
        }

        public bool DeleteCustomer()
        {
            return false;
        }

        public bool DisplayCustomers()
        {
            return false;
        }

        public bool DisplayCustomer()
        {
            return false;
        }
    }
}
