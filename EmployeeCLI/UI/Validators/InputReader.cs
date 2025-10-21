using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCLI.UI.Validators
{

    public static class InputReader
    {
        public static T Read<T>(string prompt, IValidator<T> validator, Func<string, T> parser)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim() ?? "";

                try
                {
                    T value = parser(input);

                    if (validator.IsValid(value))
                        return value;

                    Console.WriteLine($"Ошибка: {validator.ErrorMessage}");
                }
                catch
                {
                    Console.WriteLine("Ошибка: неверный формат ввода.");
                }
            }
        }

        public static bool ReadYesNo(string prompt)
        {
            var validator = new YesNoValidator();
            while (true)
            {
                Console.Write($"{prompt} (Y/N): ");
                string input = Console.ReadLine()?.Trim() ?? "";

                if (validator.IsValid(input))
                    return validator.ParseToBool(input);

                Console.WriteLine($"Ошибка: {validator.ErrorMessage}");
            }
        }
    }
}
