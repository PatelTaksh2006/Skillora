using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Skillora.Models.Entities
{
    public class Company
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "Company name is required.")]
        [MaxLength(25, ErrorMessage = "Company name cannot exceed 25 characters.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [MaxLength(100, ErrorMessage = "Industry field cannot exceed 100 characters.")]
        public string Industry { get; set; }

        public List<Job> Job { get; set; }
    }

}
