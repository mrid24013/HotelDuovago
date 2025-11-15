using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class LocationUpdate
{
    [TestMethod]
    public void LocationUpdate_ExistingLocation()
    {
        try
        {
            Guid existingId = LocationTestService.GetExistingId();
            string locationName = UtilsTestService.GetRandomString(10);
            LocationManager location = new LocationManager(existingId, 0, locationName, "SUCCESS UPDATE TEST", true);

            Assert.IsTrue(location.Update(), "No se pudo actualizar la ubicación");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void LocationUpdate_NonExistentLocation()
    {
        try
        {
            Guid nonExistingId = Guid.NewGuid();
            string locationName = UtilsTestService.GetRandomString(10);
            LocationManager location = new LocationManager(nonExistingId, 0, locationName, "FAILED UPDATE TEST", true);

            Assert.IsFalse(location.Update(), "Se actualizó una ubicación inexistente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
