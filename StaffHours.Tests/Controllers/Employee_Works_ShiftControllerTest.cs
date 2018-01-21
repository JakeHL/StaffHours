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

namespace StaffHours.Tests.Controllers
{
    [TestClass]
    public class Employee_Works_ShiftControllerTest
    {
        /* Currently this test project shares a connection string with the main project. Because of this,
         * This test may fail in the event that a record is added or removed from the DB in between GetEmployee_Works_Shifts() & controller.Get()
         * given more time, the test project could provide it's own connection string that will assure data is not modified
         */
        [TestMethod]
        public void Get()
        {
            // Arrange
            Employee_Works_ShiftController controller = new Employee_Works_ShiftController();
            List<Employee_Works_Shift> Employee_Works_Shift = Utils.DataAccess.GetEmployee_Works_Shift();

            // Act
            IEnumerable<Employee_Works_Shift> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(Employee_Works_Shift.Count, result.Count());
        }
    }
}
