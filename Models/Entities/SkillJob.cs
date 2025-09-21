using System.Security.Policy;

namespace Skillora.Models.Entities
{
    public class SkillJob
    {
        public string SkillId { get; set; }
        public string JobId { get; set; }
        public Job Job { get; set; }
        public Skill Skill { get; set; }
    }
}
