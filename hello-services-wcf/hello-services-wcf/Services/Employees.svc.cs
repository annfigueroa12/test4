using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace hello_services_wcf.Services {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Employees" in code, svc and config file together.
    public class Employees : IEmployees {

        private Data.NorthwindContextDataContext _context = new Data.NorthwindContextDataContext();

        public List<Models.Employee> GetEmployees() {

            var employees = from e in _context.Employees
                            select new Models.Employee {
                                id = e.EmployeeID,
                                FirstName = e.FirstName,
                                LastName = e.LastName
                            };

            return employees.ToList();

        }


        public void DeleteEmployee(string id) {

            var employeeToDelete = (from c in _context.Employees
                                    where c.EmployeeID == Convert.ToInt32(id)
                                    select c).SingleOrDefault();

            _context.Employees.DeleteOnSubmit(employeeToDelete);

            _context.SubmitChanges();

        }
    }
}
