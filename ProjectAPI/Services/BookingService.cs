using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Model.Entities;
using Model.Migrations;
using Model.Models;
using Model.Repositories;
using Model.Repositories.Interfaces;
using Org.BouncyCastle.Math.EC.Rfc7748;
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
                }
                _equipmentBookingRepository.CreateEquipmentBooking(equipmentBookings);
                _logger.LogInformation("Equipment booking created");
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

        public List<BookingDto> GetAllBookings()
        {
            List<BookingDto> dto = new List<BookingDto>();
            List <VehicleBooking> vehicleBookings = _vehicleBookingRepository.GetBookings();
            List<EquipmentBookingDto> equipmentBookings;

            foreach(var x in vehicleBookings)
            {
                x.account.DecryptModel();
                equipmentBookings = new List<EquipmentBookingDto>();
                equipmentBookings.AddRange(_mapper.Map < List < EquipmentBookingDto>>(_equipmentBookingRepository.GetEquipmentBookingsFromBooking(x.id)));
                dto.Add(new BookingDto
                {
                    vehicleBooking = _mapper.Map<VehicleBookingDto>(x),
                    equipmentBookings = equipmentBookings
                });
            }

            return dto;
        }

        public List<BookingDto> GetUserBookings(int id)
        {
            List<BookingDto> dto = new List<BookingDto>();
            List<VehicleBooking> vehicleBookings = _vehicleBookingRepository.GetUserBookings(id);
            List<EquipmentBookingDto> equipmentBookings;

            foreach (var x in vehicleBookings)
            {
                x.account.DecryptModel();
                equipmentBookings = new List<EquipmentBookingDto>();
                equipmentBookings.AddRange(_mapper.Map<List<EquipmentBookingDto>>(_equipmentBookingRepository.GetEquipmentBookingsFromBooking(x.id)));
                dto.Add(new BookingDto
                {
                    vehicleBooking = _mapper.Map<VehicleBookingDto>(x),
                    equipmentBookings = equipmentBookings
                });
            }

            return dto;
        }

        public void DeleteBooking(int id)
        {
            _vehicleBookingRepository.DeleteBooking(id);
            _logger.LogInformation("Booking #"+id+" deleted successfully");
        }

        public bool ValidateBooking(UpdateBookingDto dto)
        {
            if(validateVehicleAvailability(dto.vehicleBooking.id, dto.vehicleBooking.startTime,
                    dto.vehicleBooking.endTime, dto.vehicleBooking.vehicleId) == false)
            {
                return false;
            }

            foreach(var e in dto.equipmentBookings)
            {
                List<EquipmentBooking> bookings = _equipmentBookingRepository.validateRange(e.id, e.startTime, e.endTime, e.equipmentId);
                if (bookings.Count != 0)
                {
                    return false;
                }
            }
            return true;          
        }

        public void UpdateBooking(UpdateBookingDto dto)
        {
            _vehicleBookingRepository.Update(_mapper.Map<VehicleBooking>(dto.vehicleBooking));
            List<EquipmentBooking> bookings = _mapper.Map<List<EquipmentBooking>>(dto.equipmentBookings);
            List<int> ids = new List<int>();

            foreach ( var e in bookings)
            {
                if ( e.id != 0)
                {
                    _equipmentBookingRepository.Update(e);
                }
                else
                {
                    _equipmentBookingRepository.Create(e);
                }
                ids.Add(e.id);
            }

            _equipmentBookingRepository.RemoveEquipmentsInBookingById(ids, dto.vehicleBooking.id);

            _logger.LogInformation("Booking successfully updated");
        }

        public void UpdateBookingStatus(int bookingId, string status)
        {
            _vehicleBookingRepository.UpdateBookingStatus(bookingId, status);
        }

        public BookingDto GetBooking(int id)
        {
            VehicleBooking vehicle = _vehicleBookingRepository.GetVehicleBooking(id);
            vehicle.account.DecryptModel();
            BookingDto dto = new BookingDto()
            {
                vehicleBooking = _mapper.Map<VehicleBookingDto>(vehicle),
                equipmentBookings = _mapper.Map<List<EquipmentBookingDto>>(_equipmentBookingRepository.GetEquipmentBookingsFromBooking(id))
            };

            return dto;
        }
    }
}
