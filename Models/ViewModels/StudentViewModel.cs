using System;
using System.Collections.Generic;
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
        public decimal Cgpa { get;set; }

        public string Phone {  get;set; }
        public decimal Percentage10  { get;set; }
        public decimal Percentage12 { get;set; } 

        public List<string> Skills { get;set; }


    }
}
