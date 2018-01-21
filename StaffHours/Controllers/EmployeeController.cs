using StaffHours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StaffHours.Controllers
{
    public class EmployeeController : ApiController
    {
        // GET api/Employee
        public IEnumerable<Employee> Get()
        {
            return Utils.DataAccess.GetEmployees();
        }
    }
}
