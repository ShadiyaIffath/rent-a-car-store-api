using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Entities
{
    public class Equipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [MaxLength(25)]
        public string name { get; set; }

        [MaxLength(500)]
        public string features { get; set; }

        [Required]
        public double purchasedPrice { get; set; }

        [Required]
        public DateTime dayAdded { get; set; }

        [ForeignKey("categoryId")]
        public EquipmentCategory category { get; set; }

        public int categoryId { get; set; }
    }
}
