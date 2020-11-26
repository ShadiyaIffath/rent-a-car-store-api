using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Models;

namespace ProjectAPI.Services.Interfaces
{
    public interface IDMVService
    {
        public Task GetLicenses();

        public List<DMVDto> GetDMVDtos();

        public bool validateLicenseWithDMV(string licenseId);

        Task<bool> DMVNotification(int accountId, string licenseId);
    }
}
