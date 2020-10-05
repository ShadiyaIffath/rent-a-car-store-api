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
    public class EquipmentRepository : RepositoryBase<Equipment>, IEquipmentRepository
    {
        private ILogger _logger;
        public EquipmentRepository(ClientDbContext clientDbContext, ILogger<EquipmentRepository> logger) : base(clientDbContext)
        {
            _logger = logger;
        }

        public void CreateEquipmentCategory(EquipmentCategory equipmentCategory)
        {
            _clientDbContext.EquipmentCategories.Add(equipmentCategory);
            _clientDbContext.SaveChanges();
            _logger.LogInformation("Category created");
        }

        public List<Equipment> GetEquipment()
        {
            return _clientDbContext.Equipments.Include(e => e.category).ToList();
        }

        public List<EquipmentCategory> GetEquipmentCategories()
        {
            return _clientDbContext.EquipmentCategories.ToList();
        }

        public bool ValidateNameInUse(string name)
        {
            Equipment e = _clientDbContext.Equipments.Where(x => x.name == name).FirstOrDefault();
            if(e == null)
            {
                return false;
            }
            return true;
        }

        public Equipment GetEquipmentById(int id)
        {
            return _clientDbContext.Equipments.Where(x => x.id == id).Include(i => i.category).First();
        }

        public bool ValidateNameInUse(string name, int id)
        {
            Equipment e = _clientDbContext.Equipments.Where(x => x.name == name && x.id != id).FirstOrDefault();
            if (e == null)
            {
                return false;
            }
            return true;
        }

        public void DeleteById(int id)
        {
            _clientDbContext.Equipments.RemoveRange(_clientDbContext.Equipments.Where(x => x.id == id));
            _clientDbContext.SaveChanges();
            _logger.LogInformation("Equipment deleted successfully");
        }
    }
}
