using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Model.Models
{
    public class EquipmentCategoryDto
    {
        public int id { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public byte[] image { get; set; }

        public int price { get; set; }
    }
}
