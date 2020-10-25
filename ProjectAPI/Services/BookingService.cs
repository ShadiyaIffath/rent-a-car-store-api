using AutoMapper;
using Microsoft.Extensions.Logging;
using Model.Entities;
using Model.Models;
using Model.Repositories;
using Model.Repositories.Interfaces;
using ProjectAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtilityLibrary.Utils;

namespace ProjectAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private ILogger _logger;
        private IVehicleBookingRepository _vehicleBookingRepository;
        private IEquipmentBookingRepository _equipmentBookingRepository;
        private readonly IMailService _mailService;
        private IAccountService _accountService;

        public BookingService(IMapper mapper, IVehicleBookingRepository bookingRepository, IAccountService accountService,
            IEquipmentBookingRepository equipmentRepository, IMailService mailService, ILogger<BookingService> logger)
        {
            _mapper = mapper;
            _vehicleBookingRepository = bookingRepository;
            _logger = logger;
            _equipmentBookingRepository = equipmentRepository;
            _mailService = mailService;
            _accountService = accountService;
        }

        public bool validateVehicleAvailability(int? id, DateTime start, DateTime end, int vehicleId)
        {
            List<VehicleBooking> bookings = _vehicleBookingRepository.validateRange(id, start, end, vehicleId);

            if(bookings.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<EquipmentDto> GetEquipmentAvailable(int? id, DateTime start, DateTime end)
        {
            return _mapper.Map<List<EquipmentDto>>(_equipmentBookingRepository.GetAvailableEquipment(id, start, end));

        }

        public void CreateBooking(CreateBookingDto createBooking)
        {
            try
            {
                VehicleBooking vehicle = _mapper.Map<VehicleBooking>(createBooking.vehicleBooking);
                vehicle.confirmationCode = ConfirmationCode.RandomString();
                _vehicleBookingRepository.Create(vehicle);
                SendMail(vehicle);
                _logger.LogInformation("Vehicle booking created");

                List<EquipmentBooking> equipmentBookings = _mapper.Map<List<EquipmentBooking>>(createBooking.equipmentBookings);

                foreach (var e in equipmentBookings)
                {
                    e.vehicleBookingId = vehicle.id;
                    _equipmentBookingRepository.Create(e);
                    _logger.LogInformation("Equipment booking created");
                }
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw ex;
            }
        }

        private void SendMail(VehicleBooking vehicle)
        {
            AccountDto account = _accountService.GetAccountById(vehicle.accountId);
            string body = "Hello "+ account.firstName+" "+ account.lastName+",\n \tThis email is sent as a confirmation the reservation you made today from "+ vehicle.startTime+ " to "+ vehicle.endTime+ ". Please make sure to collect your reservation on time if you fail to do so your account will be blacklisted. This is your confirmation code "+ vehicle.confirmationCode+".\n Thank you.";
            MailRequest mail = new MailRequest()
            {
                ToEmail = account.email,
                Subject = "Booking successfully",
                Body = body
            };
            _mailService.SendEmailAsync(mail);
        }
    }
}
