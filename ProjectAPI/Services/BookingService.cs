using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Model.Entities;
using Model.Migrations;
using Model.Models;
using Model.Repositories;
using Model.Repositories.RepositoryFactory;
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
        private IRepositoryFactory _repositoryFactory;
        private readonly IMailService _mailService;

        public BookingService(IMapper mapper, IMailService mailService, ILogger<BookingService> logger, IRepositoryFactory repositoryFactory)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
            _logger = logger;
            _mailService = mailService;
        }

        public bool validateVehicleAvailability(int? id, DateTime start, DateTime end, int vehicleId)
        {
            List<VehicleBooking> bookings = _repositoryFactory.VehicleBookingRepository.validateRange(id, start, end, vehicleId);

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
            return _mapper.Map<List<EquipmentDto>>(_repositoryFactory.EquipmentBookingRepository.GetAvailableEquipment(id, start, end));

        }

        public void CreateBooking(CreateBookingDto createBooking)
        {
            try
            {
                VehicleBooking vehicle = _mapper.Map<VehicleBooking>(createBooking.vehicleBooking);
                vehicle.confirmationCode = ConfirmationCode.RandomString();
                _repositoryFactory.VehicleBookingRepository.Create(vehicle);
                SendMail(vehicle);
                _logger.LogInformation("Vehicle booking created");

                List<EquipmentBooking> equipmentBookings = _mapper.Map<List<EquipmentBooking>>(createBooking.equipmentBookings);

                foreach (var e in equipmentBookings)
                {
                    e.vehicleBookingId = vehicle.id;
                }
                _repositoryFactory.EquipmentBookingRepository.CreateEquipmentBooking(equipmentBookings);
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
            Account account = _repositoryFactory.AccountRepository.GetAccountById(vehicle.accountId);
            _mailService.SendBookingConfirmationEmail(account.email, account.firstName + " " + account.lastName, vehicle);
        }

        public List<BookingDto> GetAllBookings()
        {
            List<BookingDto> dto = new List<BookingDto>();
            List <VehicleBooking> vehicleBookings = _repositoryFactory.VehicleBookingRepository.GetBookings();
            List<EquipmentBookingDto> equipmentBookings;

            foreach(var x in vehicleBookings)
            {
                x.account.DecryptModel();
                equipmentBookings = new List<EquipmentBookingDto>();
                equipmentBookings.AddRange(_mapper.Map < List < EquipmentBookingDto>>(_repositoryFactory.EquipmentBookingRepository.GetEquipmentBookingsFromBooking(x.id)));
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
            List<VehicleBooking> vehicleBookings = _repositoryFactory.VehicleBookingRepository.GetUserBookings(id);
            List<EquipmentBookingDto> equipmentBookings;

            foreach (var x in vehicleBookings)
            {
                x.account.DecryptModel();
                equipmentBookings = new List<EquipmentBookingDto>();
                equipmentBookings.AddRange(_mapper.Map<List<EquipmentBookingDto>>(_repositoryFactory.EquipmentBookingRepository.GetEquipmentBookingsFromBooking(x.id)));
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
            _repositoryFactory.VehicleBookingRepository.DeleteBooking(id);
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
                List<EquipmentBooking> bookings = _repositoryFactory.EquipmentBookingRepository.validateRange(e.id, e.startTime, e.endTime, e.equipmentId);
                if (bookings.Count != 0)
                {
                    return false;
                }
            }
            return true;          
        }

        public void UpdateBooking(UpdateBookingDto dto)
        {
            _repositoryFactory.VehicleBookingRepository.Update(_mapper.Map<VehicleBooking>(dto.vehicleBooking));
            List<EquipmentBooking> bookings = _mapper.Map<List<EquipmentBooking>>(dto.equipmentBookings);
            List<int> ids = new List<int>();

            foreach ( var e in bookings)
            {
                if ( e.id != 0)
                {
                    _repositoryFactory.EquipmentBookingRepository.Update(e);
                }
                else
                {
                    _repositoryFactory.EquipmentBookingRepository.Create(e);
                }
                ids.Add(e.id);
            }

            _repositoryFactory.EquipmentBookingRepository.RemoveEquipmentsInBookingById(ids, dto.vehicleBooking.id);

            _logger.LogInformation("Booking successfully updated");
        }

        public void UpdateBookingStatus(int bookingId, string status)
        {
            _repositoryFactory.VehicleBookingRepository.UpdateBookingStatus(bookingId, status);
            _logger.LogInformation("Booking #" + bookingId + " status updated");
        }

        public BookingDto GetBooking(int id)
        {
            VehicleBooking vehicle = _repositoryFactory.VehicleBookingRepository.GetVehicleBooking(id);
            vehicle.account.DecryptModel();
            BookingDto dto = new BookingDto()
            {
                vehicleBooking = _mapper.Map<VehicleBookingDto>(vehicle),
                equipmentBookings = _mapper.Map<List<EquipmentBookingDto>>(_repositoryFactory.EquipmentBookingRepository.GetEquipmentBookingsFromBooking(id))
            };

            return dto;
        }
    }
}
