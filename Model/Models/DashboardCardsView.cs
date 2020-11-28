using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class DashboardCardsView
    {
        public int totalVehicles { get; set; }

        public int bookings { get; set; }

        public int completedBookins { get; set; }

        public int cancelledBookings { get; set; }

        public int collectedBookings { get; set; }

        public int confirmedBookings { get; set; }

        public int smallTownCar  { get; set; }

        public int hatchback { get; set; }

        public int saloon { get; set; }

        public int estate { get; set; }

        public int vans { get; set; }

        public List<AccountDto> accounts { get; set; }

        public List<VehicleBookingDto> vehicleBookings { get; set; }
    }
}
