using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Entities
{
    public class EquipmentBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public DateTime startTime { get; set; }

        [Required]
        public DateTime createdOn { get; set; }

        [ForeignKey("equipmentId")]
        public Equipment equipment { get; set; }

        public int equipmentId { get; set; }


        [ForeignKey("vehicleBookingId")]
        public VehicleBooking vehicleBooking { get; set; }

        public int vehicleBookingId { get; set; }
    }
}
