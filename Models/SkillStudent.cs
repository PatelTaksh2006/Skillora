using System.Security.Policy;

namespace Skillora.Models
{
    public class SkillStudent
    {
        public string StudentId { get; set; }
        public string SkillId { get; set; }
        public Student Student { get; set; }
        public Skill Skill { get; set; }
    }
}
