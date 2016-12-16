using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AutoMed.Models.DataModels;
using AutoMed.Models;

namespace AutoMed.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<AutoMedUser> AutoMedUsers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    }

}