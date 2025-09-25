using Microsoft.AspNetCore.Identity;
using Skillora.Models.Entities;

namespace Skillora.Models.Auth
{
    public class AppUser:IdentityUser
    {
        public string Role { get; set; }
        public bool status { get; set; }

        public string StudentId { get; set; }
        public Student Student { get; set; }
        public string CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
