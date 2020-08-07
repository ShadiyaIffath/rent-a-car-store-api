using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Entities
{
    public class VehicleBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public DateTime startTime { get; set; }

        [Required]
        public DateTime endTime { get; set; }

        [Required]
        [MaxLength(10)]
        public string confirmationCode { get; set; }

        [Required]
        public double totalCost { get; set; }

        [Required]
        public DateTime createdOn { get; set; }

        [Required]
        [MaxLength(10)]
        public string status { get; set; }

        [ForeignKey("vehicleId")]
        public Vehicle vehicle { get; set; }

        public int vehicleId { get; set; }
    }
}
