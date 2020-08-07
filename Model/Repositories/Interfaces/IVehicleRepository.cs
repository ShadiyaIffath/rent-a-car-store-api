using Model.Entities;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Repositories.Interfaces
{
    public interface IVehicleRepository:IRepositoryBase<Vehicle>
    {
        public List<Vehicle> GetVehicles();

        public void SaveVehicleType(VehicleType type);

        public List<VehicleType> GetVehicleTypes();

        public void UpdateVehicleStatus(UpdateStatusVehicleDto statusVehicleDto);

        public Vehicle GetVehicleById(int id);

        public VehicleType GetVehicleTypeById(int id);
    }
}
