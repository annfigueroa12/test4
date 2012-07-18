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

            System.Threading.Thread.Sleep(1000);

            // the the take and skip parameters off of the incoming request
            int take = _request["take"] == null ? 10 : int.Parse(_request["take"]);
            int skip = _request["skip"] == null ? 0 : int.Parse(_request["skip"]);

            // get all of the records from the employees table in the
            // northwind database.  return them in a collection of user
            // defined model objects for easy serialization. skip and then
            // take the appropriate number of records for paging.
            var employees = (from e in _context.Employees
                             select new Models.Employee {
                                 Id = e.EmployeeID,
                                 FirstName = e.FirstName,
                                 LastName = e.LastName
                             }).Skip(skip).Take(take).ToArray();

            // returns the generic response object which will contain the 
            // employees array and the total count
            return new Models.Response(employees, _context.Employees.Count());

        }

        public HttpResponseMessage Post(int id) {

            // create a response message to send back
            var response = new HttpResponseMessage();

            try {
                var employeeToUpdate = (from e in _context.Employees
                                        where e.EmployeeID == id
                                        select e).FirstOrDefault();

                if (employeeToUpdate != null && _request["LastName"] != null) {
                    employeeToUpdate.LastName = _request["LastName"];
                    _context.SubmitChanges();

                    response.StatusCode = HttpStatusCode.OK;

                } else {
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Content = new StringContent(string.Format("The employee with id {0} was not found in the database", id.ToString()));
                }
            } catch (Exception ex) {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Content = new StringContent(string.Format("The database updated failed: {0}", ex.Message));
            }

            return response;
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