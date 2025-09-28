using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;
using Skillora.Models.Entities;
using System.Collections.Generic;

namespace Skillora.Models.ViewModels
{
    public class CompanyViewModel 
    {
        public string? userId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Industry { get; set; }

        public List<Job> Job { get; set; }
    }
}
