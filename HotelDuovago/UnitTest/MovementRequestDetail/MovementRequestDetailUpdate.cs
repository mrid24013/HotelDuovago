using ApplicationLogic.Enums;
using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class MovementRequestDetailUpdate
{
    [TestMethod]
    public void MovementRequestDetailUpdate_ExistingDetail()
    {
        try
        {
            Guid existingId = MovementRequestDetailTestService.GetExistingId();
            MovementRequestHeaderManager header = new MovementRequestHeaderManager(MovementRequestHeaderTestService.GetExistingId());
            MovementTypeManager type = new MovementTypeManager(MovementTypeTestService.GetExistingId());
            InventoryManager inventory = new InventoryManager(InventoryTestService.GetExistingId());
            string description = "TEST UPDATE";
            int quantity = 20;
            RequestDetailStatus status = RequestDetailStatus.Approved;

            MovementRequestDetailManager detail = new MovementRequestDetailManager(existingId, header, type, inventory, description, quantity, status);
            Assert.IsTrue(detail.Update(), "No se pudo actualizar el detalle de solicitud de movimiento");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void MovementRequestDetailUpdate_NonExistentDetail()
    {
        try
        {
            Guid nonExistentId = Guid.NewGuid();
            MovementRequestHeaderManager header = new MovementRequestHeaderManager(MovementRequestHeaderTestService.GetExistingId());
            MovementTypeManager type = new MovementTypeManager(MovementTypeTestService.GetExistingId());
            InventoryManager inventory = new InventoryManager(InventoryTestService.GetExistingId());
            string description = "FAILED UPDATE TEST";
            int quantity = 5;
            RequestDetailStatus status = RequestDetailStatus.Open;

            MovementRequestDetailManager detail = new MovementRequestDetailManager(nonExistentId, header, type, inventory, description, quantity, status);
            Assert.IsFalse(detail.Update(), "Se actualizó un detalle inexistente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
