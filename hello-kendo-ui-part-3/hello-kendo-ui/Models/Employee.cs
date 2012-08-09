using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hello_kendo_ui.Models {

    public class Employee {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime BirthDate { get; set; }
        public string City { get; set; }

        public Employee(hello_kendo_ui.Data.Employee employee) {
            this.Id = employee.EmployeeID;
            this.FirstName = employee.FirstName;
            this.LastName = employee.LastName;
            this.Title = employee.Title;
            this.BirthDate = employee.BirthDate;
            this.City = employee.City;
        }
    }

}
