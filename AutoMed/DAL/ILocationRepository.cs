using System.Collections.Generic;
using AutoMed.Models;

namespace AutoMed.DAL
{
    public interface ILocationRepository
    {
        void AddLocation(Location location);
        void DeleteLocation(Location location);
        List<Location> SelectAll();
        void UpdateLocation(Location location);
    }
}