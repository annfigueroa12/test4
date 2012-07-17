using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace hello_services.Controllers {
    
    // Inheriting from the APIController class will designate this as a WebAPI endpoint
    public class EmployeesController : ApiController {


        // the linq to sql context that provides the data access layer
        private Data.NorthwindContextDataContext _context = new Data.NorthwindContextDataContext();
     
        // WebAPI will respond to an HTTP GET with this method
        public List<Models.Employee> Get() {

            // get all of the records from the employees table in the
            // northwind database.  return them in a collection of user
            // defined model objects for easy serialization.
            var employees = from e in _context.Employees
                            select new Models.Employee {
                                Id = e.EmployeeID,
                                FirstName = e.FirstName,
                                LastName = e.LastName
                            };

            // returns the employees as a list, which is converted
            // to an array during serialization to JSON
            return employees.ToList();

        }

        // WebAPI will respond to an HTTP DELETE with this method
        public void Delete(int id) {

            var employeeToDelete = (from e in _context.Employees
                                    where e.EmployeeID == id
                                    select e).SingleOrDefault();

            // mark the object for delete in the context
            _context.Employees.DeleteOnSubmit(employeeToDelete);

            // commit the change
            _context.SubmitChanges();

        }
    }
}