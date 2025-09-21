using System;
using System.Security.Permissions;
using System.Web;

namespace Skillora.Models.ViewModels
{
    public class StudentViewModel
    {
        public string Id { get;set; }
        public string Name { get;set; }
        public string Email { get;set; }
        public DateTime DOB { get;set; }
        public Uri Github { get;set; }
        public double Cgpa { get;set; }
        public double Percentage10  { get;set; }
        public double Percentage12 { get;set; } 
    }
}
