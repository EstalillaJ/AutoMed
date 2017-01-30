using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using AutoMed.Models;

namespace AutoMed.DAL
{
    public class ApplicationDbContext : IdentityDbContext<AutoMedUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().HasRequired(v => v.Owner).WithMany(c => c.Vehicles).WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Location> Locations { get; set; }

    }
}