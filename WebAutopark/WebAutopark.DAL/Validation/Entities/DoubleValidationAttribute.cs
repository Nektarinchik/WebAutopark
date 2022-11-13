using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WebAutopark.DAL.Validation.Entities
{
    public sealed class DoubleValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {

            if (ReferenceEquals(value, null))
            {
                return false;
            }

            try
            {
                double casted = Convert.ToDouble(value.ToString(), CultureInfo.InvariantCulture);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
