﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class UpdateEquipmentBookingDto
    {
        public int id { get; set; }

        public DateTime startTime { get; set; }

        public DateTime endTime { get; set; }

        public DateTime createdOn { get; set; }

        public int equipmentId { get; set; }

        public int vehicleBookingId { get; set; }
    }
}
