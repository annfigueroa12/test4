using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hello_kendo_ui_part_1.Models {

    public class Employee {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Employee(Data.Employee employee) {

            this.Id = employee.EmployeeID;
            this.FirstName = employee.FirstName;
            this.LastName = employee.LastName;
        
        }
    }
}
