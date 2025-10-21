namespace EmployeeCLI.Models;

public class Employee
{
    public int EmployeeID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public decimal Salary { get; set; }

    public override string ToString()
    {
        return $"ID: {EmployeeID ,-5} | Имя:  {FirstName} {LastName,-15} | Почта: {Email,-30} | ДР: {DateOfBirth:dd.MM.yyyy} | Зарплата: {Salary:C}";
    }
}