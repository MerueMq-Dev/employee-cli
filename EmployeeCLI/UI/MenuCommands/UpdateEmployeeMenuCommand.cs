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

            var employee = result.Data;
            Console.WriteLine("\nТекущие данные:");
            Console.WriteLine(employee);
            
            var updateEmployee = new UpdateEmployee { EmployeeID = id };

            Console.WriteLine("\nВведите новые значения (Enter = пропустить):\n");
            
            Console.Write($"Имя [{employee.FirstName}]: ");
            var firstName = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(firstName))
                updateEmployee.FirstName = firstName;

            Console.Write($"Фамилия [{employee.LastName}]: ");
            var lastName = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(lastName))
                updateEmployee.LastName = lastName;


            Console.Write($"Email [{employee.Email}]: ");
            var email = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(email))
                updateEmployee.Email = email;

            Console.Write($"Дата рождения [{employee.DateOfBirth:dd.MM.yyyy}] (ДД.ММ.ГГГГ): ");
            var dobStr = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(dobStr) &&
                DateTime.TryParseExact(dobStr, "dd.MM.yyyy", null,
                    System.Globalization.DateTimeStyles.None, out var dob))
            {
                updateEmployee.DateOfBirth = dob;
            }
            
            Console.Write($"Зарплата [{employee.Salary:N2}]: ");
            var salaryStr = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(salaryStr) && decimal.TryParse(salaryStr, out var salary))
                updateEmployee.Salary = salary;
            
            if (updateEmployee.FirstName == null &&
                updateEmployee.LastName == null &&
                updateEmployee.Email == null &&
                updateEmployee.DateOfBirth == null &&
                updateEmployee.Salary == null)
            {
                Console.WriteLine("\nНичего не изменено.");
                return;
            }

            // Отправляем на обновление
            var updateResult =  await Service.UpdateEmployee(updateEmployee);

            if (updateResult.IsSuccess)
                PrintSuccess(updateResult.Message);
            else
                PrintError(updateResult.Message);
        }
    }
    
}
