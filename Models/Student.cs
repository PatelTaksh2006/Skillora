using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skillora.Models
{
    public class Student
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\d{9}$")]
        public int Phone { get; set; }
        [Required]
        public DateTime DOB { get; set; }


        [Required]
        [RegularExpression(@"[\w\,]$")]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@" ^\d{6}$")]
        public int Pincode { get; set; }
        public Uri Github { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(4,2)")]
        [Range(0.0, 10.0)]
        public decimal Cgpa { get; set; }

        [Required]
        [Column(TypeName = "decimal(4,2)")]  //Database Side
        [Range(0.0, 10.0)]
        public decimal Percentage10 { get; set; }

        [Required]
        [Column(TypeName = "decimal(4,2)")]
        [Range(0.0, 10.0)]
        public decimal Percentage12 { get; set; }


    }
}
