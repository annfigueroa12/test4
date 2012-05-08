using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace hello_services.Services {
    [ServiceContract]
    public class Employees {

        private Data.NorthwindContextDataContext _context = new Data.NorthwindContextDataContext();
     
        [WebGet]
        public List<Models.Employee> Get() {

            var employees = from e in _context.Employees
                            select new Models.Employee {
                                Id = e.EmployeeID,
                                FirstName = e.FirstName,
                                LastName = e.LastName
                            };

            return employees.ToList();

        }

        [WebInvoke(Method="DELETE", UriTemplate="/{id}")]
        public void Delete(int id) {

            var employeeToDelete = (from e in _context.Employees
                                    where e.EmployeeID == id
                                    select e).SingleOrDefault();

            _context.Employees.DeleteOnSubmit(employeeToDelete);

            _context.SubmitChanges();

        }
    }
}