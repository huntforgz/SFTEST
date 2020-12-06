using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SFTEST;
using SFTEST.Controllers;
using SFTEST.DAO;
using SFTEST.Models;

namespace SFTEST.Tests.Controllers
{
    [TestClass]
    public class EmpoyeeControllerTest
    {
        
        [TestMethod]
        public void tests()


        {
            var data = new List<Employee>
            {
                new Employee{ID = 1, FirstName="Hunt1",LastName="Huang1",HiredDate=DateTime.Parse("2020-12-01")},
                new Employee{ID = 2, FirstName="Hunt2",LastName="Huang2",HiredDate=DateTime.Parse("2020-12-01")}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(c => c.Employees).Returns(mockSet.Object);

            // Arrange
            var controller = new EmployeeController(mockContext.Object);
            
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);

            // Act
            var details = controller.Details(null) ;

            // Assert
            Assert.AreEqual(details.GetType(), typeof(HttpStatusCodeResult));

            // Act
            var detailsWithResult = controller.Details(1);

            // Assert
            Assert.IsNotNull(detailsWithResult);

            // Act
            var createResult = controller.Create(
                new Employee { ID = 3, FirstName = "Hunt1", LastName = "Huang1", HiredDate = DateTime.Parse("2020-12-01")});
            // Assert
            Assert.IsNotNull(createResult);
            mockSet.Verify(m => m.Add(It.IsAny<Employee>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        
    }
}
