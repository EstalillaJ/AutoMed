using AutoMed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMed.DAL
{
    public interface ICustomerRepository
    {
        void Create(string FirstName, string LastName, string Address, string Email, string PhoneNumber, int Age, Gender Gender);
        IEnumerable<Customer> GetCustomers();
        void InsertCustomer(Customer customer);
        void DeleteCustomer(int customerId);
        void UpdateCustomer(Customer customer);
    }
}