using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCLI.UI.Validators
{
    public class CompositeValidator<T> : IValidator<T>
    {
        private readonly IEnumerable<IValidator<T>> _validators;

        public CompositeValidator(IEnumerable<IValidator<T>> validators)
        {
            _validators = validators;
        }

        public string ErrorMessage { get; private set; } = string.Empty;

        public bool IsValid(T value)
        {
            foreach (var validator in _validators)
            {
                if (!validator.IsValid(value))
                {
                    ErrorMessage = validator.ErrorMessage;
                    return false;
                }
            }
            return true;
        }
    }
}
