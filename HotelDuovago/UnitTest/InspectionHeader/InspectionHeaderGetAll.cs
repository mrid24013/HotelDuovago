using ApplicationLogic.Managers;

namespace UnitTest;

[TestClass]
public class InspectionHeaderGetAll
{
    [TestMethod]
    public void InspectionHeaderGetAll_Success()
    {
        try
        {
            List<InspectionHeaderManager> headers = InspectionHeaderManager.GetAll();
            Assert.IsTrue(headers.Count > 0, "No se pudo cargar la información de las inspecciones");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
