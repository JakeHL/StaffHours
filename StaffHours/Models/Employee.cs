using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StaffHours.Models
{
    public class Employee
    {
        public int Employee_ID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public string GetFullName()
        {
            return $"{FirstName} {Surname}";
        }
    }
}