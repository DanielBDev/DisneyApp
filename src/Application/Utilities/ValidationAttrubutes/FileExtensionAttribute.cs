using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Application.Utilities.ValidationAttrubutes
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        private readonly string[] _validTypes;

        public FileExtensionAttribute(string[] validTypes)
        {
            _validTypes = validTypes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var formFile = value as IFormFile;

            if (formFile != null)
            {
                if (!_validTypes.Contains(formFile.ContentType))
                {
                    return new ValidationResult($"Extension no valida. Las extensiones validas son: {string.Join(",",_validTypes)}");
                }
            }

            return ValidationResult.Success;
        }
    }
}
