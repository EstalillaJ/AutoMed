using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMed.Models;
namespace AutoMed.DAL
{
    public class CustomerRepository : ICustomerRepository
    {
        private ApplicationContext Context;
        public CustomerRepository(ApplicationContext context)
        {
            this.Context = context;
        }
        public void Create(string FirstName, string LastName, string Address, string Email, string PhoneNumber, int Age, Gender Gender)
        {
            Customer customer = new Customer
            {
                FirstName = FirstName,
                LastName = LastName,
                Address = Address,  
                Email = Email,
                PhoneNumber = PhoneNumber,
                Age = Age,
                Gender = Gender
            };
            Context.Customers.Add(customer);
            Context.SaveChanges();
        }
        public IEnumerable<Customer> GetCustomers()
        {
            return Context.Customers.ToList();
        }
        public void InsertCustomer(Customer customer)
        {
            Context.Customers.Add(customer);
        }
        public void DeleteCustomer(int customerId)
        {
            Customer customer = Context.Customers.Find(customerId);
            Context.Customers.Remove(customer);
        }
        public void UpdateCustomer(Customer customer)
        {
            Context.Entry(customer).State = System.Data.Entity.EntityState.Modified;
        }
    }
}