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
    public class CompanyApiController : ApiController
    {
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public Company GetCompany(int id)
        {
            CompanyBLL bll = new CompanyBLL();
            Company company = bll.getCompany(id);
            if (company == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return company;
        }
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public Company GetCompanyOne()
        {
            CompanyBLL bll = new CompanyBLL();
            return bll.getCompany(1);
        }
        [HttpPost]
        public void AddCompany([FromBody] Company com)
        {
            CompanyBLL bll = new CompanyBLL();
            bll.addCompany(com);
        }
    }
}
