using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class ManufacturerFind
{
    [TestMethod]
    public void ManufacturerFind_ExistingId()
    {
        try
        {
            Guid manufacturerId = ManufacturerTestService.GetExistingId();
            ManufacturerManager manufacturer = new ManufacturerManager(manufacturerId);
            Assert.IsTrue(manufacturer.Find(), "No se pudo cargar la información del fabricante");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    [TestMethod]
    public void ManufacturerFind_NonExistentId()
    {
        try
        {
            Guid manufacturerId = Guid.NewGuid();
            ManufacturerManager manufacturer = new ManufacturerManager(manufacturerId);
            Assert.IsFalse(manufacturer.Find(), "Se cargo la información del fabricante");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
