using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class InspectionDetailFind
{
    [TestMethod]
    public void InspectionDetailFind_ExistingId()
    {
        try
        {
            Guid existingId = InspectionDetailTestService.GetExistingId();
            InspectionDetailManager detail = new InspectionDetailManager(existingId);
            Assert.IsTrue(detail.Find(), "No se pudo cargar la información del detalle de inspección");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void InspectionDetailFind_NonExistentId()
    {
        try
        {
            Guid nonExistentId = Guid.NewGuid();
            InspectionDetailManager detail = new InspectionDetailManager(nonExistentId);
            Assert.IsFalse(detail.Find(), "Se cargó información para un detalle de inspección inexistente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
