using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Models {
    public class Person {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength (50)]
        public string Name { get; set; }

        [Required]
        public DateTime Dob { get; set; }

        [Required]
        [StringLength (15)]
        public string MobileNo { get; set; }

        [Required]
        public int DivisionId { get; set; }

        [Required]
        public int DistrictId { get; set; }

        [Required]
        public int UpazillaId { get; set; }

        [ForeignKey ("DivisionId")]
        public virtual Division Division { get; set; }

        [ForeignKey ("DistrictId")]
        public virtual District District { get; set; }

        [ForeignKey ("UpazillaId")]
        public virtual Upazilla Upazilla { get; set; }

        [Required]
        [StringLength (100)]
        public string VillageName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}