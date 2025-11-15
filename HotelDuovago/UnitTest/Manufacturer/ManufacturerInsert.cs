using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class ManufacturerInsert
{
    [TestMethod]
    public void ManufacturerInsert_NonExistentCode()
    {
        try
        {
            string manufacturerCode = UtilsTestService.GetRandomString(3);
            ManufacturerManager manufacturer = new ManufacturerManager(Guid.NewGuid(), manufacturerCode, "SUCCESS CREATION TEST");
            Assert.IsTrue(manufacturer.Insert(), "No se pudo insertar al fabricante");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    [TestMethod]
    public void ManufacturerInsert_ExistingCode()
    {
        try
        {
            string manufacturerCode = ManufacturerTestService.GetExistingCode();
            ManufacturerManager manufacturer = new ManufacturerManager(Guid.NewGuid(), manufacturerCode, "FAILED CREATION TEST");
            Assert.IsFalse(manufacturer.Insert(), "Se inserto al fabricante correctamente");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
