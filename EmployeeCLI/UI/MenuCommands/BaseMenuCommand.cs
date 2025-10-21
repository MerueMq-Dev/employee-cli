using EmployeeCLI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeCLI.UI.MenuCommands
{
    public abstract class BaseMenuCommand : IMenuCommand
    {
        protected readonly EmployeeService Service;

        protected BaseMenuCommand (EmployeeService service)
        {
            Service = service;
        }

        public abstract int Id { get; }
        public abstract string Title { get; }
        public abstract Task Execute();

        protected void PrintHeader(string title)
        {
            int totalWidth = 50;
            int padding = (totalWidth - title.Length - 2) / 2;

            Console.WriteLine();
            Console.WriteLine("╔" + new string('═', totalWidth - 2) + "╗");
            Console.WriteLine("║" + new string(' ', padding) + title +
                            new string(' ', totalWidth - title.Length - padding - 2) + "║");
            Console.WriteLine("╚" + new string('═', totalWidth - 2) + "╝");
            Console.WriteLine();
        }

        protected void PrintSuccess(string message)
        {
            Console.WriteLine($"\n✓ {message}");
        }

        protected void PrintError(string message)
        {
            Console.WriteLine($"\n✗ {message}");
        }
    }
}
