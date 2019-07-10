using System;
using System.Collections.Generic;
using System.Linq;
using LittleCofeeShopWebApp.Controllers;
using LittleCofeeShopWebApp.Domain.Abstract;
using LittleCofeeShopWebApp.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LittleCofeeShopWebApp.Test
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            // Arrange - create the mock repository
            Mock<ICofeeRepository> mock = new Mock<ICofeeRepository>();
            mock.Setup(m => m.Products).Returns(new Cofee[] {
            new Cofee{
                CofeeId =1,
                Name ="Espresso",
                PriceCoeff = 2.0M,
                VolumeOptions = new VolumeOption[]{ new VolumeOption {
                    VolumeOptionId = 1,
                    Size = 0.133M,
                    Unit = new Unit {Name = "Litre" } } },
                SugarOptions = new SugarOption[]{ new SugarOption {SugarOptionId = 1,Size = 1,Price = 0, Unit = new Unit { Name = "Teaspoon" } } } }
                }.AsQueryable());
            // Arrange - create a controller
            AdminController target = new AdminController(mock.Object);
            // Action
            Cofee[] result = ((IEnumerable<Cofee>)target.Index().
            ViewData.Model).ToArray();
            // Assert
            Assert.AreEqual(result.Length, 1);
            Assert.AreEqual("Espresso", result[0].Name);
        }
    }
}
