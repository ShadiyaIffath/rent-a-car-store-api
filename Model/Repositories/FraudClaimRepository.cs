using Microsoft.EntityFrameworkCore;
using Model.DatabaseContext;
using Model.Entities;
using Model.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Model.Repositories
{
    public class FraudClaimRepository : IFraudClaimRepository
    {
        private InsuranceDbContext _insuranceDbContext;
        private ClientDbContext _clientDbContext;

        public FraudClaimRepository(InsuranceDbContext dbContext, ClientDbContext clientDbContext)
        {
            this._clientDbContext = clientDbContext;
            this._insuranceDbContext = dbContext;
        }

        public List<FraudClaim> GetClaims()
        {
            List<string> accounts = _clientDbContext.Accounts.Select(x => x.licenseId).ToList();
            return _insuranceDbContext.Claims.Where(x => accounts.Contains(x.DrivingLicense)).ToList();
        }

        public bool ValidateLicenseExists(string drivingLicense)
        {
            var claims = _insuranceDbContext.Claims.Where(x => x.DrivingLicense == drivingLicense);
            if (claims == null)
            {
                return false;
            }
            return true;
        }
    }
}
