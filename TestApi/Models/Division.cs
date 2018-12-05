using System;
using System.ComponentModel.DataAnnotations;

namespace TestApi.Models
{
    public class Division
    {
        [Key]
        public int Id  { get; set; }

        [Required]
        [StringLength(50)]
        public string Name  { get; set; }
        public DateTime CreatedAt { get; set; }=DateTime.Now;

    }
}