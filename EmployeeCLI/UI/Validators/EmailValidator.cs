using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeeCLI.UI.Validators
{
    public class EmailValidator : IValidator<string>
    {
        private static readonly Regex _regex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        public string ErrorMessage => "Введите корректный email (пример: user@example.com).";

        public bool IsValid(string value) =>
            !string.IsNullOrWhiteSpace(value) && _regex.IsMatch(value);
    }
}
