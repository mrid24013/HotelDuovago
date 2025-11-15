using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class LocationInsert
{
    [TestMethod]
    public void LocationInsert_NewLocation()
    {
        try
        {
            Guid locationId = Guid.NewGuid();
            string locationName = UtilsTestService.GetRandomString(10);
            LocationManager location = new LocationManager(locationId, 0, locationName, "SUCCESS CREATION TEST", true);

            Assert.IsTrue(location.Insert(), "No se pudo insertar la ubicación");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void LocationInsert_DuplicateId()
    {
        try
        {
            Guid existingId = LocationTestService.GetExistingId();
            string locationName = UtilsTestService.GetRandomString(10);
            LocationManager location = new LocationManager(existingId, 0, locationName, "FAILED CREATION TEST", false);

            Assert.IsFalse(location.Insert(), "Se insertó una ubicación con ID duplicado");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
