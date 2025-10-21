using EmployeeCLI.Data;
using EmployeeCLI.Models;
using EmployeeCLI.UI;
using System.Threading.Tasks;

namespace EmployeeCLI.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> AddEmployee(CreateEmployee createEmployee)
    {
        try
        {            
            if (string.IsNullOrWhiteSpace(createEmployee.FirstName))
                return OperationResult.Failure("Имя не может быть пустым");

            if (string.IsNullOrWhiteSpace(createEmployee.LastName))
                return OperationResult.Failure("Фамилия не может быть пустой");

            if (createEmployee.Salary <= 0)
                return OperationResult.Failure("Зарплата должна быть положительной");

            await _repository.Add(createEmployee);
            return OperationResult.Success("Сотрудник успешно добавлен");
        }
        catch (Exception ex)
        {
            return OperationResult<Employee>.Failure($"Ошибка при добавлении: {ex.Message}");
        }

    }

    public async Task<OperationResult<IEnumerable<Employee>>> GetAllEmployees()
    {
        try
        {
            IEnumerable<Employee> employees = await _repository.GetAll();
            return OperationResult<IEnumerable<Employee>>.Success(employees);
        }
        catch (Exception ex)
        {
            return OperationResult<IEnumerable<Employee>>.Failure($"Ошибка при загрузке: {ex.Message}");
        }
    }

    public async Task<OperationResult<Employee>> GetEmployeeById(int id)
    {
        try
        {
            var employee =  await _repository.GetById(id);
            if (employee == null)
                return OperationResult<Employee>.Failure($"Сотрудник с ID {id} не найден");

            return OperationResult<Employee>.Success(employee);
        }
        catch (Exception ex)
        {
            return OperationResult<Employee>.Failure($"Ошибка: {ex.Message}");
        }
    }

    public async Task<OperationResult> UpdateEmployee(UpdateEmployee updateEmployee)
    {
        var employee = await _repository.GetById(updateEmployee.EmployeeID);
        if (employee == null)
            return OperationResult.Failure($"Сотрудник с ID {updateEmployee.EmployeeID} не найден");

        if (updateEmployee.Email != null)
        {
            var employeeWithEmail = await _repository.GetByEmail(employee.Email);
            if (employeeWithEmail != null && employee.EmployeeID != employeeWithEmail.EmployeeID)
            {
                return OperationResult.Failure($"Сотрудник с Email {updateEmployee.Email} уже существует");
            }
        }

        await _repository.Update(updateEmployee);
        return OperationResult.Success("Сотрудник успешно обновлён");
    }

 

    public async Task<OperationResult> DeleteEmployee(int id)
    {
        try
        {
            var employee = await _repository.GetById(id);
            if (employee == null)
                return OperationResult.Failure($"Сотрудник с ID {id} не найден");

            await _repository.Delete(id);
            return OperationResult.Success("Сотрудник успешно удален");
        }
        catch (Exception ex)
        {
            return OperationResult.Failure($"Ошибка: {ex.Message}");
        }
    }

    public async Task<OperationResult<SalaryStatistics>> GetSalaryStatistics()
    {
        try
        {
            var stats = new SalaryStatistics
            {
                AverageSalary = await _repository.GetAverageSalary(),
                CountAboveAverage = await _repository.GetCountAboveAverageSalary()
            };
            return OperationResult<SalaryStatistics>.Success(stats);
        }
        catch (Exception ex)
        {
            return OperationResult<SalaryStatistics>.Failure($"Ошибка: {ex.Message}");
        }
    }
}