using StaffHours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StaffHours.Utils
{
    public class EmployeeOverviewFactory
    {
        private List<Employee> Employees;
        private List<Shifts> Shifts;
        private List<Employee_Works_Shift> EmployeeShifts;

        public EmployeeOverviewFactory()
        {
            RefreshData();
        }
        
        public void RefreshData()
        {
            Employees = DataAccess.GetEmployees();
            Shifts = DataAccess.GetShifts();
            EmployeeShifts = DataAccess.GetEmployee_Works_Shift();
        }

        public IEnumerable<EmployeeOverview> GetAllOverviews()
        {
            foreach (var employee in Employees)
                yield return GetOverview(employee.Employee_ID);
        }

        public EmployeeOverview GetOverview(int employeeID)
        {
            Employee employee = (from e in Employees
                                 where e.Employee_ID == employeeID
                                 select e).FirstOrDefault();

            if (employee != null)
            {
                var shifts = GetShiftsWorkedByEmployee(employeeID);
                double totalHours = GetTotalShiftHours(shifts);

                return new EmployeeOverview()
                {
                    FullName = employee.GetFullName(),
                    ID = employee.Employee_ID,
                    TotalHours = totalHours                    
                };
            }
            else
            {
                throw new Exception($"No employee with id: {employeeID}");
            }
        }

        public List<Shifts> GetShiftsWorkedByEmployee(int employeeID)
        {
            var employeeShifts = (from es in EmployeeShifts
                                  where es.Employee_ID == employeeID
                                  select es).ToList();

            return (from s in Shifts
                    join es in employeeShifts
                    on s.Shift_ID equals es.Shift_ID
                    select s).ToList();
        }

        public double GetTotalShiftHours(List<Shifts> shifts)
        {
            return shifts.Sum(x => GetShiftHours(x.Shift_Start, x.Shift_End));
        }

        public double GetShiftHours(DateTime start, DateTime end)
        {
            TimeSpan span = end - start;
            return span.TotalHours;
        }
    }
}