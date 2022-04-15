using System;
using System.Globalization;
using System.Windows.Controls;

namespace QRCodeGenerator
{
    class VersionRule: ValidationRule
    {
        public static bool isValid;
        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        { 
            var age = 0;

            try
            {
                if (((string)value).Length > 0)
                    age = int.Parse((string)value);
                else
                    isValid = false;
            }
            catch (Exception e)
            {
                isValid = false;
            }
            if ((age < Min) || (age > Max))
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }
            return new ValidationResult(isValid, null);
        }
    }
}
