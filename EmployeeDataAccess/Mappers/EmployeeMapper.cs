using DTO.Model;
using EmployeeDataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDataAccess.Mappers
{
    public class EmployeeMapper
    {
        public static DTO.Model.Employee Map(Model.Employee employee)
        {
            if (employee == null) return null;
            return new DTO.Model.Employee(employee.EmployeeId, employee.Name, employee.YearsEmployed, employee.CompanyId);
        }
        public static Model.Employee Map(DTO.Model.Employee employee)
        {
            if (employee == null) return null;
            return new Model.Employee(employee.EmployeeId, employee.Name, employee.YearsEmployed, employee.CompanyId);
        }


    }
}
