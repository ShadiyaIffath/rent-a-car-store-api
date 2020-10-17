using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class CreateCategoryDto
    {
        public string title { get; set; }

        public string description { get; set; }

        public Object image { get; set; }

        public int price { get; set; }
    }
}
