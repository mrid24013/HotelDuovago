using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class InventoryInsert
{
    [TestMethod]
    public void InventoryInsert_ValidData()
    {
        try
        {
            Guid newInventoryId = Guid.NewGuid();

            // Obtén IDs válidos para Location, Product y User
            Guid locationId = LocationTestService.GetExistingId();
            Guid productId = ProductTestService.GetExistingId();
            Guid userId = UserTestService.GetExistingId();

            LocationManager location = new LocationManager(locationId);
            ProductManager product = new ProductManager(productId);
            UserManager user = new UserManager(userId);

            InventoryManager inventory = new InventoryManager(
                newInventoryId,
                location,
                product,
                user,
                stock: 100,
                minStock: 10,
                maxStock: 200
            );

            Assert.IsTrue(inventory.Insert(), "No se pudo insertar el inventario");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
