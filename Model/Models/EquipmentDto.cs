using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class EquipmentDto
    {
        public int id { get; set; }

        public string name { get; set; }

        public string features { get; set; }

        public double purchasedPrice { get; set; }

        public DateTime dayAdded { get; set; }

        public EquipmentCategoryDto category { get; set; }

        public int categoryId { get; set; }
    }
}
