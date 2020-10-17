using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class CreateVehicleDto
    {
        public bool active { get; set; }

        public DateTime dayAdded { get; set; }

        public string carCode { get; set; }

        public string model { get; set; }

        public string engine { get; set; }

        public bool automatic { get; set; }

        public Object image { get; set; }

        public int typeId { get; set; }

        public int engineCapacity { get; set; }

        public int fuelConsumption { get; set; }
    }
}
