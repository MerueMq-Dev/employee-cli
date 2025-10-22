using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCLI.Data
{
    public class DatabaseBootstrapper
    {
        private readonly string _masterConnectionString;
        private readonly string _employeeConnectionString;

        public DatabaseBootstrapper(string master, string employee)
        {
            _masterConnectionString = master;
            _employeeConnectionString = employee;
        }

        public bool TryInitialize(int maxAttempts = 5)
        {
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    DatabaseInitializer.EnsureDatabaseAndTableExist(_masterConnectionString, _employeeConnectionString);
                    Console.WriteLine("✓ База данных успешно инициализирована.");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Попытка {attempt}/{maxAttempts}: {ex.Message}");
                    Thread.Sleep(2000);
                }
            }

            Console.WriteLine("⚠️ Не удалось подключиться к SQL Server после нескольких попыток.");
            return false;
        }
    }
}
