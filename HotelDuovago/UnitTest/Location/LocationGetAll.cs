using ApplicationLogic.Managers;

namespace UnitTest;

[TestClass]
public class LocationGetAll
{
    [TestMethod]
    public void GetAll_Success()
    {
        try
        {
            List<LocationManager> locations = LocationManager.GetAll();
            Assert.IsTrue(locations.Count > 0, "No se pudo cargar la información de las sucursales");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
