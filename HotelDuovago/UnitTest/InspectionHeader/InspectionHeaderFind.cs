using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class InspectionHeaderFind
{
    [TestMethod]
    public void InspectionHeaderFind_ExistingId()
    {
        try
        {
            Guid existingId = InspectionHeaderTestService.GetExistingId();
            InspectionHeaderManager header = new InspectionHeaderManager(existingId);
            Assert.IsTrue(header.Find(), "No se pudo cargar la información de la inspección");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void InspectionHeaderFind_NonExistentId()
    {
        try
        {
            Guid nonExistentId = Guid.NewGuid();
            InspectionHeaderManager header = new InspectionHeaderManager(nonExistentId);
            Assert.IsFalse(header.Find(), "Se cargó información para una inspección inexistente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
