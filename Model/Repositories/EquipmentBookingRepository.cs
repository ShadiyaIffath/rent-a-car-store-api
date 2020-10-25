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
            if (id == null)
            {
                bookings = _clientDbContext.EquipmentBookings.Where(x => x.equipment.id == equipmentId && x.status == "Confirmed" && (start > x.vehicleBooking.endTime || end < x.startTime)).ToList();
            }//existing booking duration validation
            else
            {
                bookings = _clientDbContext.EquipmentBookings.Where(x => x.equipment.id == equipmentId && x.id != id && x.status == "Confirmed" && (start > x.vehicleBooking.endTime || end < x.startTime)).ToList();
            }
            return bookings;
        }

        public List<Equipment> GetAvailableEquipment(int? id, DateTime start, DateTime end)
        {
            List<Equipment> allEquipment = _clientDbContext.Equipments.Include(x => x.category).ToList();
            List<Equipment> equipments = new List<Equipment>();
            if (id == 0)
            {
                var ids = _clientDbContext.EquipmentBookings
                          .Where(s => s.status == "Confirmed" && ((s.startTime <= start && s.vehicleBooking.endTime >= start) || (s.startTime <= end && s.vehicleBooking.endTime >= end)))
                          .Select(x => x.equipment.id).Distinct().ToList();

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
            else
            {
                var ids = _clientDbContext.EquipmentBookings
                          .Where(s => s.status == "Confirmed" && s.vehicleBooking.id != id &&
                           ((s.startTime <= start && s.vehicleBooking.endTime >= start) || (s.startTime <= end && s.vehicleBooking.endTime >= end)))
                          .Select(x => x.id).Distinct().ToList();
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
    }
}
