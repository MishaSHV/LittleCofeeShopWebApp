using System;
using System.Linq;
using LittleCofeeShopWebApp.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LittleCofeeShopWebApp.Domain.Abstract;
using Moq;
using LittleCofeeShopWebApp.Controllers;
using System.Web.Mvc;

namespace LittleCofeeShopWebApp.Test
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            // Arrange - create some test products
            Recipe r1 = new Recipe { CofeeId = 1, CofeeName = "Espresso", Options = new CofeeOptions() };
            Recipe r2 = new Recipe { CofeeId = 2, CofeeName = "Latte", Options = new CofeeOptions() };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Act
            target.AddItem(r1, 1);
            target.AddItem(r2, 1);
            CartLine[] results = target.Lines.ToArray();
            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Recipe, r1);
            Assert.AreEqual(results[1].Recipe, r2);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Arrange - create some test products
            Recipe r1 = new Recipe { CofeeId = 1, CofeeName = "Espresso", Options = new CofeeOptions() };
            Recipe r2 = new Recipe { CofeeId = 2, CofeeName = "Latte", Options = new CofeeOptions() };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Act
            target.AddItem(r1, 1);
            target.AddItem(r2, 1);
            target.AddItem(r1, 10);
            CartLine[] results = target.Lines.OrderBy(c => c.Recipe.CofeeId).ToArray();
            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 11);
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            // Arrange - create some test products
            Recipe r1 = new Recipe { CofeeId = 1, CofeeName = "Espresso", Options = new CofeeOptions() };
            Recipe r2 = new Recipe { CofeeId = 2, CofeeName = "Latte", Options = new CofeeOptions() };
            Recipe r3 = new Recipe { CofeeId = 3, CofeeName = "Americano", Options = new CofeeOptions() };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Arrange - add some products to the cart
            target.AddItem(r1, 1);
            target.AddItem(r2, 3);
            target.AddItem(r3, 5);
            target.AddItem(r2, 1);
            // Act
            target.RemoveLine(r2);
            // Assert
            Assert.AreEqual(target.Lines.Where(c => c.Recipe== r2).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // Arrange - create some test products
            Recipe r1 = new Recipe { CofeeId = 1, CofeeName = "Espresso",CofeePriceCoef = 2.0M,VolumeSize = 0.133M,Options =new CofeeOptions() };
            Recipe r2 = new Recipe { CofeeId = 2, CofeeName = "Latte", CofeePriceCoef = 3.0M, VolumeSize = 0.250M, Options = new CofeeOptions() };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Act
            target.AddItem(r1, 1);
            target.AddItem(r2, 1);
            target.AddItem(r1, 3);
            decimal result = target.ComputeTotalValue();
            // Assert
            Assert.AreEqual(result, 1.814M);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Arrange - create some test products
            Recipe r1 = new Recipe { CofeeId = 1, CofeeName = "Espresso", CofeePriceCoef = 2.0M, VolumeSize = 0.133M };
            Recipe r2 = new Recipe { CofeeId = 2, CofeeName = "Latte", CofeePriceCoef = 3.0M, VolumeSize = 0.250M };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Arrange - add some items
            target.AddItem(r1, 1);
            target.AddItem(r2, 1);
            // Act - reset the cart
            target.Clear();
            // Assert
            Assert.AreEqual(target.Lines.Count(), 0);
        }

        [TestMethod]
        public void Can_Add_To_Cart()
        {
            // Arrange - create the mock repository
            Mock<ICofeeRepository> mock = new Mock<ICofeeRepository>();
            mock.Setup(m => m.Products).Returns(new Cofee[] {
            new Cofee{
                CofeeId =1,
                Name ="Espresso",
                PriceCoeff =2.0M,
                VolumeOptions = new VolumeOption[]{ new VolumeOption {VolumeOptionId=1, Size=0.133M,Unit =new Unit {Name = "Litre" } } },
                SugarOptions=new SugarOption[]{ new SugarOption {SugarOptionId = 1,Size = 1,Price = 0, Unit = new Unit { Name = "Not avaible" } } } } 
                }.AsQueryable());
            // Arrange - create a Cart
            Cart cart = new Cart();
            // Arrange - create the controller
            CartController target = new CartController(mock.Object);
            // Act - add a product to the cart
            target.AddToCart(cart, 1, 1, 1, true);
            // Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Recipe.CofeeId, 1);
        }

        [TestMethod]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {
            // Arrange - create the mock repository
            Mock<ICofeeRepository> mock = new Mock<ICofeeRepository>();
            mock.Setup(m => m.Products).Returns(new Cofee[] {
            new Cofee{
                CofeeId =1,
                Name ="Espresso",
                PriceCoeff =2.0M,
                VolumeOptions = new VolumeOption[]{ new VolumeOption {VolumeOptionId=1, Size=0.133M,Unit =new Unit {Name = "Litre" } } },
                SugarOptions=new SugarOption[]{ new SugarOption {SugarOptionId = 1,Size = 1,Price = 0, Unit = new Unit { Name = "Not avaible" } } } }
                }.AsQueryable());
            // Arrange - create a Cart
            Cart cart = new Cart();
            // Arrange - create the controller
            CartController target = new CartController(mock.Object);
            // Act - add a product to the cart
            RedirectToRouteResult result = target.AddToCart(cart, 2, 1, 1, true);
            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }

        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            // Arrange - create a Cart
            Cart cart = new Cart();
            // Arrange - create the controller
            CartController target = new CartController(null);
            // Act - call the Index action method
            Cart result = (Cart)target.Index(cart).ViewData.Model;
            // Assert
            Assert.AreSame(result, cart);
        }
    }
}
