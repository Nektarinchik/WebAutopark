using System.ComponentModel.DataAnnotations;
using WebAutopark.DAL.Validation.Interfaces;
using WebAutopark.DAL.Validation.Entities;

namespace WebAutopark.DAL.Validation.Entities
{
    public sealed class RegistrationNumberValidationAttribute : ValidationAttribute
    {
        private INumberValidator? _numberValidator = null;
        public RegistrationNumberValidationAttribute(Type type)
        {
            object? obj = Activator.CreateInstance(type);
            if (obj != null)
            {
                try
                {
                    _numberValidator = (INumberValidator)obj;
                }
                catch
                { }
            }
        }

        public override bool IsValid(object? value)
        {
            if (ReferenceEquals(value, null) || ReferenceEquals(_numberValidator, null))
            {
                return false;
            }

            return _numberValidator.IsValid(value.ToString());
        }
    }
}
