using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.BLL;
using DTO.Model;

namespace WebGUI.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company

        //TODO: mangler at kunne tilføje folk til et firma, som så automatisk fjerner dem fra deres gamle. Og man skal også kunne fjerne uden at sætte et nyt..?

        public ActionResult GetCompany(int id)
        {
            CompanyBLL companyBLL = new CompanyBLL();
            Company model = companyBLL.getCompany(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View("Company", model);
        }
        public ActionResult EnterCompany()
        {
            return View("Company");
        }
        [HttpPost]
        public ActionResult AddCompany(Company company)
        {
            if (company == null)
            {
                return View("Company", null);
            }
            else if (ModelState.IsValid)
            {
                CompanyBLL companyBLL = new CompanyBLL();
                companyBLL.addCompany(company);
                return View("CompanyAdded");
            }
            else
            {
                return View("Company");
            }
        }
    }
}