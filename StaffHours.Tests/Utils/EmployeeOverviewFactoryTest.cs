using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffHours;
using StaffHours.Controllers;
using StaffHours.Models;
using StaffHours.Utils;
using System.Data.SqlClient;

namespace StaffHours.Tests.Controllers
{
    [TestClass]
    public class EmployeeOverviewFactoryTest
    {
        [TestMethod]
        public void GetShiftHoursPreArranged()
        {
            // Arrange
            EmployeeOverviewFactory eof = new EmployeeOverviewFactory();
            DateTime startDate = new DateTime(2018, 01, 21, 09, 00, 00);
            DateTime endDate = new DateTime(2018, 01, 21, 17, 00, 00);

            // Act
            double result = eof.GetShiftHours(startDate, endDate);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, 8);
        }

        [TestMethod]
        public void GetTotalShiftHoursPreArranged()
        {
            // Arrange
            EmployeeOverviewFactory eof = new EmployeeOverviewFactory();
            List<Shifts> shifts = new List<Shifts>();
            for (int i = 1; i <= 28; i++)
                shifts.Add(new Shifts()
                {
                    Shift_Start = new DateTime(2018, 01, i, 09, 00, 00),
                    Shift_End = new DateTime(2018, 01, i, 17, 00, 00)
                });


            // Act
            double result = eof.GetTotalShiftHours(shifts);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, 224);
        }

        /* This test method assumes there is an Employee with an ID of 1
         * Given more time I would implement a stored procedure to return an ID that definitely exists
         */
        [TestMethod]
        public void GetTotalShiftHoursAgainstDatabase()
        {
            // Arange
            string query = @"SELECT SUM(DATEDIFF(Hour, S.Shift_Start, S.Shift_End)) as 'value' FROM dbo.Employee as E
                            INNER JOIN dbo.Employee_Works_Shift as EWS on E.Employee_ID = EWS.Employee_ID
                            INNER JOIN dbo.Shifts as S on EWS.Shift_ID = S.Shift_ID WHERE E.Employee_ID = 1";
            EmployeeOverviewFactory eof = new EmployeeOverviewFactory();

            // Act
            double calculatedHours = eof.GetTotalShiftHours(eof.GetShiftsWorkedByEmployee(1));
            double fromDBHours = 0;

            using (SqlConnection conn = new SqlConnection(DataAccess.ConnString))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand(query, conn))
                using (var reader = comm.ExecuteReader())
                    while (reader.Read())
                        fromDBHours = Convert.ToDouble(reader["value"]);
            }

            calculatedHours = Math.Floor(calculatedHours);
            fromDBHours = Math.Floor(fromDBHours);

            // Assert
            Assert.AreEqual(calculatedHours, fromDBHours);
        }
    }
}
