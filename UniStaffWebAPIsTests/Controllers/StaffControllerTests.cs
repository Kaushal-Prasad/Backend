using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniStaffWebAPIs.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniStaffWebAPIs.Controllers.Tests
{
    [TestClass()]
    public class StaffControllerTests
    {
        [TestMethod()]
        public void calculateStaffSalaryTest()
        {
            Assert.AreEqual(20000, StaffController.calculateStaffSalary(5,2));
            Assert.AreEqual(48000, StaffController.calculateStaffSalary(6, 4));
            Assert.AreEqual(84000, StaffController.calculateStaffSalary(7, 6));
            Assert.AreEqual(128000, StaffController.calculateStaffSalary(8, 8)); 
            Assert.AreEqual(80000, StaffController.calculateStaffSalary(5, 8));
            Assert.AreEqual(100000, StaffController.calculateStaffSalary(5, 10));
            Assert.AreEqual(210000, StaffController.calculateStaffSalary(7, 15));  
        }
    }
}