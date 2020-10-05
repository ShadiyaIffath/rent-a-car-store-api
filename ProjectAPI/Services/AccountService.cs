using AutoMapper;
using Microsoft.Extensions.Logging;
using Model.Entities;
using Model.Models;
using Model.Repositories.Interfaces;
using Newtonsoft.Json;
using ProjectAPI.Interfaces;
using ProjectAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace ProjectAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private ILogger _logger;
        private IJwtAuthenticationManager _jwtAuthenticationManager;
        private IAccountRepository _accountRepository;

        public AccountService(IMapper mapper, IAccountRepository accountRepository, IJwtAuthenticationManager jwtAuthenticationManager, ILogger<AccountService> logger)
        {
            _mapper = mapper;
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public string AuthenticateUser(LoginDto logincredentials)
        {
            string loggedIn = _accountRepository.login(logincredentials.email, logincredentials.password);
            string token = null;

            if (loggedIn != null)
            {
                int id = _accountRepository.getAccountId(logincredentials.email);
                token = _jwtAuthenticationManager.Authenticate(logincredentials.email, loggedIn, id);
                _logger.LogInformation("User authenticated: " + id);
            }
            return token;
        }
        public bool RegisterUser(CreateCustomerDto customerDto)
        {
            bool registered = false;
            if (_accountRepository.validateEmailInUse(customerDto.email))
            {
                return registered;
            }

            Account account = new Account();

            account = _mapper.Map<Account>(customerDto);
            ImageFile drivingLicense = JsonConvert.DeserializeObject<ImageFile>(customerDto.drivingLicense.ToString());
            account.drivingLicense = Convert.FromBase64String(drivingLicense.value);

            ImageFile additionalIdentification = JsonConvert.DeserializeObject<ImageFile>(customerDto.additionalIdentification.ToString());
            account.additionalIdentitfication = Convert.FromBase64String(additionalIdentification.value);
            _accountRepository.createCustomerAccount(account);
            _logger.LogInformation("New User created");
            registered = true;
            return registered;
        }

        public List<AccountDto> GetAccounts()
        {
            return _mapper.Map<List<AccountDto>>((_accountRepository.getAccounts()));
        }

        public void DeleteAccountById(int id)
        {
            _accountRepository.DeleteById(id);
        }

        public void UpdateAccountStatus(int id, bool status)
        {
            _accountRepository.UpdateAccountStatus(id, status);
        }
    }
}
