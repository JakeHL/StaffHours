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
    public class ShiftsControllerTest
    {
        /* Currently this test project shares a connection string with the main project. Because of this,
         * This test may fail in the event that a record is added or removed from the DB in between GetShiftss() & controller.Get()
         * given more time, the test project could provide it's own connection string that will assure data is not modified
         */
        [TestMethod]
        public void Get()
        {
            // Arrange
            ShiftsController controller = new ShiftsController();
            List<Shifts> Shifts = Utils.DataAccess.GetShifts();

            // Act
            IEnumerable<Shifts> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(Shifts.Count, result.Count());
        }
    }
}
