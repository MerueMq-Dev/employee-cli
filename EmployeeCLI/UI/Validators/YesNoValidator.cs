using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCLI.UI.Validators
{
    public class YesNoValidator : IValidator<string>
    {
        public string ErrorMessage => "Введите Y (да) или N (нет).";

        public bool IsValid(string value)
        {
            var input = value?.Trim().ToUpperInvariant();
            return input is "Y" or "YES" or "Д" or "ДА" or "N" or "NO" or "Н" or "НЕТ";
        }

        public bool ParseToBool(string value)
        {
            var input = value?.Trim().ToUpperInvariant();
            return input is "Y" or "YES" or "Д" or "ДА";
        }
    }
}
