using AutoMed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMed.DAL
{
    public interface IVehicleRepository
    {
        void Create(Vehicle vehicle);
        List<Vehicle> SearchVehicleByLicensePlate(string LicensePlate);
        void InsertVehicle(Vehicle vehicle);
        void DeleteVehicle(int vehicleId);
        void UpdateVehicle(Vehicle vehicle);
    }
}