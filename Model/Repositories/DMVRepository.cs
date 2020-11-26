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
    public class DMVRepository : RepositoryBase<DMV>, IDMVRepository
    {
        private ILogger _logger;
        public DMVRepository(ClientDbContext clientDbContext, ILogger<DMVRepository> logger) : base(clientDbContext)
        {
            _logger = logger;
        }

        public List<DMV> GetDMV()
        {
            return _clientDbContext.DMV.ToList();
        }

        public void SaveDMVList(List<DMV> dmv)
        {
            _clientDbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE [DMV]");
            foreach(var d in dmv){
                _clientDbContext.DMV.Add(d);
            }
            _clientDbContext.SaveChanges();
        }

        public bool ValidIdExists(string id)
        {
            int count = _clientDbContext.DMV.Where(x => x.drivingLicense == id).ToList().Count;
            if(count == 0)
            {
                return false;
            }
            return true;           
        }

        public List<DMV> GetOffense(string id)
        {
            return _clientDbContext.DMV.Where(x => x.drivingLicense == id).ToList();
        }
    }
}
