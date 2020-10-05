using AutoMapper;
using Microsoft.Extensions.Logging;
using Model.Entities;
using Model.Models;
using Model.Repositories.Interfaces;
using Newtonsoft.Json;
using ProjectAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Services
{
    public class VehicleService : IVehicleService
    {
        private IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;
        private ILogger _logger;

        public VehicleService(IVehicleRepository vehicleRepository, IMapper mapper, ILogger<VehicleService> logger)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public VehicleTypeDto GetVehicleTypeById(int id)
        {
           return _mapper.Map<VehicleTypeDto>(_vehicleRepository.GetVehicleTypeById(id));
        }

        public List<VehicleDto> GetAllVehicles()
        {
            return _mapper.Map<List<VehicleDto>>(_vehicleRepository.GetVehicles());
        }

        public void CreateVehicleType(CreateVehicleTypeDto vehicleTypeDto)
        {
            _vehicleRepository.SaveVehicleType(_mapper.Map<VehicleType>(vehicleTypeDto));
            _logger.LogInformation("Vehicle type created");
        }

        public List<VehicleTypeDto> GetVehicleTypes()
        {
            return _mapper.Map<List<VehicleTypeDto>>(_vehicleRepository.GetVehicleTypes()); 
        }

        public void AddVehicle(CreateVehicleDto createVehicleDto)
        {
            Vehicle vehicle = _mapper.Map<Vehicle>(createVehicleDto);
            ImageFile image = JsonConvert.DeserializeObject<ImageFile>(createVehicleDto.image.ToString());
            vehicle.image = Convert.FromBase64String(image.value);
            _vehicleRepository.Create(vehicle);
        }

        public VehicleDto GetVehicleById(int id)
        {
            return _mapper.Map<VehicleDto>(_vehicleRepository.GetVehicleById(id));
        }

        public void UpdateVehicle(UpdateVehicleDto updateVehicleDto)
        {
            Vehicle vehicle = _mapper.Map<Vehicle>(updateVehicleDto);
            vehicle.image = Convert.FromBase64String(updateVehicleDto.image.ToString());
            _vehicleRepository.Update(vehicle);
        }

        public void UpdateVehicleStatus(UpdateStatusVehicleDto updateVehicleDto)
        {
            _vehicleRepository.UpdateVehicleStatus(updateVehicleDto);
        }

        public void DeleteVehicleById(int id)
        {
            _vehicleRepository.DeleteById(id);
        }
    }
}
