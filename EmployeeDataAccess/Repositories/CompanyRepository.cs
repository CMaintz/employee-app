using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using DTO.Model;
using EmployeeDataAccess.Contexts;
using EmployeeDataAccess.Mappers;
using EmployeeDataAccess.Model;

namespace EmployeeDataAccess.Repositories
{
    public class CompanyRepository
    {
        public static DTO.Model.Company getCompany(int id)
        {
            using (EmployeeContext context = new EmployeeContext())
            {
                return CompanyMapper.Map(context.Companies.Find(id));
            }
        }

        public static List<DTO.Model.Employee> getCompanyEmployees(int id)
        {
            using (EmployeeContext context = new EmployeeContext())
            {
                DTO.Model.Company company = CompanyMapper.Map(context.Companies.Find(id));
                return company == null ? new List<DTO.Model.Employee>() : company.Employees.ToList();
            }
        }

        public static void addCompany(DTO.Model.Company company)
        {
            using (EmployeeContext context = new EmployeeContext())
            {
                //TODO: throw if not found?
                context.Companies.Add(CompanyMapper.Map(company));
                context.SaveChanges();
            }

        }

        public static void addEmployeeToCompany(int companyId, DTO.Model.Employee employee)
        {
            using (EmployeeContext context = new EmployeeContext())
            {
                //  Company company = context.Companies.Find(c => c.CompanyId == companyId);
                var company = context.Companies.Include("Employees").FirstOrDefault(c => c.CompanyId == companyId);

                if (company != null && employee != null)
                {
                    company.Employees.Add(EmployeeMapper.Map(employee));
                    employee.CompanyId = companyId;
                    context.SaveChanges();
                }
            }
        }
        public static void addEmployeeToCompany(int companyId, int employeeId)
        {
            using (var context = new EmployeeContext())
            {
                var company = context.Companies.Include("Employees").FirstOrDefault(c => c.CompanyId == companyId);
                var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);

                if (company != null && employee != null)
                {
                    company.Employees.Add(employee);
                    employee.CompanyId = companyId;
                    context.SaveChanges();
                }
            }
        }
        public static void addEmployeeCompany(int companyId, List<DTO.Model.Employee> employees)
        {
            using (EmployeeContext context = new EmployeeContext())
            {
                //   Model.Company company = context.Companies.Where(c => c.CompanyId == companyId).FirstOrDefault();
                //   Model.Company company = context.Companies.Where(c => c.CompanyId == companyId).Include(c => c.Employees).FirstOrDefault(c => c.CompanyId == companyId);
                Model.Company company = context.Companies.Include(c => c.Employees).FirstOrDefault(c => c.CompanyId == companyId);
                foreach (DTO.Model.Employee employee in employees)
                {
                    Model.Employee e = context.Employees.Find(employee.EmployeeId);
                    employee.CompanyId = companyId;
                    e.CompanyId = companyId;
                    company.Employees.Add(e);
                }
                context.SaveChanges();
            }
        }
        public static List<DTO.Model.Company> getAllCompanies()
        {
            using (var context = new EmployeeContext()) {

                List<Model.Company> tempList = context.Companies.ToList();
                List<DTO.Model.Company> toReturn = new List<DTO.Model.Company>();

                foreach (Model.Company c in tempList)
                {
                    toReturn.Add(CompanyMapper.Map(c));
                }
                return toReturn;
            }
        }
    }
}
