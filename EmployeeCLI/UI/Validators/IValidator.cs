using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCLI.UI.Validators
{
    public interface IValidator<T>
    {
        bool IsValid(T value);

        string ErrorMessage { get; }
    }
}
