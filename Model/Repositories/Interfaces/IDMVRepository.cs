using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Repositories.Interfaces
{
    public interface IDMVRepository : IRepositoryBase<DMV>
    {
        public void SaveDMVList(List<DMV> dmv);

        public List<DMV> GetDMV();

        public bool ValidIdExists(string id);

        List<DMV> GetOffense(string id);
    }
}
