using Model.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories.Interfaces
{
    public interface IEquipmentBookingRepository : IRepositoryBase<EquipmentBooking>
    {
        public List<EquipmentBooking> validateRange(int? id, DateTime start, DateTime end, int equipmentId);

        public List<Equipment> GetAvailableEquipment(int? id, DateTime start, DateTime end);

        public List<EquipmentBooking> GetEquipmentBookingsFromBooking(int id);

        public void CreateEquipmentBooking(List<EquipmentBooking> equipmentBooking);

        public void RemoveEquipmentsInBookingById(List<int> ids, int booking);
    }
}
