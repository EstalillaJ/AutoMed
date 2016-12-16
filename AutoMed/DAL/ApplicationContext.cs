using System.Data.Entity;
using AutoMed.Models.DataModels;
using AutoMed.Models;

namespace AutoMed.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<AutoMedUserDataModel> AutoMedUsers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<QuoteDataModel> QuoteDataModels { get; set; }
	    public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}