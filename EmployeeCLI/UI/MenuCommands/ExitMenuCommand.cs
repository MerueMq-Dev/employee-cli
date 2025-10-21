using EmployeeCLI.Exceptions;
using EmployeeCLI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCLI.UI.MenuCommands
{
    public class ExitMenuCommand(EmployeeService service) : BaseMenuCommand(service)
    {
        public override int Id => 0;
        public override string Title => "Выход";
          
        public async override Task Execute()
        {
            Console.WriteLine("\nЗавершение работы программы...");
            throw new ExitMenuException();
        }
    }
}
