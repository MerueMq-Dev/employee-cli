using Dapper;
using EmployeeCLI.Models;
using Microsoft.Data.SqlClient;

namespace EmployeeCLI.Data;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public EmployeeRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }   

    public async Task Add(CreateEmployee employee)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            string sql = @"INSERT INTO Employees (FirstName, LastName, Email, DateOfBirth, Salary) 
                          VALUES (@FirstName, @LastName, @Email, @DateOfBirth, @Salary)";

           await connection.ExecuteAsync(sql, employee);
        }
    }

    public async Task<IEnumerable<Employee>> GetAll()
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            string sql = "SELECT * FROM Employees ORDER BY EmployeeID";
            return await connection.QueryAsync<Employee>(sql);
        }
    }

    public async Task<Employee?> GetById(int id)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            string sql = "SELECT * FROM Employees WHERE EmployeeID = @Id";
            return await connection.QueryFirstOrDefaultAsync<Employee>(sql, new { Id = id });
        }
    }

    public async Task<Employee?> GetByEmail(string email)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            string sql = "SELECT * FROM Employees WHERE Email = @Email";
            return await connection.QueryFirstOrDefaultAsync<Employee>(sql, new { Email = email });
        }
    }    

    public async Task Delete(int id)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            string sql = "DELETE FROM Employees WHERE EmployeeID = @Id";
             await connection.ExecuteAsync(sql, new { Id = id });
        }
    }

    public async Task<int> GetCountAboveAverageSalary()
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            string sql = @"SELECT COUNT(*) FROM Employees 
                          WHERE Salary > (SELECT AVG(Salary) FROM Employees)";
            return await connection.ExecuteScalarAsync<int>(sql);
        }
    }

    public async Task<decimal> GetAverageSalary()
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            string sql = "SELECT ISNULL(AVG(Salary), 0) FROM Employees";
            return await connection.ExecuteScalarAsync<decimal>(sql);
        }
    }

    public async Task Update(UpdateEmployee updateEmployee)
    {
        const string sql = @"
        UPDATE Employees
        SET
            FirstName   = COALESCE(@FirstName, FirstName),
            LastName    = COALESCE(@LastName, LastName),
            Email       = COALESCE(@Email, Email),
            DateOfBirth = COALESCE(@DateOfBirth, DateOfBirth),
            Salary      = COALESCE(@Salary, Salary)
        WHERE EmployeeID = @EmployeeID;
        ";

        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.ExecuteAsync(sql, new
            {
                updateEmployee.EmployeeID,
                updateEmployee.FirstName,
                updateEmployee.LastName,
                updateEmployee.Email,
                updateEmployee.DateOfBirth,
                updateEmployee.Salary
            });
        }           
    }
}