// See https://aka.ms/new-console-template for more information

using Dapper;
using EmployeeCLI.Data;
using EmployeeCLI.Services;
using EmployeeCLI.UI;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

public class Program
{
    private static async Task Main(string[] args)
    {       
        string employeeConnectionString= @"Server=127.0.0.1,1433;Database=EmployeeDB;User Id=sa;Password=MyStrong!Password123;TrustServerCertificate=True;Encrypt=False;";
        string masterConnectionString = "Server=127.0.0.1,1433;Database=master;User Id=sa;Password=MyStrong!Password123;TrustServerCertificate=True";

        try
        {
            DatabaseInitializer.EnsureDatabaseAndTableExist(masterConnectionString, employeeConnectionString);

            IDbConnectionFactory connectionFactory = new SqlConnectionFactory(employeeConnectionString);
          
            IEmployeeRepository repository = new EmployeeRepository(connectionFactory);
                      
            var service = new EmployeeService(repository);
            var menu = new Menu(service);

            await menu.Run();

            Console.WriteLine("\nПрограмма завершена. До свидания!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Критическая ошибка: {ex.Message}");
            //Console.WriteLine("\nПодробности:");
            //Console.WriteLine(ex.StackTrace);
            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    } 
}
