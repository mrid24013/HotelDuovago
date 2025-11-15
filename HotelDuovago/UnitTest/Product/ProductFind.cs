using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class ProductFind
{
    [TestMethod]
    public void ProductFind_ExistingId()
    {
        try
        {
            Guid productId = ProductTestService.GetExistingId();
            ProductManager product = new ProductManager(productId);

            Assert.IsTrue(product.Find(), "No se pudo cargar la información del producto");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void ProductFind_NonExistentId()
    {
        try
        {
            Guid productId = Guid.NewGuid();
            ProductManager product = new ProductManager(productId);

            Assert.IsFalse(product.Find(), "Se cargó la información del producto");
        }
        catch (Exception)
        {
            throw;
        }
    }
}