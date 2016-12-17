using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMed.DAL;
using AutoMed.Models;
namespace AutoMed.Tests.DAL
{
    [TestClass]
    public class CustomerRepositoryTest
    {
        [ClassInitialize]
        public static void TestCreate()
        {
            Customer customer = new Customer();
            CustomerRepository customerRepo = new CustomerRepository();
            customerRepo.InsertCustomer(customer);
        }
    }
}
