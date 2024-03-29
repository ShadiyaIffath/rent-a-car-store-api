﻿using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Repositories.Interfaces
{
    public interface IFraudClaimRepository
    {
        List<FraudClaim> GetClaims();

        bool ValidateLicenseExists(string drivingLicense);
    }
}
