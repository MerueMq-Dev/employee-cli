using System.Data;

namespace EmployeeCLI.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
