using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Services.Interfaces
{
    public interface IWebScrapingService
    {
        Task CheckForUpdates(string url);

        List<CarRatingDto> GetRatingDtos();
    }
}
