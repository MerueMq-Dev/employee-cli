using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeeCLI.UI.Validators
{
    public class LettersOnlyValidator : IValidator<string>
    {
        private static readonly Regex _regex = new(@"^[a-zA-Zа-яА-ЯёЁ\s]+$");

        public string ErrorMessage => "Допустимы только буквы и пробелы.";

        public bool IsValid(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return _regex.IsMatch(value);
        }
    }
}
