using AutoMapper;
using Microsoft.Extensions.Logging;
using Model.Entities;
using Model.Models;
using Model.Models.MailService;
using Model.Repositories.Interfaces;
using Model.Repositories.RepositoryFactory;
using Newtonsoft.Json;
using ProjectAPI.Interfaces;
using ProjectAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Permissions;
using System.Threading.Tasks;
using UtilityLibrary.Utils;

namespace ProjectAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private ILogger _logger;
        private IJwtAuthenticationManager _jwtAuthenticationManager;
        private IRepositoryFactory _repositoryFactory;
        private IFraudClaimRepository _fraudClaimRepository;
        private readonly IMailService _mailService;

        public AccountService(IMapper mapper,IJwtAuthenticationManager jwtAuthenticationManager, ILogger<AccountService> logger, IMailService mailService,
            IFraudClaimRepository fraudClaimRepository, IRepositoryFactory repositoryFactory)
        {
            _mapper = mapper;
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _logger = logger;
            _mailService = mailService;
            _fraudClaimRepository = fraudClaimRepository;
            _repositoryFactory = repositoryFactory;
        }

        public string AuthenticateUser(LoginDto logincredentials)
        {
            string token = null;
            try
            {
                Account loggedIn = _repositoryFactory.AccountRepository.login(logincredentials.email, logincredentials.password);


                if (loggedIn != null)
                {
                    token = _jwtAuthenticationManager.Authenticate(logincredentials.email, loggedIn.type.type, loggedIn.id);
                    _logger.LogInformation("User authenticated: " + loggedIn.id);
                }
            }catch(Exception ex)
            {
                _logger.Log(LogLevel.Warning, ex.Message);
            }
            return token;
        }
        public void RegisterUser(CreateCustomerDto customerDto)
        {
            Account account = new Account();
            account = _mapper.Map<Account>(customerDto);
            ImageFile drivingLicense = JsonConvert.DeserializeObject<ImageFile>(customerDto.drivingLicense.ToString());
            account.drivingLicense = Convert.FromBase64String(drivingLicense.value);

            ImageFile additionalIdentification = JsonConvert.DeserializeObject<ImageFile>(customerDto.additionalIdentification.ToString());
            account.additionalIdentitfication = Convert.FromBase64String(additionalIdentification.value);
            _repositoryFactory.AccountRepository.createCustomerAccount(account);
            account.DecryptModel();
            SendWelcomeEmail(account.email, account.firstName+" " + account.lastName);
            _logger.LogInformation("New User created");
        }

        public bool validateEmail(string email)
        {
            return _repositoryFactory.AccountRepository.validateEmailInUse(email);
        }

        public bool validateLicense(string id)
        {
            return _repositoryFactory.DMVRepository.ValidIdExists(id);        
        }

        public bool validateFraudLicense(string license)
        {
            return _fraudClaimRepository.ValidateLicenseExists(license);
        }

        public List<AccountDto> GetAccounts()
        {
            return _mapper.Map<List<AccountDto>>((_repositoryFactory.AccountRepository.getAccounts()));
        }

        public void DeleteAccountById(int id)
        {
            _repositoryFactory.AccountRepository.DeleteById(id);
            _logger.LogInformation("Account deleted successfully");
        }

        public void UpdateAccountStatus(int id, bool status)
        {
            _repositoryFactory.AccountRepository.UpdateAccountStatus(id, status);
            _logger.LogInformation("Account status updated");
        }

        public AccountDto GetAccountById(int id)
        {
            Account account = _repositoryFactory.AccountRepository.GetAccountById(id);
            account.DecryptModel();
            return _mapper.Map<AccountDto>(account);
        }

        public bool UpdateAccount(AccountDto dto)
        {
            if (_repositoryFactory.AccountRepository.CheckIfEmailIsUsed(dto.email, dto.id))
            {
                return false;
            }

            Account a = _mapper.Map<Account>(dto);
            a.EncryptModel();
            _repositoryFactory.AccountRepository.Update(a);
            a.DecryptModel();
            SendProfileUpdatedMail(a.email, a.firstName + " " + a.lastName);
            _logger.LogInformation("Account successfully updated");
            return true;
        }

        public string PasswordConfirmation(AccountDto dto)
        {
            string code = ConfirmationCode.RandomString();
            _mailService.SendPasswordUpdateConfirmation(dto.email, dto.firstName + " " + dto.lastName, code);
            return code;
        }

        public void UpdateAccountPassword(int id, string password)
        {
            password = EncryptUtil.EncryptString(password);
            _repositoryFactory.AccountRepository.UpdatePassword(id, password);
            _logger.LogInformation("Account password changed");
        }

        public DashboardCardsView GetCardDetails()
        {
            DashboardCardsView card = new DashboardCardsView();
            try{
                DateTime today = DateTime.Today;
                List<VehicleBooking> bookings = _repositoryFactory.VehicleBookingRepository.GetBookings() ;
               
                card.vehicleBookings = _mapper.Map<List<VehicleBookingDto>>(_repositoryFactory.VehicleBookingRepository.GetBookingsWithinRange(today, today.AddDays(7)));
                card.bookings = card.vehicleBookings.Count;

                int completed, confirmed, cancelled, collected;
                completed = collected = cancelled = confirmed = 0;

                foreach (var v in bookings)
                {
                    switch (v.status)
                    {
                        case "Completed":
                            completed++;
                            break;
                        case "Cancelled":
                            cancelled++;
                            break;
                        case "Confirmed":
                            confirmed++;
                            break;
                        case "Collected":
                            collected++;
                            break;
                    }
                }
                card.cancelledBookings = cancelled;
                card.collectedBookings = collected;
                card.completedBookins = completed;
                card.confirmedBookings = confirmed;

                card.accounts = GetAccounts();

                List<VehicleDto> vehicles = _mapper.Map<List<VehicleDto>>(_repositoryFactory.VehicleRepository.GetVehicles());
                card.totalVehicles = vehicles.Count;

                int a, b, c, d, e;
                a = b = c = d = e = 0;

                foreach(var v in vehicles)
                {
                    switch (v.type.id)
                    {
                        case 4:
                            a++;
                            break;
                        case 5:
                            b++;
                            break;
                        case 6:
                            c++;
                            break;
                        case 7:
                            d++;
                            break;
                        case 8:
                            e++;
                            break;
                 
                    }
                }
                card.smallTownCar = a;
                card.hatchback = b;
                card.saloon = c;
                card.estate = d;
                card.vans = e;
            }catch(Exception ex)
            {
                _logger.LogError("Dashboard card view failed: " + ex.Message);
                throw ex;
            }
           
            return card;
        }

        private void SendWelcomeEmail(string email, string recipient)
        {
            WelcomeRequest request = new WelcomeRequest()
            {
                ToEmail = email,
                UserName = recipient
            };

            _mailService.SendWelcomeEmailAsync(request);
        }

        private void SendProfileUpdatedMail(string email, string recipient)
        {
            ProfileUpdated updated = new ProfileUpdated()
            {
                ToEmail = email,
                UserName = recipient,
                Today = DateTime.Today.ToString("dd/MM/yyyy HH:mm:ss")
            };

            _mailService.SendProfileUpdated(updated);
        }
    }
}
