using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCLI.UI.Validators
{
    public class StringLengthValidator : IValidator<string>
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public StringLengthValidator(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        public string ErrorMessage => $"Длина должна быть от {_minLength} до {_maxLength} символов.";

        public bool IsValid(string value)
        {            
            if (string.IsNullOrEmpty(value))
                return false;

            return value.Length >= _minLength && value.Length <= _maxLength;
        }
    }
}
