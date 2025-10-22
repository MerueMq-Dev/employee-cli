using EmployeeCLI.Models;
using EmployeeCLI.Services;
using EmployeeCLI.UI.Validators;

namespace EmployeeCLI.UI.MenuCommands
{
    public class UpdateEmployeeMenuCommand : BaseMenuCommand
    {
        public override int Id => 3;
        public override string Title => "Обновить информацию о сотруднике";

        public UpdateEmployeeMenuCommand(EmployeeService service) : base(service) { }

        public async override Task Execute()
        {
            PrintHeader("ОБНОВЛЕНИЕ ДАННЫХ СОТРУДНИКА");

            int id = InputReader.Read("Id: ", new PositiveIntValidator(), s => int.Parse(s));

            var result =  await Service.GetEmployeeById(id);
            if (!result.IsSuccess)
            {
                PrintError(result.Message);
                return;
            }

            var existingEmployee = result.Data;
            Console.WriteLine("\nТекущие данные:");
            Console.WriteLine(existingEmployee);
            
            var updateEmployee = new UpdateEmployee { EmployeeID = id };

            Console.WriteLine("\nВведите новые значения (Enter = пропустить):\n");
       

            Console.WriteLine("\n💡 Введите новые значения или нажмите Enter для пропуска:\n");

            updateEmployee.FirstName = InputReader.ReadOptionalString(
                $"Имя [{existingEmployee.FirstName}]: ",
                    [new NonEmptyStringValidator(), new LettersOnlyValidator(), 
                    new StringLengthValidator(1, 50)]
            );

            updateEmployee.LastName = InputReader.ReadOptionalString(
                $"Фамилия [{existingEmployee.LastName}]: ",
                [new NonEmptyStringValidator(), new LettersOnlyValidator(), new StringLengthValidator(1, 50)]

            );

            updateEmployee.Email = InputReader.ReadOptionalString(
                $"Email [{existingEmployee.Email}]: ",
                [new EmailValidator(), new StringLengthValidator(1, 100)]
            );

            updateEmployee.DateOfBirth = InputReader.ReadOptional(
                $"Дата рождения [{existingEmployee.DateOfBirth:dd.MM.yyyy}] (ДД.ММ.ГГГГ): ",
                new DateValidator(),
                s => DateTime.ParseExact(s, "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture)
            );

            updateEmployee.Salary = InputReader.ReadOptional(
                $"Зарплата [{existingEmployee.Salary:N2}]: ",
                new PositiveDecimalValidator(), 
                s => decimal.Parse(s)
            );
                    
            if (updateEmployee.FirstName == null &&
                updateEmployee.LastName == null &&
                updateEmployee.Email == null &&
                updateEmployee.DateOfBirth == null &&
                updateEmployee.Salary == null)
            {
                Console.WriteLine("\nНичего не изменено.");
                return;
            }
           
            var updateResult =  await Service.UpdateEmployee(updateEmployee);

            if (updateResult.IsSuccess)
                PrintSuccess(updateResult.Message);
            else
                PrintError(updateResult.Message);
        }
    }    
}
