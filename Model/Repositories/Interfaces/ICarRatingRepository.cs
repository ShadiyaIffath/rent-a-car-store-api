using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Repositories.Interfaces
{
    public interface ICarRatingRepository : IRepositoryBase<CarRating>
    {
        CarRating FindByModel(string model);

        void UpdateRating(List<dynamic> carRatings);
    }
}
