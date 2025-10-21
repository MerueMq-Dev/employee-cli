using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCLI.Models
{
    public class UpdateEmployee
    {
        public int EmployeeID { get; set; } 
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public decimal? Salary { get; set; }
      
    }
}
