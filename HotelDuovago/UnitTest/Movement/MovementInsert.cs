using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class MovementInsert
{
    [TestMethod]
    public void MovementInsert_ValidData()
    {
        try
        {
            Guid newId = Guid.NewGuid();
            ProductManager product = new ProductManager(ProductTestService.GetExistingId());
            InventoryManager fromInventory = new InventoryManager(InventoryTestService.GetExistingId());
            InventoryManager toInventory = new InventoryManager(InventoryTestService.GetExistingId());
            MovementTypeManager type = new MovementTypeManager(MovementTypeTestService.GetExistingId());
            MovementRequestDetailManager requestDetail = new MovementRequestDetailManager(MovementRequestDetailTestService.GetExistingId());
            int quantity = 5;
            DateTime createdAt = DateTime.Now;

            MovementManager movement = new MovementManager(
                newId,
                product,
                fromInventory,
                toInventory,
                type,
                requestDetail,
                quantity,
                createdAt
            );

            Assert.IsTrue(movement.Insert(), "No se pudo insertar el movimiento");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
