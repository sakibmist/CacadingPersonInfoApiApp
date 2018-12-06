using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Models {
    public class District {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength (50)]
        public string Name { get; set; }

        [Required]
        public int DivisionId { get; set; }

        [ForeignKey ("DivisionId")]
        public virtual Division Division { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}