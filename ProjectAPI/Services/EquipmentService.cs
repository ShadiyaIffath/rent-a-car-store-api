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
    public class EquipmentService : IEquipmentService
    {
        private IEquipmentRepository _equipmentRepository;
        private readonly IMapper _mapper;
        private ILogger _logger;

        public EquipmentService(IMapper mapper, IEquipmentRepository equipmentRepository, ILogger<EquipmentService> logger)
        {
            _equipmentRepository = equipmentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public bool CreateEquipment(CreateEquipmentDto createEquipment)
        {
            bool success = false;
            if (_equipmentRepository.ValidateNameInUse(createEquipment.name))
            {
                return success;
            }
            else
            {
                Equipment e = _mapper.Map<Equipment>(createEquipment);
                _equipmentRepository.Create(e);
                _logger.LogInformation("Equipment created successfully");
            }
            return success;
        }

        public List<EquipmentCategoryDto> GetEquipmentCategories()
        {
            return _mapper.Map<List<EquipmentCategoryDto>>(_equipmentRepository.GetEquipmentCategories());
        }

        public List<EquipmentDto> GetEquipment()
        {
            return _mapper.Map<List<EquipmentDto>>(_equipmentRepository.GetEquipment());
        }

        public void CreateEquipmentCategory(CreateCategoryDto createCategory)
        {
            EquipmentCategory category = _mapper.Map<EquipmentCategory>(createCategory);
            ImageFile image = JsonConvert.DeserializeObject<ImageFile>(createCategory.image.ToString());
            category.image = Convert.FromBase64String(image.value);
            _equipmentRepository.CreateEquipmentCategory(category);
            _logger.LogInformation("Equipment category created successfully");
        }
    }
}
