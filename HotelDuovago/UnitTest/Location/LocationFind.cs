using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class LocationFind
{
    [TestMethod]
    public void LocationFind_ExistingId()
    {
        try
        {
            Guid locationId = LocationTestService.GetExistingId();
            LocationManager location = new LocationManager(locationId);
            Assert.IsTrue(location.Find(), "No se pudo cargar la información de la ubicación");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    [TestMethod]
    public void LocationFind_NonExistentId()
    {
        try
        {
            Guid locationId = Guid.NewGuid();
            LocationManager location = new LocationManager(locationId);
            Assert.IsFalse(location.Find(), "Se cargó la información de la ubicación");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
