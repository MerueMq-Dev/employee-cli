using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCLI.UI.Validators
{
    public class IntRangeValidator : IValidator<int>
    {
        private readonly int _min;
        private readonly int _max;

        public IntRangeValidator(int min = int.MinValue, int max = int.MaxValue)
        {
            _min = min;
            _max = max;
        }

        public string ErrorMessage => $"Введите число от {_min} до {_max}.";

        public bool IsValid(int value) =>
            value >= _min && value <= _max;
    }
}
