using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.ENTITIES.Models;

namespace Project.MVCUI.ViewModels
{
    public class EmployeeVM
    {
        public Employee Employee { get; set; }

        public List<Employee> Employees { get; set; }
    }
}