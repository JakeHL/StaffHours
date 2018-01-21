using StaffHours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StaffHours.Controllers
{
    public class ShiftsController : ApiController
    {
        // GET api/shifts
        public IEnumerable<Shifts> Get()
        {
            return Utils.DataAccess.GetShifts();
        }
    }
}
