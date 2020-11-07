using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using UtilityLibrary.Utils;

namespace Model.Entities
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [MaxLength(150)]
        public string email { get; set; }

        [Required]
        [MaxLength(100)]
        public string password { get; set; }

        [Required]
        [MaxLength(100)]
        public string firstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string lastName { get; set; }

        [Required]
        public DateTime dob { get; set; }

        [Required]
        [MaxLength(10)]
        public int phone { get; set; }

        public byte[] drivingLicense { get; set; }

        public byte[] additionalIdentitfication { get; set; }

        [Required]
        public bool active { get; set; }

        public string licenseId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime activatedDate { get; set; }

        [ForeignKey("typeId")]
        public AccountType type { get; set; }

        public int typeId { get; set; }

        public void DecryptModel()
        {
            this.email = EncryptUtil.DecryptString(this.email);
            this.password = EncryptUtil.DecryptString(this.password);
            this.firstName = EncryptUtil.DecryptString(this.firstName);
            this.lastName = EncryptUtil.DecryptString(this.lastName);
        }

        public void EncryptModel()
        {
            this.email = EncryptUtil.EncryptString(this.email);
            this.password = EncryptUtil.EncryptString(this.password);
            this.firstName = EncryptUtil.EncryptString(this.firstName);
            this.lastName = EncryptUtil.EncryptString(this.lastName);
        }

    }
}
