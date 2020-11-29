using AutoMapper;
using Microsoft.Extensions.Logging;
using Model.Entities;
using Model.Models;
using Model.Repositories.RepositoryFactory;
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
        private IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;
        private ILogger _logger;

        public VehicleService(IRepositoryFactory repositoryFactory, IMapper mapper, ILogger<VehicleService> logger)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
            _logger = logger;
        }

        public VehicleTypeDto GetVehicleTypeById(int id)
        {
           return _mapper.Map<VehicleTypeDto>(_repositoryFactory.VehicleRepository.GetVehicleTypeById(id));
        }

        public List<VehicleDto> GetAllVehicles()
        {
            return _mapper.Map<List<VehicleDto>>(_repositoryFactory.VehicleRepository.GetVehicles());
        }

        public void CreateVehicleType(CreateVehicleTypeDto vehicleTypeDto)
        {
            _repositoryFactory.VehicleRepository.SaveVehicleType(_mapper.Map<VehicleType>(vehicleTypeDto));
            _logger.LogInformation("Vehicle type created");
        }

        public List<VehicleTypeDto> GetVehicleTypes()
        {
            return _mapper.Map<List<VehicleTypeDto>>(_repositoryFactory.VehicleRepository.GetVehicleTypes()); 
        }

        public void AddVehicle(CreateVehicleDto createVehicleDto)
        {
            Vehicle vehicle = _mapper.Map<Vehicle>(createVehicleDto);
            ImageFile image = JsonConvert.DeserializeObject<ImageFile>(createVehicleDto.image.ToString());
            vehicle.image = Convert.FromBase64String(image.value);
            _repositoryFactory.VehicleRepository.Create(vehicle);
            _logger.LogInformation("Vehicle created");
        }

        public VehicleDto GetVehicleById(int id)
        {
            return _mapper.Map<VehicleDto>(_repositoryFactory.VehicleRepository.GetVehicleById(id));
        }

        public void UpdateVehicle(UpdateVehicleDto updateVehicleDto)
        {
            Vehicle vehicle = _mapper.Map<Vehicle>(updateVehicleDto);
            vehicle.image = Convert.FromBase64String(updateVehicleDto.image.ToString());
            _repositoryFactory.VehicleRepository.Update(vehicle);
            _logger.LogInformation("Vehicle Successfully updated");
        }

        public void UpdateVehicleStatus(UpdateStatusVehicleDto updateVehicleDto)
        {
            _repositoryFactory.VehicleRepository.UpdateVehicleStatus(updateVehicleDto);
            _logger.LogInformation("Vehicle updated");
        }

        public void DeleteVehicleById(int id)
        {
            _repositoryFactory.VehicleRepository.DeleteById(id);
            _logger.LogInformation("Vehicle deleted");
        }
    }
}
