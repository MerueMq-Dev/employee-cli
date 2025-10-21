using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCLI.Data
{

    public static class DatabaseInitializer
    {
        public static void EnsureDatabaseAndTableExist(string masterConnectionString, string employeeConnectionString)
        {
            using var connection = new SqlConnection(masterConnectionString);
            connection.Open();
            
            var createDb = @"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'EmployeeDB')
                         BEGIN
                             CREATE DATABASE EmployeeDB;
                         END";
            connection.Execute(createDb);

          
            using var employeeConnection = new SqlConnection(employeeConnectionString);
            employeeConnection.Open();

            var createTable = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Employees' AND xtype='U')
                            BEGIN
                                CREATE TABLE Employees (
                                    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
                                    FirstName NVARCHAR(50) NOT NULL,
                                    LastName NVARCHAR(50) NOT NULL,
                                    Email NVARCHAR(100) NOT NULL,
                                    DateOfBirth DATE NOT NULL,
                                    Salary DECIMAL(18,2) NOT NULL
                                );
                            END";
            employeeConnection.Execute(createTable);
        }
    }
}
