using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class ManufacturerUpdate
{
    [TestMethod]
    public void ManufacturerUpdate_ExistingManufacturer()
    {
        try
        {
            Guid manufacturerId = ManufacturerTestService.GetExistingId();
            string manufacturerCode = UtilsTestService.GetRandomString(3);
            ManufacturerManager manufacturer = new ManufacturerManager(manufacturerId, manufacturerCode, "SUCCESS UPDATE TEST");
            Assert.IsTrue(manufacturer.Update(), "No se pudo actualizar al fabricante");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    [TestMethod]
    public void ManufacturerUpdate_NonExistentManufacturer()
    {
        try
        {
            Guid manufacturerId = Guid.NewGuid();
            string manufacturerCode = UtilsTestService.GetRandomString(3);
            ManufacturerManager manufacturer = new ManufacturerManager(manufacturerId, manufacturerCode, "FAILED UPDATE TEST");
            Assert.IsFalse(manufacturer.Update(), "Se actualizo al fabricante correctamente");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
