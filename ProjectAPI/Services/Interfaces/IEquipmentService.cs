using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Services.Interfaces
{
    public interface IEquipmentService
    {
        public Boolean CreateEquipment(CreateEquipmentDto createEquipment);

        public List<EquipmentCategoryDto> GetEquipmentCategories();

        public List<EquipmentDto> GetEquipment();

        public void CreateEquipmentCategory(CreateCategoryDto createCategory);

        public EquipmentDto GetEquipmentById(int id);

        public bool UpdateEquipment(EquipmentDto equipmentDto);

        public void DeleteEquipment(int id);
    }
}
