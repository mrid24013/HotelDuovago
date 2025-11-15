using ApplicationLogic.Managers;

namespace UnitTest;

[TestClass]
public class MovementRequestDetailGetAll
{
    [TestMethod]
    public void MovementRequestDetailGetAll_Success()
    {
        try
        {
            List<MovementRequestDetailManager> details = MovementRequestDetailManager.GetAll();
            Assert.IsTrue(details.Count > 0, "No se pudo cargar la información de los detalles de la solicitud de movimiento");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
