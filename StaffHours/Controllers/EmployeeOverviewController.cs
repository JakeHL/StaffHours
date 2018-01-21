using StaffHours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StaffHours.Controllers
{
    public class EmployeeOverviewController : ApiController
    {
        // GET: api/EmployeeOverview
        public IEnumerable<EmployeeOverview> Get()
        {
            return new Utils.EmployeeOverviewFactory().GetAllOverviews();
        }

        // GET: api/EmployeeOverview/1
        public EmployeeOverview Get(int id)
        {
            return new Utils.EmployeeOverviewFactory().GetOverview(id);
        }
    }
}
