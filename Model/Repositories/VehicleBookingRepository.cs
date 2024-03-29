﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.DatabaseContext;
using Model.Entities;
using Model.Models;
using Model.Repositories.Base;
using Model.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Repositories
{
    public class VehicleBookingRepository : RepositoryBase<VehicleBooking>, IVehicleBookingRepository
    {

        public VehicleBookingRepository(ClientDbContext clientDbContext) : base(clientDbContext)
        {
        }

        public List<VehicleBooking> validateRange(int? id, DateTime start, DateTime end, int vehicleId)
        {
            List<VehicleBooking> bookings = new List<VehicleBooking>();
            //new booking duration validation
            if (id == 0)
            {
                bookings = _clientDbContext.VehicleBookings.Where(x => x.vehicle.id == vehicleId &&
                (x.status=="Confirmed" || x.status == "Collected") && 
                ((x.startTime <= start && x.endTime >= start) || (x.startTime <= end && x.endTime >= end) ||
                (x.startTime >= start && x.endTime <= end))).ToList();
            }//existing booking duration validation
            else
            {
                bookings = _clientDbContext.VehicleBookings.Where(x => x.vehicle.id == vehicleId && 
                x.id != id &&
                (x.status == "Confirmed" || x.status == "Collected") &&
                ((x.startTime <= start && x.endTime >= start) || (x.startTime <= end && x.endTime >= end ||
                (x.startTime >= start && x.endTime <= end)))).ToList();
            }
            return bookings;
        }

        public List<VehicleBooking> GetBookings()
        {
            return _clientDbContext.VehicleBookings.Include(x => x.vehicle).ThenInclude(a => a.type).Include(x => x.account).ToList();
        }

        public void DeleteBooking(int id)
        {
            _clientDbContext.EquipmentBookings.RemoveRange(_clientDbContext.EquipmentBookings.Where(x => x.vehicleBooking.id == id));
            _clientDbContext.VehicleBookings.RemoveRange(_clientDbContext.VehicleBookings.Where(x => x.id == id));
            _clientDbContext.SaveChanges();
        }

        public void UpdateBookingStatus(int id, string status)
        {
            VehicleBooking vehicleBooking = _clientDbContext.VehicleBookings.Where(x => x.id == id).FirstOrDefault();
            vehicleBooking.status = status;
            _clientDbContext.SaveChanges();
       }

        public VehicleBooking GetVehicleBooking(int id)
        {
            return _clientDbContext.VehicleBookings.Where(y => y.id == id).Include(x => x.vehicle).ThenInclude(a => a.type).Include(x => x.account).FirstOrDefault();
        }

        public List<VehicleBooking> GetUserBookings(int id)
        {
            return _clientDbContext.VehicleBookings.Where(y => y.account.id == id).Include(x => x.vehicle).ThenInclude(a => a.type).Include(x => x.account).ToList();
        }

        public List<VehicleBooking> GetBookingsWithinRange(DateTime start, DateTime end)
        {
            return _clientDbContext.VehicleBookings.Where(x => (x.status == "Confirmed" || x.status == "Collected") && ((x.startTime <= start && x.endTime >= start) || (x.startTime <= end && x.endTime >= end) || (x.startTime >= start && x.endTime <= end) || (x.startTime >= start && x.endTime <= end))).ToList();
        }
    }

}
