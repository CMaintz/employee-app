using BusinessLogic.BLL;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebGUI.Controllers
{
    public class EmployeeController : Controller
    {

        public ActionResult GetEmployee(int id)
        {
            EmployeeBLL employeeBLL = new EmployeeBLL();
            Employee model = employeeBLL.getEmployee(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View("Employee", model);
        }
        public ActionResult EnterEmployee()
        {
            return View("Employee");
        }
        [HttpPost]
        public ActionResult AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                return View("Employee", null);
            }
            else if (ModelState.IsValid)
            {
                EmployeeBLL employeeBLL = new EmployeeBLL();
                employeeBLL.addEmployee(employee);
                return View("EmployeeAdded", employee);
            } else
            {
                return View("Employee");
            }
        }
    }
}