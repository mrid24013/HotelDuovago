using ApplicationLogic.Enums;
using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class MovementRequestDetailInsert
{
    [TestMethod]
    public void MovementRequestDetailInsert_ValidData()
    {
        try
        {
            Guid newId = Guid.NewGuid();
            MovementRequestHeaderManager header = new MovementRequestHeaderManager(MovementRequestHeaderTestService.GetExistingId());
            MovementTypeManager type = new MovementTypeManager(MovementTypeTestService.GetExistingId());
            InventoryManager inventory = new InventoryManager(InventoryTestService.GetExistingId());
            string description = "TEST INSERT";
            int quantity = 10;
            RequestDetailStatus status = RequestDetailStatus.Open;

            MovementRequestDetailManager detail = new MovementRequestDetailManager(newId, header, type, inventory, description, quantity, status);
            Assert.IsTrue(detail.Insert(), "No se pudo insertar el detalle de solicitud de movimiento");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
