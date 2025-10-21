using EmployeeCLI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCLI.UI.MenuCommands
{
    public class SalaryStatisticsMenuCommand : BaseMenuCommand
    {
        public override int Id => 5;
        public override string Title => "Статистика по зарплатам";

        public SalaryStatisticsMenuCommand(EmployeeService service) : base(service) { }

        public async override Task Execute()
        {
            PrintHeader("СТАТИСТИКА ПО ЗАРПЛАТАМ");

            var result =  await Service.GetSalaryStatistics();

            if (!result.IsSuccess)
            {
                PrintError(result.Message);
                return;
            }

            var stats = result.Data;
            Console.WriteLine($"Средняя зарплата: {stats.AverageSalary:C}");
            Console.WriteLine($"Сотрудников с зарплатой выше средней: {stats.CountAboveAverage}");
        }
    }
}
