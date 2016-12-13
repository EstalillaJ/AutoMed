using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AutoMed.Models.DataModels;

namespace AutoMed.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<AutoMedUser> AutoMedUsers { get; set; }

    }
}