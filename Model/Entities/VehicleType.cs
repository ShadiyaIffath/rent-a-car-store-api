using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Entities
{
    public class VehicleType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [MaxLength(25)]
        public string type { get; set; }

        [Required]
        public double pricePerDay { get; set; }

        [Required]
        public byte[] image { get; set; }
    }
}
