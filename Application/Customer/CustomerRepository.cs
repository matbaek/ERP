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
        private Customer customer = new Customer();

        //Metods
        public void AddCustomer(Customer customer)
        {
            customer.AddCustomer(customer.CompanyName, customer.CustomerAddress, customer.CustomerTelephone, customer.CustomerZip, customer.CustomerTown, customer.CustomerEmail);
        }

        public void EditCustomer(Customer customer)
        {
            customer.EditCustomer(customer.CustomerID, customer.CompanyName, customer.CustomerAddress, customer.CustomerTelephone, customer.CustomerZip, customer.CustomerTown, customer.CustomerEmail);
        }

        public void DeleteCustomer(int customerID)
        {
            customer.DeleteCustomer(customerID);
        }

        public List<Customer> DisplayCustomers()
        {
            return customer.GetCustomers();
        }

        public List<Customer> DisplaySpecificCustomers(Customer customer)
        {
            return customer.GetSpecificCustomers(customer.CompanyName, customer.CustomerAddress, customer.CustomerTelephone, customer.CustomerZip, customer.CustomerTown, customer.CustomerEmail);
        }

        public Customer DisplayCustomer(int customerID)
        {
            return customer.GetCustomer(customerID);
        }
    }
}
