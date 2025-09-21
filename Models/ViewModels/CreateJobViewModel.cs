using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skillora.Models.ViewModels
{
    public class CreateJobViewModel
    {
        public string Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Title must be 2-100 characters.")]
        public string Title { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Description can be maximum 1000 characters.")]
        public string Description { get; set; }

        [Required]
        [Range(2, int.MaxValue)]
        public int package { get; set; } //LPA

        [Required]
        [StringLength(100, ErrorMessage = "Job location can be maximum 100 characters.")]
        public string JobLocation { get; set; }

        [Required]
        public string[] selectedSkills { get; set; }

        public List<SelectListItem> skills { get; set; }










        public string CompanyId { get; set; }

        [Required(ErrorMessage = "Minimum CGPA is required.")]
        [Column(TypeName = "decimal(4,2)")]
        [Range(0.0, 10.0, ErrorMessage = "Minimum CGPA must be between 0.0 and 10.0.")]
        public decimal? MinCGPA { get; set; }

        [Required(ErrorMessage = "Minimum 10th percentage is required.")]
        [Column(TypeName = "decimal(5,2)")]
        [Range(0.0, 100.0, ErrorMessage = "Minimum 10th percentage must be between 0.0 and 100.0.")]
        public decimal? MinPercentage10 { get; set; }

        [Required(ErrorMessage = "Minimum 12th percentage is required.")]
        [Column(TypeName = "decimal(5,2)")]
        [Range(0.0, 100.0, ErrorMessage = "Minimum 12th percentage must be between 0.0 and 100.0.")]
        public decimal? MinPercentage12 { get; set; }

        [Range(20, 24, ErrorMessage = "Minimum age must be between 20 and 24.")]
        public int? MinAge { get; set; }

        [Range(20, 24, ErrorMessage = "Maximum age must be between 20 and 24.")]
        public int? MaxAge { get; set; }
    }
}
