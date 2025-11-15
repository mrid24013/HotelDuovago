using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class InspectionDetailUpdate
{
    [TestMethod]
    public void InspectionDetailUpdate_ExistingDetail()
    {
        try
        {
            Guid existingId = InspectionDetailTestService.GetExistingId();
            InspectionHeaderManager header = new InspectionHeaderManager(InspectionHeaderTestService.GetExistingId());
            InventoryManager inventory = new InventoryManager(InventoryTestService.GetExistingId());
            int expectedQuantity = 25;
            int realQuantity = 22;

            InspectionDetailManager detail = new InspectionDetailManager(
                existingId,
                header,
                inventory,
                expectedQuantity,
                realQuantity
            );

            Assert.IsTrue(detail.Update(), "No se pudo actualizar el detalle de inspección");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void InspectionDetailUpdate_NonExistentDetail()
    {
        try
        {
            Guid nonExistentId = Guid.NewGuid();
            InspectionHeaderManager header = new InspectionHeaderManager(InspectionHeaderTestService.GetExistingId());
            InventoryManager inventory = new InventoryManager(InventoryTestService.GetExistingId());
            int expectedQuantity = 30;
            int realQuantity = 28;

            InspectionDetailManager detail = new InspectionDetailManager(
                nonExistentId,
                header,
                inventory,
                expectedQuantity,
                realQuantity
            );

            Assert.IsFalse(detail.Update(), "Se actualizó un detalle de inspección inexistente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
