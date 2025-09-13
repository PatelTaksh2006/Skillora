using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skillora.Models
{
    public class JobConstraint
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "Minimum CGPA is required.")]
        [Column(TypeName = "decimal(4,2)")]
        [Range(0.0, 10.0, ErrorMessage = "Minimum CGPA must be between 0.0 and 10.0.")]
        public decimal MinCGPA { get; set; }

        [Required(ErrorMessage = "Minimum 10th percentage is required.")]
        [Column(TypeName = "decimal(5,2)")]
        [Range(0.0, 100.0, ErrorMessage = "Minimum 10th percentage must be between 0.0 and 100.0.")]
        public decimal MinPercentage10 { get; set; }

        [Required(ErrorMessage = "Minimum 12th percentage is required.")]
        [Column(TypeName = "decimal(5,2)")]
        [Range(0.0, 100.0, ErrorMessage = "Minimum 12th percentage must be between 0.0 and 100.0.")]
        public decimal MinPercentage12 { get; set; }

        [Range(17, 30, ErrorMessage = "Minimum age must be between 17 and 30.")]
        public int MinAge { get; set; }

        [Range(17, 30, ErrorMessage = "Maximum age must be between 17 and 30.")]
        public int MaxAge { get; set; }

        public string JobId { get; set; }

        public Job Job { get; set; }
    }

}
