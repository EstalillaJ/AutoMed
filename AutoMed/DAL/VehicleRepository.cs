using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMed.Models;

namespace AutoMed.DAL
{
    public class VehicleRepository : IVehicleRepository
    {
        private ApplicationContext Context;
        public VehicleRepository(ApplicationContext context)
        {
            this.Context = context;
        }
        public void Create(string Vin, string Make, string Model, int Year, string Color, string LicensePlateNumber)
        {
            Vehicle vehicle = new Vehicle
            {
                Vin = Vin,
                Make = Make,
                Model = Model,
                Year = Year,
                Color = Color,
                LicensePlateNumber = LicensePlateNumber
            };
            Context.Vehicles.Add(vehicle);
            Context.SaveChanges();
        }
        public IEnumerable<Vehicle> GetVehicles()
        {
            return Context.Vehicles.ToList();
        }
        public void InsertVehicle(Vehicle vehicle)
        {
            Context.Vehicles.Add(vehicle);
        }
        public void DeleteVehicle(int vehicleId)
        {
            Vehicle vehicle = Context.Vehicles.Find(vehicleId);
            Context.Vehicles.Remove(vehicle);
        }
        public void UpdateVehicle(Vehicle vehicle)
        {
            Context.Entry(vehicle).State = System.Data.Entity.EntityState.Modified;
        }
    }
}