using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace hello_services_wcf.Services {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmployees" in both code and config file together.
    [ServiceContract]
    public interface IEmployees {
        [OperationContract]
        [WebGet(UriTemplate="/", ResponseFormat=WebMessageFormat.Json)]
        List<Models.Employee> GetEmployees();


        [OperationContract]
        [WebInvoke(Method="DELETE", UriTemplate="/{id}")]
        void DeleteEmployee(string id);
    }
}
