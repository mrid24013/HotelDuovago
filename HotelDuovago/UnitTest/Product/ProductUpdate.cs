using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class ProductUpdate
{
    [TestMethod]
    public void ProductUpdate_ExistingProduct()
    {
        try
        {
            Guid productId = ProductTestService.GetExistingId();
            string productCode = UtilsTestService.GetRandomString(3);
            Guid manufacturerId = ManufacturerTestService.GetExistingId();
            ManufacturerManager manufacturer = new ManufacturerManager(manufacturerId);
            manufacturer.Find();

            ProductManager product = new ProductManager(
                productId,
                productCode,
                "SUCCESS PRODUCT UPDATE TEST",
                543.21M,
                manufacturer
            );

            Assert.IsTrue(product.Update(), "No se pudo actualizar el producto");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void ProductUpdate_NonExistentProduct()
    {
        try
        {
            Guid productId = Guid.NewGuid();
            string productCode = UtilsTestService.GetRandomString(3);
            Guid manufacturerId = ManufacturerTestService.GetExistingId();
            ManufacturerManager manufacturer = new ManufacturerManager(manufacturerId);
            manufacturer.Find();

            ProductManager product = new ProductManager(
                productId,
                productCode,
                "FAILED PRODUCT UPDATE TEST",
                111.11M,
                manufacturer
            );

            Assert.IsFalse(product.Update(), "Se actualizó el producto correctamente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}