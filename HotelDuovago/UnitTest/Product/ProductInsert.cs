using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class ProductInsert
{
    [TestMethod]
    public void ProductInsert_NonExistentCode()
    {
        try
        {
            string productCode = UtilsTestService.GetRandomString(3);
            Guid manufacturerId = ManufacturerTestService.GetExistingId();
            ManufacturerManager manufacturer = new ManufacturerManager(manufacturerId);
            manufacturer.Find();

            ProductManager product = new ProductManager(
                Guid.NewGuid(),
                productCode,
                "SUCCESS PRODUCT CREATION TEST",
                123.45M,
                manufacturer
            );

            Assert.IsTrue(product.Insert(), "No se pudo insertar el producto");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void ProductInsert_ExistingCode()
    {
        try
        {
            string productCode = ProductTestService.GetExistingCode();
            Guid manufacturerId = ManufacturerTestService.GetExistingId();
            ManufacturerManager manufacturer = new ManufacturerManager(manufacturerId);
            manufacturer.Find();

            ProductManager product = new ProductManager(
                Guid.NewGuid(),
                productCode,
                "FAILED PRODUCT CREATION TEST",
                999.99M,
                manufacturer
            );

            Assert.IsFalse(product.Insert(), "Se insertó el producto correctamente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
