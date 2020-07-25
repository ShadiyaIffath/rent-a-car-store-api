using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Models;
using Model.Repositories;
using Model.Repositories.Interfaces;
using Newtonsoft.Json;
using ProjectAPI.Interfaces;

namespace ProjectAPI.Controllers
{
    [Authorize]
    [Route("api/account")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IAccountRepository _accountRepository;
        private IJwtAuthenticationManager _jwtAuthenticationManager;
        private readonly IMapper _mapper;
        public AccountsController(IAccountRepository accountRepository, IJwtAuthenticationManager authenticationManager,
            IMapper mapper)
        {
            _accountRepository = accountRepository;
            _jwtAuthenticationManager = authenticationManager;
            _mapper = mapper;
        }
        // GET: api/<AccountsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AccountsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]LoginDto logincredentials)
        {
            
            string loggedIn = _accountRepository.login(logincredentials.email, logincredentials.password);

            if (loggedIn != null)
            {

                var token =  _jwtAuthenticationManager.Authenticate(logincredentials.email, loggedIn);

                if(token == null)
                {
                    return Unauthorized();
                }
                return Ok(token);
            }
            return Unauthorized();
        }



        [AllowAnonymous]
        [HttpPost("signup"), DisableRequestSizeLimit]
        public IActionResult RegisterCustomer([FromBody]CreateCustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_accountRepository.validateEmailInUse(customerDto.email))
            {
                return Ok("Email is in use");
            }
            
            Account account = new Account();
            try
            {
                account = _mapper.Map<Account>(customerDto);
                ImageFile drivingLicense = JsonConvert.DeserializeObject<ImageFile>(customerDto.drivingLicense.ToString());
                account.drivingLicense = Convert.FromBase64String(drivingLicense.value);

                ImageFile additionalIdentification = JsonConvert.DeserializeObject<ImageFile>(customerDto.additionalIdentification.ToString());
                account.additionalIdentitfication = Convert.FromBase64String(additionalIdentification.value);
            }
            catch (Exception)
            {
                return BadRequest("Invalid driving license or identificcation");
            }

             _accountRepository.createCustomerAccount(account);

            return Ok();
        }

        [HttpPost("register")]
        public IActionResult RegisterAdmin([FromBody]CreateAdminDto adminDto) 
        {
            return Ok();
        }
    }
}
