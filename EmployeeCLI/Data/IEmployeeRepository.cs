using EmployeeCLI.Models;

namespace EmployeeCLI.Data;

public interface IEmployeeRepository
{
    public Task Add(CreateEmployee employee);
    public Task<IEnumerable<Employee>> GetAll();
    public Task<Employee?> GetById(int id);
    public Task<Employee?> GetByEmail(string email);
    public Task Update(UpdateEmployee updateEmployee);
    public Task Delete(int id);
    public Task<int> GetCountAboveAverageSalary();
    public Task<decimal> GetAverageSalary();
}