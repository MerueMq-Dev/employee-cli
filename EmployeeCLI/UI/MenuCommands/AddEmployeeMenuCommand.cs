using EmployeeCLI.Models;
using EmployeeCLI.Services;
using EmployeeCLI.UI.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCLI.UI.MenuCommands
{
    public class AddEmployeeMenuCommand : BaseMenuCommand
    {
        public AddEmployeeMenuCommand(EmployeeService service) : base(service) { }

        public override int Id => 1;
        public override string Title => "Добавить нового сотрудника";


        public async override Task Execute()
        {
            PrintHeader("ДОБАВЛЕНИЕ НОВОГО СОТРУДНИКА");

            var employee = new CreateEmployee
            {
                FirstName = InputReader.Read("Имя: ", new NonEmptyStringValidator(), s => s),
                LastName = InputReader.Read("Фамилия: ", new NonEmptyStringValidator(), s => s),
                Email = InputReader.Read("Email: ", new EmailValidator(), s => s),
                DateOfBirth = InputReader.Read("Дата рождения (ДД.ММ.ГГГГ): ", new DateValidator(), s => DateTime.Parse(s)),
                Salary = InputReader.Read("Зарплата: ", new PositiveDecimalValidator(), s => decimal.Parse(s))
            };
            
            var result = await Service.AddEmployee(employee);

            if (result.IsSuccess)
                PrintSuccess(result.Message);
            else
                PrintError(result.Message);
        }
    }
}
