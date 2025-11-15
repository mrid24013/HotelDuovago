using ApplicationLogic.Managers;

namespace UnitTest;

[TestClass]
public class InspectionDetailGetAll
{
    [TestMethod]
    public void InspectionDetailGetAll_Success()
    {
        try
        {
            List<InspectionDetailManager> details = InspectionDetailManager.GetAll();
            Assert.IsTrue(details.Count() > 0, "No se pudo cargar la información de los detalles de inspección");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
