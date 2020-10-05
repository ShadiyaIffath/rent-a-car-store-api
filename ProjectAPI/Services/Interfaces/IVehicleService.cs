using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Services.Interfaces
{
    public interface IVehicleService
    {
        VehicleTypeDto GetVehicleTypeById(int id);

        List<VehicleDto> GetAllVehicles();

        void CreateVehicleType(CreateVehicleTypeDto vehicleTypeDto);

        List<VehicleTypeDto> GetVehicleTypes();

        void AddVehicle(CreateVehicleDto createVehicleDto);

        VehicleDto GetVehicleById(int id);

        void UpdateVehicle(UpdateVehicleDto updateVehicleDto);

        void UpdateVehicleStatus(UpdateStatusVehicleDto updateVehicleDto);

        void DeleteVehicleById(int id);
    }
}
