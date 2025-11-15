using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class InventoryFind
{
    [TestMethod]
    public void InventoryFind_ExistingId()
    {
        try
        {
            Guid inventoryId = InventoryTestService.GetExistingId();
            InventoryManager inventory = new InventoryManager(inventoryId);
            Assert.IsTrue(inventory.Find(), "No se pudo cargar la información del inventario");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void InventoryFind_NonExistentId()
    {
        try
        {
            Guid inventoryId = Guid.NewGuid();
            InventoryManager inventory = new InventoryManager(inventoryId);
            Assert.IsFalse(inventory.Find(), "Se cargó información de un inventario inexistente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
