using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCLI.UI.Validators
{
    public class PositiveIntValidator : IValidator<int>
    {
        public string ErrorMessage => "Введите положительное число.";

        public bool IsValid(int value) =>
            value > 0;
    }
}
