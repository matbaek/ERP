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
        public void AddCustomer(Customer customer)
        {
            customer.AddCustomer(customer.CompanyName, customer.ContactPerson, customer.CustomerAddress, customer.CustomerTelephone, customer.CustomerZip, customer.CustomerTown);
        }

        public void EditCustomer(Customer customer)
        {
            customer.EditCustomer(customer.CustomerID, customer.CompanyName, customer.ContactPerson, customer.CustomerAddress, customer.CustomerTelephone, customer.CustomerZip, customer.CustomerTown);
        }

        public void DeleteCustomer(int customerID)
        {
            customer.DeleteCustomer(customerID);
        }

        public List<Customer> DisplayCustomers()
        {
            return customers = customer.GetCustomers();
        }

        public List<Customer> DisplaySpecificCustomers(Customer customer)
        {
            return customers = customer.GetSpecificCustomers(customer.CompanyName, customer.ContactPerson, customer.CustomerAddress, customer.CustomerTelephone, customer.CustomerZip, customer.CustomerTown);
        }

        public Customer DisplayCustomer(int customerID)
        {
            return customer.GetCustomer(customerID);
        }
    }
}
