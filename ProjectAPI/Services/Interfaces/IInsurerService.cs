using System;
using System.Collections.Generic;
using System.Linq;
using Model.Entities;
using System.Threading.Tasks;

namespace ProjectAPI.Services.Interfaces
{
    public interface IInsurerService
    {
        bool ValidateLicenseExists(string license);

        List<FraudClaim> GetFraudClaims();
    }
}
