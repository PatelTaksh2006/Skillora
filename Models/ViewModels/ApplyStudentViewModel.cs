using System;
using System.Collections.Generic;

namespace Skillora.Models.ViewModels
{
    public class ApplyStudentViewModel
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Uri Github { get; set; }
        public decimal Cgpa { get; set; }
        public decimal Percentage10 { get; set; }
        public decimal Percentage12 { get; set; }

        public List<string> Skills { get; set; }
        public List<string> matched { get; set; }
    }
}
