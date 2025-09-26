using System.Collections.Generic;

namespace Skillora.Models.ViewModels
{
    public class JobViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int package { get; set; } // LPA

        public string JobLocation { get; set; }

        public string CompanyName { get; set; }
        public List<string> Skills { get; set; }
        public decimal MinCGPA { get; set; }

        public decimal MinPercentage10 { get; set; }

        public decimal MinPercentage12 { get; set; }

        public int MinAge { get; set; }

        public int MaxAge { get; set; }
    }
}
