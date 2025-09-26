using System.Collections.Generic;

namespace Skillora.Models.ViewModels
{
    public class JobListViewModel
    {
        public string Id { get; set; }

        
        public List<string> CompanySkills { get; set; }

        // Skills matched between student and company
        public List<string> MatchedSkills { get; set; }

        public string Job {  get; set; }
        public string CompanyName { get; set; }

        // Skills company requires but student doesn't have
        public List<string> RemainingSkills { get; set; }

        public bool eligible { get; set; }

        public bool applied { get; set; }
    }
}
