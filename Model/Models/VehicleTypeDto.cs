﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class VehicleTypeDto
    {
        public int id { get; set; }

        public string type { get; set; }

        public int passengers { get; set; }

        public double pricePerDay { get; set; }
    }
}
