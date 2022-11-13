using System.Text.RegularExpressions;
using WebAutopark.DAL.Validation.Interfaces;

namespace WebAutopark.DAL.Validation.Entities
{
    public sealed class BelarusianNumberValidator : INumberValidator
    {
        private string _defaultCarsNumberPattern = @"^\d{4} [ABEKMHOPCTYX]{2}-[1234567]{1}$";
        private string _electricalCarsNumberPattern = @"^[ABEKMHOPCTYX]{1}\d{3}[ABEKMHOPCTYX]{2}-[1234567]{1}$";
        public bool IsValid(string value)
        {
            if (Regex.IsMatch(value, _defaultCarsNumberPattern, RegexOptions.IgnoreCase) ||
                Regex.IsMatch(value, _electricalCarsNumberPattern, RegexOptions.IgnoreCase)
                )
            {
                return true;
            }

            return false;
        }
    }
}
