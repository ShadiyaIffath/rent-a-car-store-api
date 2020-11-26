using AutoMapper;
using Microsoft.Extensions.Logging;
using Model.Entities;
using Model.Models;
using Model.Models.MailService;
using Model.Repositories.Interfaces;
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
        private IDMVRepository _dmvRepository;
        private IEquipmentRepository _equipmentRepository;
        private IVehicleBookingRepository _vehicleBookingRepository;
        private IVehicleRepository _vehicleRepository;
        private IAccountRepository _accountRepository;
        private readonly IMailService _mailService;

        public AccountService(IMapper mapper, IAccountRepository accountRepository, IJwtAuthenticationManager jwtAuthenticationManager,
            ILogger<AccountService> logger, IMailService mailService, IVehicleBookingRepository bookingRepository, IDMVRepository dmvRepository,
            IEquipmentRepository equipmentRepository, IVehicleRepository vehicleRepository )
        {
            _mapper = mapper;
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _accountRepository = accountRepository;
            _vehicleBookingRepository = bookingRepository;
            _vehicleRepository = vehicleRepository;
            _equipmentRepository = equipmentRepository;
            _dmvRepository = dmvRepository;
            _logger = logger;
            _mailService = mailService;
        }

        public string AuthenticateUser(LoginDto logincredentials)
        {
            Account loggedIn = _accountRepository.login(logincredentials.email, logincredentials.password);
            string token = null;

            if (loggedIn != null)
            {
                token = _jwtAuthenticationManager.Authenticate(logincredentials.email, loggedIn.type.type, loggedIn.id);
                _logger.LogInformation("User authenticated: " + loggedIn.id);
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
            _accountRepository.createCustomerAccount(account);
            account.DecryptModel();
            SendWelcomeEmail(account.email, account.firstName+" " + account.lastName);
            _logger.LogInformation("New User created");
        }

        public bool validateEmail(string email)
        {
            return _accountRepository.validateEmailInUse(email);
        }

        public bool validateLicense(string id)
        {
            return _dmvRepository.ValidIdExists(id);        
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

        public AccountDto GetAccountById(int id)
        {
            Account account = _accountRepository.GetAccountById(id);
            account.DecryptModel();
            return _mapper.Map<AccountDto>(account);
        }

        public bool UpdateAccount(AccountDto dto)
        {
            if (_accountRepository.CheckIfEmailIsUsed(dto.email, dto.id))
            {
                return false;
            }

            Account a = _mapper.Map<Account>(dto);
            a.EncryptModel();
            _accountRepository.Update(a);
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
            _accountRepository.UpdatePassword(id, password);
        }

        public DashboardCardsView GetCardDetails()
        {
            DashboardCardsView card = new DashboardCardsView();
            try{
                DateTime today = new DateTime();
                List<VehicleBooking> bookings = _vehicleBookingRepository.GetBookings() ;
               
                card.vehicleBookingDtos = _mapper.Map<List<VehicleBookingDto>>(_vehicleBookingRepository.GetBookingsWithinRange(today, today.AddDays(7)));

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

                List<VehicleDto> vehicles = _mapper.Map<List<VehicleDto>>(_vehicleRepository.GetVehicles());
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
