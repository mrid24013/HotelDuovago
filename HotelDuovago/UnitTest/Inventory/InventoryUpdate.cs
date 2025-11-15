using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class InventoryUpdate
{
    [TestMethod]
    public void InventoryUpdate_ExistingInventory()
    {
        try
        {
            Guid existingInventoryId = InventoryTestService.GetExistingId();

            Guid locationId = LocationTestService.GetExistingId();
            Guid productId = ProductTestService.GetExistingId();
            Guid userId = UserTestService.GetExistingId();

            LocationManager location = new LocationManager(locationId);
            ProductManager product = new ProductManager(productId);
            UserManager user = new UserManager(userId);

            InventoryManager inventory = new InventoryManager(
                existingInventoryId,
                location,
                product,
                user,
                stock: 150,
                minStock: 20,
                maxStock: 300
            );

            Assert.IsTrue(inventory.Update(), "No se pudo actualizar el inventario");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void InventoryUpdate_NonExistentInventory()
    {
        try
        {
            Guid nonExistentInventoryId = Guid.NewGuid();

            Guid locationId = LocationTestService.GetExistingId();
            Guid productId = ProductTestService.GetExistingId();
            Guid userId = UserTestService.GetExistingId();

            LocationManager location = new LocationManager(locationId);
            ProductManager product = new ProductManager(productId);
            UserManager user = new UserManager(userId);

            InventoryManager inventory = new InventoryManager(
                nonExistentInventoryId,
                location,
                product,
                user,
                stock: 50,
                minStock: 5,
                maxStock: 100
            );

            Assert.IsFalse(inventory.Update(), "Se actualizó un inventario inexistente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
