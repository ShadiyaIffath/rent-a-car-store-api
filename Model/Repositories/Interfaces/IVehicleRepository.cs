using Model.Entities;
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
    }
}
