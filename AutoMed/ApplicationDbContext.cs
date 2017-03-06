using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using AutoMed.Models;
using AutoMed.Models.DataModels;

namespace AutoMed.DAL
{
    public class ApplicationDbContext : IdentityDbContext<AutoMedUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {   //  Vehicles will still get deleted via cascade delete from the customers quotes
            modelBuilder.Entity<Vehicle>().HasRequired(v => v.Owner).WithMany(c => c.Vehicles).WillCascadeOnDelete(false);
            modelBuilder.Entity<Quote>().HasMany(x => x.Documents).WithRequired(x => x.Quote).WillCascadeOnDelete(true);
            modelBuilder.Entity<IncomeBracket>().HasKey(x => new { x.ScaleId, x.Income, x.NumInHousehold }).HasKey(x => x.Id);
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
        public DbSet<IncomeBracket> IncomeBrackets { get; set; }
        public DbSet<BracketMapping> BracketMappings { get; set; }
        public DbSet<Scale> Scales { get; set; }
    }
}