using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class CreateVehicleTypeDto
    {
        public string type { get; set; }

        public double pricePerDay { get; set; }

        public int passengers { get; set; }
    }
}
