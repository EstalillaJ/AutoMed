using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMed.Models;
namespace AutoMed.DAL
{
    public class CustomerRepository : ICustomerRepository
    {
        public CustomerRepository()
        {

        }
        public void Create(Customer customer)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                Context.Customers.Add(customer);
                Context.SaveChanges();
            }
        }
        public List<Customer> FindCustomerByName(string nameString)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                return Context.Customers.Where(x => (x.FirstName + x.LastName).Contains(nameString)).ToList(); 
            }
        }
        public Customer FindCustomerById(int customerId)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                return Context.Customers.Find(customerId);
            }
        }
        public IEnumerable<Customer> GetCustomers()
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                return Context.Customers.ToList();
            }
        }
        public void InsertCustomer(Customer customer)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                Context.Customers.Add(customer);
            }
        }
        public void DeleteCustomer(int customerId)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                Customer customer = Context.Customers.Find(customerId);
                Context.Customers.Remove(customer);
            }
        }
        public void UpdateCustomer(Customer customer)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                Context.Entry(customer).State = System.Data.Entity.EntityState.Modified;
            }
        }
    }
}