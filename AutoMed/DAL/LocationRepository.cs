using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AutoMed.Models;

namespace AutoMed.DAL
{
    public class LocationRepository : ILocationRepository
    {
        public LocationRepository()
        {

        }

        public void AddLocation(Location location)
        {
            using (ApplicationContext context = new ApplicationContext())
            {   
                context.Locations.Add(location);
                context.SaveChanges();
            }
        }

        public void DeleteLocation(Location location)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Locations.Remove(context.Locations.Find(location.Id));
                context.SaveChanges();
            }
        }

        public void UpdateLocation(Location location)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Entry(location).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public List<Location> SelectAll()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Locations.ToList();
            }
        }
    }
}