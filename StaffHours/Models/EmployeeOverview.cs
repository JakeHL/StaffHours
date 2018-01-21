using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StaffHours.Models
{
    public class EmployeeOverview
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public double TotalHours { get; set; }
    }
}