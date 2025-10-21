using EmployeeCLI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCLI.UI.MenuCommands
{
    public class ViewAllEmployeesMenuCommand : BaseMenuCommand
    {
        public override int Id => 2;
        public override string Title => "Просмотреть всех сотрудников";

        public ViewAllEmployeesMenuCommand(EmployeeService service) : base(service) { }

        public async override Task Execute()
        {
            PrintHeader("СПИСОК СОТРУДНИКОВ");

            var result = await Service.GetAllEmployees();

            if (!result.IsSuccess)
            {
                PrintError(result.Message);
                return;
            }

            var employees = result.Data;
            var employeeCount = employees.Count();

            if (employeeCount == 0)
            {
                Console.WriteLine("Список сотрудников пуст.");
                return;
            }

            foreach (var emp in employees)
            {
                Console.WriteLine(emp);
            }

            Console.WriteLine($"\nВсего сотрудников: {employeeCount}");
        }
    }
}
