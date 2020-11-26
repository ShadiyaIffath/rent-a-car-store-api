using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class DashboardCardsView
    {
        public int totalVehicles { get; set; }

        public int totalEquipment { get; set; }

        public int vehicleBookings { get; set; }

        public int completedBookins { get; set; }

        public int cancelledBookings { get; set; }

        public int collectedBookings { get; set; }

        public int confirmedBookings { get; set; }

        public List<VehicleDto> vehicles { get; set; }

        public List<AccountDto> accounts { get; set; }

        public List<VehicleBookingDto> vehicleBookingDtos { get; set; }
    }
}
