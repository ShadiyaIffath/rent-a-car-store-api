using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Entities
{
    public class Inquiry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [MaxLength(100)]
        public string name { get; set; }
        [Required]
        [MaxLength(150)]
        public string email { get; set; }
        [Required]
        [MaxLength(10)]
        public string phone { get; set; }

        [Required]
        public DateTime createdOn { get; set; }

        [Required]
        public string inquiry { get; set; }

        public bool responded { get; set; }
    }
}
