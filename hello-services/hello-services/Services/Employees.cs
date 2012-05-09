using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace hello_services.Services {

    // decorating a class as a service contract is still a must for
    // working with WCF and WebAPI
    [ServiceContract]
    public class Employees {

        // the linq to sql context that provides the data access layer
        private Data.NorthwindContextDataContext _context = new Data.NorthwindContextDataContext();
     
        // decorating the method with WebGet defines it as a web method that
        // responds to a GET
        [WebGet]
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

        // decorating a method as WebInvoke designates it as web accessbile
        // Method = the HTTP method to respond to
        // URITemplate = the formation of the url to respond to, including parameters
        [WebInvoke(Method="DELETE", UriTemplate="/{id}")]
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