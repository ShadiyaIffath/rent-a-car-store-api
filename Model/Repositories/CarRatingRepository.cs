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
    public class CarRatingRepository : RepositoryBase<CarRating>, ICarRatingRepository
    {
        public CarRatingRepository(ClientDbContext clientDbContext) : base(clientDbContext)
        {
        }

        public CarRating FindByModel(string model)
        {
            return _clientDbContext.CarRatings.SingleOrDefault(row => row.Model == model);
        }

        public void UpdateRating(List<dynamic> carRatings)
        {
            var dbcontents = FindAll();

            // Loop over the database contents
            foreach (CarRating rating in dbcontents)
            {
                // If a rating can no longer be found we remove the row and 
                var advertExists = carRatings.Find(item => item.Model == rating.Model);
                if (advertExists == null)
                {
                    Delete(rating);
                }
            }

            // Loop over fresh data
            foreach (CarRating rating in carRatings)
            {
                // We check if the advert exists in the database
                var existingadvert = FindByModel(rating.Model);
                if (existingadvert != null)
                {
                    // The advert exists, then we check if the price is still the same,
                    if (existingadvert.RatePerMonth != rating.RatePerMonth)
                    {
                        existingadvert.RatePerMonth = rating.RatePerMonth;
                        _clientDbContext.SaveChanges();
                    }
                    continue;
                }
                Create(rating);
            }
        }
    }
}
