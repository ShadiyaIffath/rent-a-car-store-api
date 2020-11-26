using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngleSharp;
using AngleSharp.Html.Parser;

namespace ProjectAPI.Controllers
{

    [Route("/api/scrape")]
    [ApiController]
    public class WebScrapingController : Controller
    {
        private readonly String websiteUrl = "https://www.malkey.lk/rates/self-drive-rates.html";
        private readonly ILogger _logger;

        // Constructor
        public WebScrapingController(ILogger<WebScrapingController> logger)
        {
            _logger = logger;
        }
    }
}
