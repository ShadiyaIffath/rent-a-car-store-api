using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.DatabaseContext;
using Model.Entities;
using Model.Repositories.Base;
using Model.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Repositories
{
    public class VehicleRepository : RepositoryBase<Vehicle>, IVehicleRepository
    {
        private ILogger _logger;

        public VehicleRepository(ClientDbContext clientDbContext, ILogger<VehicleRepository> logger) : base(clientDbContext)
        {
            _logger = logger;
        }

        public List<Vehicle> GetVehicles()
        {
            return _clientDbContext.Vehicles.Include( v => v.type).ToList();   
        }
        public void SaveVehicleType(VehicleType type)
        {
            try
            {
                _clientDbContext.VehicleTypes.Add(type);
                _clientDbContext.SaveChanges();

            }catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
            }
        }

        public List<VehicleType> GetVehicleTypes()
        {
            List<VehicleType> types = new List<VehicleType>();

            try
            {
                types = _clientDbContext.VehicleTypes.ToList();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
            }
            return types;
        }
    }
}
