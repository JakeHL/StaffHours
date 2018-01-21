using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StaffHours.Models
{
    public class Shifts
    {
        public int Shift_ID { get; set; }
        public DateTime Shift_Start { get; set; }
        public DateTime Shift_End { get; set; }
        public string Shift_Name { get; set; }
    }
}