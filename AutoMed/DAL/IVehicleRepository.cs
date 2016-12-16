using AutoMed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMed.DAL
{
    public interface IVehicleRepository
    {
        void Create(string Vin, string Make, string Model, int Year, string Color, string LicensePlateNumber);
        IEnumerable<Vehicle> GetVehicles();
        void InsertVehicle(Vehicle vehicle);
        void DeleteVehicle(int vehicleId);
        void UpdateVehicle(Vehicle vehicle);
    }
}