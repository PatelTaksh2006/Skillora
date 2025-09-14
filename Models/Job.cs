using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skillora.Models
{
    public class Job
    {

        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Title must be 2-100 characters.")]
        public string Title { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Description can be maximum 1000 characters.")]
        public string Description { get; set; }

        [Required]
        [Range(2, int.MaxValue)]
        public int package {  get; set; } //LPA

        [Required]
        [StringLength(100, ErrorMessage = "Job location can be maximum 100 characters.")]
        public string JobLocation { get; set; }
        public string CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        public JobConstraint JobConstraint { get; set; }

        public List<StudentJob> StudentJobs{ get; set; }=new List<StudentJob>();

        public List<SkillJob> SkillJobs{ get; set; }=new List<SkillJob>();

    }
}
