using System.ComponentModel.DataAnnotations;

namespace Skillora.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Skill
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "Skill name is required.")]
        [MaxLength(15, ErrorMessage = "Skill name cannot exceed 15 characters.")]
        
        public string Name { get; set; }
    }

}
