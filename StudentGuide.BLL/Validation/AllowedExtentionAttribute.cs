using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Validation
{
    public class AllowedExtentionAttribute :ValidationAttribute
    {
        private readonly string _allowedExtention;

        public AllowedExtentionAttribute(string allowedExtention)
        {
            _allowedExtention = allowedExtention;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var isAllowed = _allowedExtention.Split(',').Contains(extention ,StringComparer.OrdinalIgnoreCase);
                if (!isAllowed)
                {
                    return new ValidationResult($"only {_allowedExtention} extentions");
                }
            }
            return ValidationResult.Success;
        }
    }
}
