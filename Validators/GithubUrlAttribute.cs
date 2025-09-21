using System;
using System.ComponentModel.DataAnnotations;

namespace Skillora.Validators
{
    public class GithubUrlAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (!Uri.TryCreate(value.ToString(), UriKind.Absolute, out var url) || (url.Scheme != Uri.UriSchemeHttp && url.Scheme != Uri.UriSchemeHttps))
            {
                return new ValidationResult("Invalid URL format");
            }

            if(!url.Host.Contains("github.com"))
            {
                return new ValidationResult("URL must be github url");
            }

            return ValidationResult.Success;
        }
    }
}
