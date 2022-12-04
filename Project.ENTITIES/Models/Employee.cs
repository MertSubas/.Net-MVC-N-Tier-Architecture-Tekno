using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Employee:BaseEntity
    {
        public Employee()
        {
            Role = UserRole.Employee;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TCNO { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public UserRole Role { get; set; }
        public decimal MonthlySales { get; set; }
        public decimal Salary { get; set; }


        //Relational Properties 
        public virtual AppUser AppUser { get; set; }
    }
}
