using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Model.Entities;
using Model.Models;
using Model.Repositories.Interfaces;
using ProjectAPI.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;

namespace ProjectAPI.Services
{
    public class WebScrapingService : IWebScrapingService
    {
        private ICarRatingRepository _carRatingRepository;
        private IConfiguration _iConfiguration;
        private readonly IMapper _mapper;
        private ILogger _logger;

        public WebScrapingService(ICarRatingRepository CarRatingRepository, IMapper mapper, ILogger<WebScrapingService> logger, IConfiguration iConfiguration)
        {
            _carRatingRepository = CarRatingRepository;
            _mapper = mapper;
            _logger = logger;
            _iConfiguration = iConfiguration;
        }

        public async Task CheckForUpdates(string key)
        {
            try
            {
                // We create the container for the data we want
                List<dynamic> adverts = new List<dynamic>();
                string url = _iConfiguration.GetValue<string>("RentalSites:" + key);

                await GetPageData(url, adverts);

                if (key.Equals("malkey"))
                {
                    _carRatingRepository.UpdateRating(adverts);
                }
            }catch(Exception ex)
            {
                _logger.LogError("Database was not updated due to :"+ex.Message);
            }
        }

        private async Task<List<dynamic>> GetPageData(string url, List<dynamic> results)
        {
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            CarRating advert = new CarRating();

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var carRatesHTMLView = htmlDocument.DocumentNode.Descendants("tbody").ToList();
            //.Where(node => node.GetAttributeValue("class", "")
            //.Equals("table selfdriverates")).ToList();

            string carType = "";
            var allRows = carRatesHTMLView[0].Descendants("tr").ToList();

            foreach (var row in allRows)
            {
                var category = row.Descendants("td")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("lightgray")).ToList();

                if (category.Count != 0)
                {
                    carType = category[0].InnerText.Trim('\n', '\t', '\r');
                }
                else
                {
                    var data = row.Descendants("td")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("text")).ToList();

                    if (data.Count == 4)
                    {
                        advert = new CarRating();
                        advert.CarCategory = carType;
                        advert.Model = data[0].InnerText;
                        advert.RatePerMonth = float.Parse(data[1].InnerText.Trim(','));
                        advert.RatePerWeek = float.Parse(data[2].InnerText.Trim(','));
                        advert.Milleage = float.Parse(data[3].InnerText.Trim(','));
                        results.Add(advert);
                        _logger.LogInformation("New Car Rating web scraped");
                    }
                }
            }
            return results;
        }

        public List<CarRatingDto> GetRatingDtos()
        {
            return _mapper.Map<List<CarRatingDto>>(_carRatingRepository.FindAll());
        }
    }
}
