using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class CustomerRepository
    {
        private List<Customer> customers = new List<Customer>();
        private Customer customer = new Customer();

        //Metods
        public bool AddCustomer(string companyName, string productOwner, string address, string zip, string town, string telephone)
        {
            return false;
        }

        public bool EditCustomer(string companyName, string productOwner, string address, string zip, string town, string telephone)
        {
            return false;
        }

        public bool DeleteCustomer()
        {
            return false;
        }

        public List<Customer> DisplayCustomers()
        {
            return customers = customer.GetCustomers();
        }

        public bool DisplayCustomer()
        {
            return false;
        }
    }
}
