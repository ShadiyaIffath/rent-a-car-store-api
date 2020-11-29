using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.DatabaseContext;
using Model.Entities;
using Model.Models;
using Model.Repositories.Base;
using Model.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Model.Repositories
{
    public class VehicleRepository : RepositoryBase<Vehicle>, IVehicleRepository
    {

        public VehicleRepository(ClientDbContext clientDbContext) : base(clientDbContext)
        {
        }

        public List<Vehicle> GetVehicles()
        {
            return _clientDbContext.Vehicles.Include( v => v.type).ToList();   
        }
        public void SaveVehicleType(VehicleType type)
        {
            _clientDbContext.VehicleTypes.Add(type);
            _clientDbContext.SaveChanges();
        }

        public List<VehicleType> GetVehicleTypes()
        {
            List<VehicleType> types = new List<VehicleType>();

            types = _clientDbContext.VehicleTypes.ToList();

            return types;
        }
        public void UpdateVehicleStatus(UpdateStatusVehicleDto statusVehicleDto)
        {

            Vehicle vehicle = _clientDbContext.Vehicles.Where(x => x.id == statusVehicleDto.id).First();

            if (vehicle == null)
            {
                throw new NullReferenceException();
            }
            vehicle.active = statusVehicleDto.active;
            vehicle.dayRemoved = statusVehicleDto.dayRemoved;
            _clientDbContext.SaveChanges();
        }

        public Vehicle GetVehicleById(int id)
        {
            return _clientDbContext.Vehicles.Where(x => x.id == id).Include(i=> i.type).First();
        }

        public VehicleType GetVehicleTypeById(int id)
        {
            return _clientDbContext.VehicleTypes.First(x => x.id == id);
        }

        public void DeleteById(int id)
        {
            _clientDbContext.Vehicles.RemoveRange(_clientDbContext.Vehicles.Where(x => x.id == id));
            _clientDbContext.SaveChanges();
        }
    }
}
