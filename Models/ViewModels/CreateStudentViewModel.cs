using Microsoft.AspNetCore.Mvc.Rendering;
using Skillora.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skillora.Models.ViewModels
{
    public class CreateStudentViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",ErrorMessage ="Invalid Email Format")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$")]
        public string Phone { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName ="date")]
        public DateTime? DOB { get; set; }

        [Required]
        
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^\d{6}$",ErrorMessage ="Pincode must be 6 digit number")]
        public string Pincode { get; set; }

        [GithubUrl]
        public Uri Github { get; set; }

        [Required]
        [Range(0.0, 10.0)]
        public decimal? Cgpa { get; set; }

        [Required]
        [Range(0.0, 100.0)]
        public decimal? Percentage10 { get; set; }

        [Required]
        [Range(0.0, 100.0)]
        public decimal? Percentage12 { get; set; }

        public List<SelectListItem> skills { get; set; }
        [Required]
        [Display(Name="Skills (Use Crtl + select to select/disselect multiple options)")]
        public string[] selectedSKills { get; set; }
    }
}
