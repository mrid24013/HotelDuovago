using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class InspectionDetailInsert
{
    [TestMethod]
    public void InspectionDetailInsert_ValidData()
    {
        try
        {
            Guid newId = Guid.NewGuid();
            InspectionHeaderManager header = new InspectionHeaderManager(InspectionHeaderTestService.GetExistingId());
            InventoryManager inventory = new InventoryManager(InventoryTestService.GetExistingId());
            int expectedQuantity = 20;
            int realQuantity = 18;

            InspectionDetailManager detail = new InspectionDetailManager(
                newId,
                header,
                inventory,
                expectedQuantity,
                realQuantity
            );

            Assert.IsTrue(detail.Insert(), "No se pudo insertar el detalle de inspección");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
