using GroceryStore.Common.BindingModels.Admin.Products;
using GroceryStore.Services.Admin.Interfaces;
using GroceryStore.Web.Areas.Admin.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GroceryStore.Tests.Controllers.Admin.ProductsControllerTests
{
    [TestClass]
    public class EditTests
    {
        [TestMethod]
        public void Edit_WithModelShouldCallServiceEditProduct()
        {
            var model = new ProductBindingModel();
            var serviceCalled = false;
            var mockReposity = new Mock<IAdminProductsService>();
            mockReposity.
                Setup(repo => repo.EditProduct(1,model))
                .Callback(() => serviceCalled = true);

            var controller = new ProductsController(mockReposity.Object);

            var result = controller.Edit(1,model);

            Assert.IsTrue(serviceCalled);
        }

        [TestMethod]
        public void Edit_WithIdCallServiceGetProduct()
        {
            var serviceCalled = false;
            var mockReposity = new Mock<IAdminProductsService>();
            mockReposity.
                Setup(repo => repo.GetProduct(1))
                .Callback(() => serviceCalled = true);

            var controller = new ProductsController(mockReposity.Object);

            var result = controller.Edit(1);

            Assert.IsTrue(serviceCalled);
        }
    }
}
