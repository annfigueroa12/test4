using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace hello_kendo_ui.Controllers {

    // Inheriting from the APIController class will designate this as a WebAPI endpoint
    public class EmployeesController : ApiController {

        // the linq to sql context that provides the data access layer
        private Data.NorthwindContextDataContext _context = new Data.NorthwindContextDataContext();
        // the current http request being made
        HttpRequest _request = HttpContext.Current.Request;

        // WebAPI will respond to an HTTP GET with this method
        public Models.Response Get() {

            // the the take and skip parameters off of the incoming request
            int take = _request["take"] == null ? 10 : int.Parse(_request["take"]);
            int skip = _request["skip"] == null ? 0 : int.Parse(_request["skip"]);

            // get all of the records from the employees table in the
            // northwind database.  return them in a collection of user
            // defined model objects for easy serialization. skip and then
            // take the appropriate number of records for paging.
            var employees = (from e in _context.Employees
                             select new Models.Employee(e)).Skip(skip).Take(take).ToArray();

            // returns the generic response object which will contain the 
            // employees array and the total count
            return new Models.Response(employees, _context.Employees.Count());
        }

        public HttpResponseMessage Post(int id) {

            // create a response message to send back
            var response = new HttpResponseMessage();

            try {
                // select the employee from the database where the id
                // matches the one passed in at api/employees/id
                var employeeToUpdate = (from e in _context.Employees
                                        where e.EmployeeID == id
                                        select e).FirstOrDefault();

                // if there was an employee returned from the database
                if (employeeToUpdate != null) {
                    
                    // update the employee object handling null values or empty strings
                    employeeToUpdate.LastName = string.IsNullOrEmpty(_request["LastName"]) ? employeeToUpdate.LastName : _request["LastName"];
                    employeeToUpdate.Address = string.IsNullOrEmpty(_request["Address"]) ? employeeToUpdate.Address : _request["Address"];
                    employeeToUpdate.City = string.IsNullOrEmpty(_request["City"]) ? employeeToUpdate.City : _request["City"];
                    employeeToUpdate.BirthDate = string.IsNullOrEmpty(_request["BirthDate"]) ? employeeToUpdate.BirthDate : Convert.ToDateTime(_request["BirthDate"]);

                    // submit the changes to the database
                    _context.SubmitChanges();

                    // set the server response to OK
                    response.StatusCode = HttpStatusCode.OK;

                } else {
                    // we couldn't find the employee with the passed in id
                    // set the response status to error and return a message
                    // with some more info.
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Content = new StringContent(string.Format("The employee with id {0} was not found in the database", id.ToString()));
                }
            } catch (Exception ex) {
                // something went wrong - possibly a database error. return a
                // 500 server error and send the details of the exception.
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Content = new StringContent(string.Format("The database updated failed: {0}", ex.Message));
            }

            // return the HTTP Response.
            return response;
        }

        public Models.Response Delete(int id) {
            try {
                // retrieve the employee to update from the database
                // based on the parameter passed in from api/employees/id
                var employeeToDelete = (from e in _context.Employees
                                        where e.EmployeeID == id
                                        select e).FirstOrDefault();

                // if a valid employee object was found by id
                if (employeeToDelete != null) {
                    // mark the object for deletion
                    _context.Employees.DeleteOnSubmit(employeeToDelete);
                    // delete the object from the database
                    _context.SubmitChanges();

                    // return an empty Models.Response object (this returns a 200 OK)
                    return new Models.Response();
                } else {
                    // otherwise set the error field of a response object and return it.
                    return new Models.Response(string.Format("The employee with id {0} was not found in the database", id.ToString()));
                }
            } catch (Exception ex) {
                // something went wrong. set the errors field of 
                return new Models.Response(string.Format("There was an error updating employee with id {0}: {1}", id.ToString(), ex.Message));
            }
        }           
    }
}