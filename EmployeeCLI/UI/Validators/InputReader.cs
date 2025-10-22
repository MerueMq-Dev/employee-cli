using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCLI.UI.Validators
{

    public static class InputReader
    {

        public static T Read<T>(
            string prompt,
            IValidator<T> validator,
            Func<string, T> parser)
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


        public static T Read<T>(
            string prompt,
            IEnumerable<IValidator<T>> validators,
            Func<string, T> parser
            )
        {
            var compositeValidator = new CompositeValidator<T>(validators);
            return Read(prompt, compositeValidator, parser);
        }

       

        public static string? ReadOptionalString(
            string prompt,
            IValidator<string> validator)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrEmpty(input))
                    return null;

                if (validator.IsValid(input))
                    return input;

                Console.WriteLine($"Ошибка: {validator.ErrorMessage}");
            }
        }

        public static string? ReadOptionalString(
            string prompt,
            IEnumerable<IValidator<string>> validators)
        {
            var compositeValidator = new CompositeValidator<string>(validators);
            return ReadOptionalString(prompt, compositeValidator);
        }


        public static T? ReadOptional<T>(
            string prompt,
            IValidator<T> validator,
            Func<string, T> parser) where T : struct
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrEmpty(input))
                    return null;

                try
                {
                    T value = parser(input);
                    if (validator.IsValid(value))
                        return value;

                    Console.WriteLine($"Ошибка: {validator.ErrorMessage}");
                }
                catch
                {
                    Console.WriteLine("Ошибка: неверный формат ввода. Попробуйте снова или нажмите Enter для пропуска.");
                }
            }
        }

        public static T? ReadOptional<T>(
            string prompt,
            IEnumerable<IValidator<T>> validators,
            Func<string, T> parser
            ) where T : struct
        {
            var compositeValidator = new CompositeValidator<T>(validators);
            return ReadOptional(prompt, compositeValidator, parser);
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
