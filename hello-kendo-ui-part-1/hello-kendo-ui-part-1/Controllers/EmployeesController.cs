using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Collections;

namespace hello_kendo_ui_part_1.Controllers {
    
    public class EmployeesController : ApiController {

        Data.NorthwindContextDataContext _context = new Data.NorthwindContextDataContext();
        HttpRequest request = HttpContext.Current.Request;

        public Models.Response Get() {

            // simulate some latency for a query on a large set of data
            System.Threading.Thread.Sleep(1000);

            // get the take and skip parameters
            int skip = request["skip"] == null ? 0 : int.Parse(request["skip"]);
            int take = request["take"] == null ? 10 : int.Parse(request["take"]);

            // select the employees from the database, skipping and taking the correct amount
            var employees = (from e in _context.Employees
                             select new Models.Employee(e)).Skip(skip).Take(take).ToArray();

            return new Models.Response(employees, _context.Employees.Count());
        }
    }
}