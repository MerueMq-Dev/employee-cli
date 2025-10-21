using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCLI.UI.Validators
{
    public class DateValidator : IValidator<DateTime>
    {
        private const string Format = "dd.MM.yyyy";
        public string ErrorMessage => $"Дата должна быть в формате {Format}, в прошлом и не старше 100 лет.";

        public bool IsValid(DateTime value) =>
            value < DateTime.Now && value > DateTime.Now.AddYears(-100);
        
        public static bool TryParse(string input, out DateTime date)
        {
            return DateTime.TryParseExact(
                input,
                Format,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out date);
        }
    }
}
