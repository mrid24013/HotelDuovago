using ApplicationLogic.Managers;

namespace UnitTest;

[TestClass]
public class ManufacturerGetAll
{
    [TestMethod]
    public void GetAll_Success()
    {
        try
        {
            List<ManufacturerManager> manufacturers = ManufacturerManager.GetAll();
            Assert.IsTrue(manufacturers.Count > 0, "No se pudo cargar la información de los fabricantes");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
