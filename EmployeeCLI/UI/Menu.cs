namespace EmployeeCLI.UI;

using EmployeeCLI.Exceptions;
using EmployeeCLI.Services;
using EmployeeCLI.UI.MenuCommands;
using System;

public class Menu
{
    private readonly EmployeeService _service;
    private readonly List<IMenuCommand> _commands;
    private bool _running = true;

    public Menu(EmployeeService service)
    {        
        _commands = new List<IMenuCommand>
        {
            new AddEmployeeMenuCommand(service),
            new ViewAllEmployeesMenuCommand(service),
            new UpdateEmployeeMenuCommand(service),
            new DeleteEmployeeMenuCommand(service),
            new SalaryStatisticsMenuCommand(service),
            new ExitMenuCommand(service)
        }.OrderBy(c => c.Id).ToList();
        _service = service;
    }

    public async Task Run()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        while (_running)
        {
            try
            {
                ShowMainMenu();

                Console.Write("\nВыберите команду: ");
                string? input = Console.ReadLine()?.Trim();

                if (int.TryParse(input, out int choice))
                {
                    var command = _commands.Find(c => c.Id == choice);
                    if (command != null)
                    {
                        await command.Execute();
                    }
                    else
                    {
                        Console.WriteLine("\n✗ Неверный выбор. Попробуйте снова.");
                    }
                }
                else
                {
                    Console.WriteLine("\n✗ Введите корректный номер команды.");
                }              

                if (_running)
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
            }
            catch (ExitMenuException ex)
            {
                _running = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Непредвиденная ошибка: {ex.Message}");
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }

    private void ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════════════╗");
        Console.WriteLine("║     СИСТЕМА УПРАВЛЕНИЯ СОТРУДНИКАМИ            ║");
        Console.WriteLine("╠════════════════════════════════════════════════╣");

        foreach (var cmd in _commands)
        {
            Console.WriteLine($"║  {cmd.Id}. {cmd.Title,-42} ║");
        }

        Console.WriteLine("╚════════════════════════════════════════════════╝");
    }
}