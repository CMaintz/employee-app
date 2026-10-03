using BusinessLogic.BLL;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    public class EmployeeApiController : ApiController
    {
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public Employee GetEmployee(int id)
        {
            EmployeeBLL bll = new EmployeeBLL();
            Employee employee = bll.getEmployee(id);
            if (employee == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return employee;
        }
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public Employee GetEmployeeOne()
        {
            EmployeeBLL bll = new EmployeeBLL();
            return bll.getEmployee(1);
        }
        [HttpPost]
        public void AddEmployee([FromBody]Employee emp)
        {
            EmployeeBLL bll = new EmployeeBLL();
            bll.addEmployee(emp);
        }
    }
}
