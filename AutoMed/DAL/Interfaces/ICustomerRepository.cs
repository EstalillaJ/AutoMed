using AutoMed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMed.DAL
{
    public interface ICustomerRepository
    {
        void Create(Customer customer);
        List<Customer> FindCustomerByName(string nameString);
        Customer FindCustomerById(int customerId);
        IEnumerable<Customer> GetCustomers();
        void InsertCustomer(Customer customer);
        void DeleteCustomer(int customerId);
        void UpdateCustomer(Customer customer);
    }
}