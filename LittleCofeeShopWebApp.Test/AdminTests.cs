using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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

        [TestMethod]
        public void Can_Edit_Product()
        {
            // Arrange - create the mock repository
            Mock<ICofeeRepository> mock = new Mock<ICofeeRepository>();
            var volumeOptions = new VolumeOption[]{ new VolumeOption {
                    VolumeOptionId = 1,
                    Size = 0.133M,
                    Unit = new Unit {Name = "Litre" } } };
            var sugarOptions = new SugarOption[] { new SugarOption { SugarOptionId = 1, Size = 1, Price = 0, Unit = new Unit { Name = "Teaspoon" } } };
            var espresso = new Cofee
            {
                CofeeId = 1,
                Name = "Espresso",
                PriceCoeff = 2.0M,
                VolumeOptions = volumeOptions,
                SugarOptions = sugarOptions
            };

            var americano = new Cofee
            {
                CofeeId = 2,
                Name = "Americano",
                PriceCoeff = 3.0M,
                VolumeOptions = volumeOptions,
                SugarOptions = sugarOptions
            };

            mock.Setup(m => m.Products).Returns(new Cofee[] {
                espresso, americano
                }.AsQueryable());

            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Act
            Cofee c1 = target.Edit(1).ViewData.Model as Cofee;
            Cofee c2 = target.Edit(2).ViewData.Model as Cofee;
            // Assert
            Assert.AreEqual(1, c1.CofeeId);
            Assert.AreEqual(2, c2.CofeeId);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Product()
        {
            // Arrange - create the mock repository
            Mock<ICofeeRepository> mock = new Mock<ICofeeRepository>();
            var volumeOptions = new VolumeOption[]{ new VolumeOption {
                    VolumeOptionId = 1,
                    Size = 0.133M,
                    Unit = new Unit {Name = "Litre" } } };
            var sugarOptions = new SugarOption[] { new SugarOption { SugarOptionId = 1, Size = 1, Price = 0, Unit = new Unit { Name = "Teaspoon" } } };
            var espresso = new Cofee
            {
                CofeeId = 1,
                Name = "Espresso",
                PriceCoeff = 2.0M,
                VolumeOptions = volumeOptions,
                SugarOptions = sugarOptions
            };

            var americano = new Cofee
            {
                CofeeId = 2,
                Name = "Americano",
                PriceCoeff = 3.0M,
                VolumeOptions = volumeOptions,
                SugarOptions = sugarOptions
            };

            mock.Setup(m => m.Products).Returns(new Cofee[] {
                espresso, americano
                }.AsQueryable());
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Act
            Cofee result = (Cofee)target.Edit(4).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Arrange - create mock repository
            Mock<ICofeeRepository> mock = new Mock<ICofeeRepository>();
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Arrange - create a product
            Cofee cofee = new Cofee { Name = "Test" };
            // Act - try to save the product
            ActionResult result = target.Edit(cofee);
            // Assert - check that the repository was called
            mock.Verify(m => m.SaveProduct(cofee));
            // Assert - check the method result type
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange - create mock repository
            Mock<ICofeeRepository> mock = new Mock<ICofeeRepository>();
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Arrange - create a product
            Cofee cofee = new Cofee { Name = "Test" };
            // Arrange - add an error to the model state
            target.ModelState.AddModelError("error", "error");
            // Act - try to save the product
            ActionResult result = target.Edit(cofee);
            // Assert - check that the repository was not called
            mock.Verify(m => m.SaveProduct(It.IsAny<Cofee>()), Times.Never());
            // Assert - check the method result type
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
