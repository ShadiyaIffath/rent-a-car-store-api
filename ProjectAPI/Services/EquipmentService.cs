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
    public class EquipmentService : IEquipmentService
    {
        private readonly IMapper _mapper;
        private ILogger _logger;
        private IRepositoryFactory _repositoryFactory;

        public EquipmentService(IMapper mapper, IRepositoryFactory repositoryFactory, ILogger<EquipmentService> logger)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
            _logger = logger;
        }

        public bool CreateEquipment(CreateEquipmentDto createEquipment)
        {
            bool success = false;
            if (_repositoryFactory.EquipmentRepository.ValidateNameInUse(createEquipment.name))
            {
                return success;
            }
            else
            {
                Equipment e = _mapper.Map<Equipment>(createEquipment);
                _repositoryFactory.EquipmentRepository.Create(e);
                success = true;
                _logger.LogInformation("Equipment created successfully");
            }
            return success;
        }

        public List<EquipmentCategoryDto> GetEquipmentCategories()
        {
            return _mapper.Map<List<EquipmentCategoryDto>>(_repositoryFactory.EquipmentRepository.GetEquipmentCategories());
        }

        public List<EquipmentDto> GetEquipment()
        {
            return _mapper.Map<List<EquipmentDto>>(_repositoryFactory.EquipmentRepository.GetEquipment());
        }

        public void CreateEquipmentCategory(CreateCategoryDto createCategory)
        {
            EquipmentCategory category = _mapper.Map<EquipmentCategory>(createCategory);
            ImageFile image = JsonConvert.DeserializeObject<ImageFile>(createCategory.image.ToString());
            category.image = Convert.FromBase64String(image.value);
            _repositoryFactory.EquipmentRepository.CreateEquipmentCategory(category);
            _logger.LogInformation("Equipment category created successfully");
        }

        public EquipmentDto GetEquipmentById(int id)
        {
            return _mapper.Map<EquipmentDto>(_repositoryFactory.EquipmentRepository.GetEquipmentById(id));
        }

        public bool UpdateEquipment(EquipmentDto equipmentDto)
        {
            bool success = false;
            if (_repositoryFactory.EquipmentRepository.ValidateNameInUse(equipmentDto.name, equipmentDto.id))
            {
                return success;
            }
            else
            {
                Equipment e = _mapper.Map<Equipment>(equipmentDto);
                _repositoryFactory.EquipmentRepository.Update(e);
                success = true;
                _logger.LogInformation("Equipment created successfully");
            }
            return success;
        }

        public void DeleteEquipment(int id )
        {
            _repositoryFactory.EquipmentRepository.DeleteById(id);
            _logger.LogInformation("Equipment successfully deleted");
        }
    }
}
