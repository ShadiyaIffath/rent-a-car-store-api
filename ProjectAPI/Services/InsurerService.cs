using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Repositories.Interfaces;
using ProjectAPI.Services.Interfaces;

namespace ProjectAPI.Services
{
    public class InsurerService : IInsurerService
    {
        private IFraudClaimRepository _fraudClaimRepository;

        public InsurerService(IFraudClaimRepository fraudClaimRepository)
        {
            _fraudClaimRepository = fraudClaimRepository;
        }

        public bool ValidateLicenseExists(string license)
        {
            return _fraudClaimRepository.ValidateLicenseExists(license);
        }

        public List<FraudClaim> GetFraudClaims()
        {
            return _fraudClaimRepository.GetClaims();
        }
    }
}
