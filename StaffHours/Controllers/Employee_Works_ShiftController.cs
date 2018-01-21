using StaffHours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StaffHours.Controllers
{
    public class Employee_Works_ShiftController : ApiController
    {
        // GET api/Employee_Works_Shift
        public IEnumerable<Employee_Works_Shift> Get()
        {
            return Utils.DataAccess.GetEmployee_Works_Shift();
        }
    }
}
