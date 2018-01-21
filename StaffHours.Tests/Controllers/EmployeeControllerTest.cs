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
    public class EmployeeControllerTest
    {
        /* Currently this test project shares a connection string with the main project. Because of this,
         * This test may fail in the event that a record is added or removed from the DB in between GetEmployees() & controller.Get()
         * given more time, the test project could provide it's own connection string that will assure data is not modified
         */
        [TestMethod]
        public void Get()
        {
            // Arrange
            EmployeeController controller = new EmployeeController();
            List<Employee> employees = Utils.DataAccess.GetEmployees();

            // Act
            IEnumerable<Employee> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(employees.Count, result.Count());
        }
    }
}
