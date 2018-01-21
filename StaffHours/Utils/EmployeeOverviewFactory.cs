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
            // Load up the data on instantiation
            RefreshData();
        }
        
        /// <summary>
        /// Provides a way to update the data when needed
        /// </summary>
        public void RefreshData()
        {
            Employees = DataAccess.GetEmployees();
            Shifts = DataAccess.GetShifts();
            EmployeeShifts = DataAccess.GetEmployee_Works_Shift();
        }

        // Returns an overview for every employee
        public IEnumerable<EmployeeOverview> GetAllOverviews()
        {
            foreach (var employee in Employees)
                yield return GetOverview(employee.Employee_ID);
        }

        // returns an overview for a specific employee
        public EmployeeOverview GetOverview(int employeeID)
        {
            // Get the first (and only) employee with matching ID
            Employee employee = (from e in Employees
                                 where e.Employee_ID == employeeID
                                 select e).FirstOrDefault();

            // If an employee with the specified ID exists...
            if (employee != null)
            {
                // Get each shift worked by the employee
                var shifts = GetShiftsWorkedByEmployee(employeeID);
                // Calculate the total hours from all shifts collected
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
            // get all Employee_Works_Shift that has an employee_Id of employeeID
            var employeeShifts = (from es in EmployeeShifts
                                  where es.Employee_ID == employeeID
                                  select es).ToList();
            // Get every shift that has a shift ID that occurs in employeeShifts
            return (from s in Shifts
                    join es in employeeShifts
                    on s.Shift_ID equals es.Shift_ID
                    select s).ToList();
        }

        public double GetTotalShiftHours(List<Shifts> shifts)
        {
            // for each shift in shifts, add the difference in hours between the start and end of a shift
            return shifts.Sum(x => GetShiftHours(x.Shift_Start, x.Shift_End));
        }

        public double GetShiftHours(DateTime start, DateTime end)
        {
            // get the difference between two datetimes.
            TimeSpan span = end - start;
            // return that difference in hours
            return span.TotalHours;
        }
    }
}