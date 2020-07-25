using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DatabaseContext;

namespace ProjectAPI.Controllers
{
    [Route("api/dummy")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        private ClientDbContext _clientDbContext;

        public DummyController(ClientDbContext clientDbContext)
        {
            _clientDbContext = clientDbContext;
        }

        [HttpGet]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }

    
}
