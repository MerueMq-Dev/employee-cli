# 🧑‍💼 EmployeeCLI

Консольное приложение для управления сотрудниками, использующее **SQL Server** в Docker и библиотеку **Dapper** для доступа к данным.

---

## 🚀 Запуск проекта

### 1. Запустить базу данных в Docker

Перед первым запуском необходимо поднять контейнер с SQL Server:

```bash
docker-compose up -d
```
>💡 Убедитесь, что порт 1433 свободен на вашей машине.

После этого база данных EmployeeDB будет создана автоматически при первом запуске приложения (если ещё не существует).

### 2. Запустить консольное приложение
```bash
dotnet run --project EmployeeCLI
```
Приложение подключится к контейнеру employee-sql-server и создаст таблицу Employees, если её нет.



##  📂 Структура проекта
```bash
EmployeeCLI/
├── Data/
│   ├── DatabaseInitializer.cs      # Проверка и создание базы данных / таблиц
│   ├── SqlConnectionFactory.cs     # Фабрика подключений к БД
│   └── EmployeeRepository.cs       # Репозиторий для CRUD-операций
│
├── Services/
│   └── EmployeeService.cs          # Логика бизнес-операций с сотрудниками
│
├── UI/
│   ├── Menu.cs                     # Главное меню приложения
│   ├── MenuCommands/               # Отдельные команды меню
│   │   ├── AddEmployeeMenuCommand.cs
│   │   ├── ViewAllEmployeesMenuCommand.cs
│   │   ├── UpdateEmployeeMenuCommand.cs
│   │   ├── DeleteEmployeeMenuCommand.cs
│   │   ├── SalaryStatisticsMenuCommand.cs
│   │   └── ExitMenuCommand.cs
│   │
│   ├── Validators/                 # Валидаторы пользовательского ввода
│   │   ├── IValidator.cs
│   │   ├── EmailValidator.cs
│   │   ├── DateValidator.cs
│   │   ├── DecimalValidator.cs
│   │   ├── IntValidator.cs
│   │   └── NonEmptyStringValidator.cs
│   │
│   └── InputReader.cs              # Унифицированное чтение и валидация ввода
│
├── Models/
│   └── Employee.cs                 # Модель сотрудника
│
├── Exceptions/
│   └── ExitMenuException.cs        # Исключение для выхода из приложения
│
├── Program.cs                      # Точка входа, DI и запуск меню
└── docker-compose.yml              # Контейнер SQL Server
⚙️ Конфигурация подключения
В Program.cs определены две строки подключения:
```

```csharp
string employeeConnectionString = @"Server=127.0.0.1,1433;Database=EmployeeDB;User Id=sa;Password=MyStrong!Password123;TrustServerCertificate=True;Encrypt=False;";
```
```csharp
string masterConnectionString = @"Server=127.0.0.1,1433;Database=master;User Id=sa;Password=MyStrong!Password123;TrustServerCertificate=True;";
```

masterConnectionString — используется для создания базы данных EmployeeDB

employeeConnectionString — используется для работы с самой базой

## 💾 Функциональность
✅ Добавление, просмотр, обновление и удаление сотрудников 

📊 Просмотр статистики по зарплатам

🔒 Проверка корректности ввода (email, дата, зарплата и т.д.)

🧱 Автоматическое создание базы и таблицы при первом запуске

🚫 Обработка ошибок и устойчивость к сбоям



## 🖥️ Пример интерфейса
```bash
╔════════════════════════════════════════════════╗
║     СИСТЕМА УПРАВЛЕНИЯ СОТРУДНИКАМИ            ║
╠════════════════════════════════════════════════╣
║  1. Добавить нового сотрудника                 ║
║  2. Просмотреть всех сотрудников               ║
║  3. Обновить информацию о сотруднике           ║
║  4. Удалить сотрудника                         ║
║  5. Статистика по зарплатам                    ║
║  0. Выход                                      ║
╚════════════════════════════════════════════════╝

Выберите команду: 1

╔══════════════════════════════════════════╗
║      ДОБАВЛЕНИЕ НОВОГО СОТРУДНИКА        ║
╚══════════════════════════════════════════╝

Имя: Иван
Фамилия: Петров
Email: ivan.petrov@example.com
Дата рождения (ДД.ММ.ГГГГ): 12.05.1990
Зарплата: 85000

✓ Сотрудник успешно добавлен!
```

## 🧱 Требования
 - .NET 9.0 или новее
 - Docker и Docker Compose
 - SQL Server (через контейнер)