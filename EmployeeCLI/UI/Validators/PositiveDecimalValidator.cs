using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCLI.UI.Validators
{
    public class PositiveDecimalValidator : IValidator<decimal>
    {
        public string ErrorMessage => "Введите положительное число.";

        public bool IsValid(decimal value) =>
            value > 0;
    }
}
