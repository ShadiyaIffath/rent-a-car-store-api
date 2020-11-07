using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class CreateInquiryDto
    {
        public string name { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public string inquiry { get; set; }

        public bool responded { get; set; }

        public DateTime createdOn { get; set; }
    }
}
