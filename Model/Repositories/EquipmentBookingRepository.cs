using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.DatabaseContext;
using Model.Entities;
using Model.Repositories.Base;
using Model.Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Model.Repositories
{
    public class EquipmentBookingRepository : RepositoryBase<EquipmentBooking>, IEquipmentBookingRepository
    {
        private ILogger _logger;

        public EquipmentBookingRepository(ClientDbContext clientDbContext, ILogger<EquipmentBookingRepository> logger) : base(clientDbContext)
        {
            _logger = logger;
        }

        public List<EquipmentBooking> validateRange(int? id, DateTime start, DateTime end, int equipmentId)
        {
            List<EquipmentBooking> bookings = new List<EquipmentBooking>();
            //new booking duration validation
            if (id == 0)
            {
                bookings = _clientDbContext.EquipmentBookings.Where(x => x.equipment.id == equipmentId && (x.vehicleBooking.status == "Confirmed" || x.vehicleBooking.status == "Collected") && ((x.startTime <= start && x.vehicleBooking.endTime >= start) || (x.startTime <= end && x.vehicleBooking.endTime >= end ))).ToList();
            }//existing booking duration validation       
            else
            {
                bookings = _clientDbContext.EquipmentBookings.Where(x => x.equipment.id == equipmentId && x.id != id && (x.vehicleBooking.status == "Confirmed" || x.vehicleBooking.status == "Collected") && ((x.startTime <= start && x.vehicleBooking.endTime >= start) || (x.startTime <= end && x.vehicleBooking.endTime >= end))).ToList();
            }
            return bookings;
        }

        public List<Equipment> GetAvailableEquipment(int? id, DateTime start, DateTime end)
        {
            List<Equipment> allEquipment = _clientDbContext.Equipments.Include(x => x.category).ToList();
            List<Equipment> equipments = new List<Equipment>();
            List<int> ids = new List<int>();
            if (id == 0)
            {
                ids = _clientDbContext.EquipmentBookings
                          .Where(s => (s.vehicleBooking.status == "Confirmed" || s.vehicleBooking.status == "Collected") && ((s.startTime <= start && s.vehicleBooking.endTime >= start) || (s.startTime <= end && s.vehicleBooking.endTime >= end)))
                          .Select(x => x.equipment.id).ToList();
            }
            else
            {
                ids = _clientDbContext.EquipmentBookings
                          .Where(s => (s.vehicleBooking.status == "Confirmed" || s.vehicleBooking.status == "Collected") && s.vehicleBooking.id != id &&
                           ((s.startTime <= start && s.vehicleBooking.endTime >= start) || (s.startTime <= end && s.vehicleBooking.endTime >= end)))
                          .Select(x => x.id).ToList();
            }

            if (ids.Count == 0)
            {
                equipments = allEquipment;
            }
            else
            {
                foreach (var e in allEquipment)
                {
                    foreach (var i in ids)
                    {
                        if (e.id != i)
                        {
                            equipments.Add(e);
                        }
                    }
                }
            }
            return equipments;
        }

        public List<EquipmentBooking> GetEquipmentBookingsFromBooking(int id)
        {
            return _clientDbContext.EquipmentBookings
                .Include(a => a.equipment).ThenInclude(b => b.category).Where(x => x.vehicleBooking.id == id).ToList();
        }

        public void CreateEquipmentBooking(List<EquipmentBooking> equipmentBookings)
        {
            _clientDbContext.EquipmentBookings.AddRange(equipmentBookings);
            _clientDbContext.SaveChanges();
        }

        public void RemoveEquipmentsInBookingById(List<int> ids, int booking)
        {
            List<EquipmentBooking> bookings = _clientDbContext.EquipmentBookings.Where(x => x.vehicleBooking.id == booking).ToList();

            foreach (var e in bookings)
            {
                var exist = false;
                foreach (var i in ids)
                {
                    if (e.id == i)
                    {
                        exist = true;
                    }
                }

                if (!exist)
                {
                    _clientDbContext.EquipmentBookings.Remove(e);
                    _clientDbContext.SaveChanges();
                }
            }
        }
    }
}
