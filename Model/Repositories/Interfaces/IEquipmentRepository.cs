﻿using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Repositories.Interfaces
{
    public interface IEquipmentRepository : IRepositoryBase<Equipment>
    {
        bool ValidateNameInUse(string name);
        List<EquipmentCategory> GetEquipmentCategories();

        List<Equipment> GetEquipment();

        void CreateEquipmentCategory(EquipmentCategory equipmentCategory);

        Equipment GetEquipmentById(int id);

        bool ValidateNameInUse(string name, int id);

        void DeleteById(int id);

        int CountEquipment();
    }
}
