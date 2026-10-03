using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Model;
using EmployeeDataAccess.Model;

namespace EmployeeDataAccess.Mappers
{
    public class CompanyMapper
    {
        public static Model.Company Map(DTO.Model.Company company)
        {
            if (company == null) return null;
            return new Model.Company
            {
                CompanyId = company.CompanyId,
                Name = company.Name,
                Employees = company.Employees.Select(e => EmployeeMapper.Map(e)).ToList()
            };
        }

        public static DTO.Model.Company Map(Model.Company company)
        {
            if (company == null) return null;
            return new DTO.Model.Company
            {
                CompanyId = company.CompanyId,
                Name = company.Name,
                Employees = company.Employees.Select(e => EmployeeMapper.Map(e)).ToList()
            };
        }
    }
}
