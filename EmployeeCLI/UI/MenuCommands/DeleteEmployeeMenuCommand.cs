using EmployeeCLI.Services;
using EmployeeCLI.UI.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCLI.UI.MenuCommands
{
    public class DeleteEmployeeMenuCommand : BaseMenuCommand
    {
        public override int Id => 4;
        public override string Title => "Удалить сотрудника";

        public DeleteEmployeeMenuCommand(EmployeeService service) : base(service) { }

        public async override Task Execute()
        {
            PrintHeader("УДАЛЕНИЕ СОТРУДНИКА");

            int id = InputReader.Read("Id: ", new PositiveIntValidator(), s => int.Parse(s));

            var result = await Service.GetEmployeeById(id);
            if (!result.IsSuccess)
            {
                PrintError(result.Message);
                return;
            }

            var employee = result.Data;
            Console.WriteLine("\nДанные сотрудника:");
            Console.WriteLine(employee);
           
            if (InputReader.ReadYesNo("\nВы уверены, что хотите удалить этого сотрудника?"))
            {
                var deleteResult = await Service.DeleteEmployee(id);
                if (deleteResult.IsSuccess)
                    PrintSuccess(deleteResult.Message);
                else
                    PrintError(deleteResult.Message);
            }
            else
            {
                Console.WriteLine("\nОтменено.");
            }
        }
    }
}
