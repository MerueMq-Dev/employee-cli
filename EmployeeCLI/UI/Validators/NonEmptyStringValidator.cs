using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCLI.UI.Validators
{
    public class NonEmptyStringValidator : IValidator<string>
    {
        public string ErrorMessage => "Значение не может быть пустым.";

        public bool IsValid(string value) =>
            !string.IsNullOrWhiteSpace(value);
    }
}
