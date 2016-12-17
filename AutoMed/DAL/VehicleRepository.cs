using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMed.Models;
namespace AutoMed.DAL
{
    public class VehicleRepository : IVehicleRepository
    {
        public VehicleRepository(ApplicationContext context)
        {

        }
        public void Create(Vehicle vehicle)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                Context.Vehicles.Add(vehicle);
                Context.SaveChanges();
            }
        }
        public List<Vehicle> SearchVehicleByLicensePlate(string LicensePlate)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                return Context.Vehicles.Where(x => x.LicensePlate == LicensePlate).ToList();
            }
        }
        public void InsertVehicle(Vehicle vehicle)
        {
            using (ApplicationContext Context = new ApplicationContext())
            { 
                Context.Vehicles.Add(vehicle);
                vehicle.OwnerId = vehicle.Owner.Id;
            }
        }
        public void DeleteVehicle(int vehicleId)
        {
            using (ApplicationContext Context = new ApplicationContext())
            {
                Vehicle vehicle = Context.Vehicles.Find(vehicleId);
                Context.Vehicles.Remove(vehicle);
            }
        }
        public void UpdateVehicle(Vehicle vehicle)
        {
            using (ApplicationContext Context = new ApplicationContext())
            { 
                Context.Entry(vehicle).State = System.Data.Entity.EntityState.Modified;
            }
        }
    }
}