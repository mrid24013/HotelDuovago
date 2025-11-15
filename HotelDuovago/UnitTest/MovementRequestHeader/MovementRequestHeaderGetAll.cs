using ApplicationLogic.Managers;

namespace UnitTest;

[TestClass]
public class MovementRequestHeaderGetAll
{
    [TestMethod]
    public void GetAll_Success()
    {
        try
        {
            List<MovementRequestHeaderManager> headers = MovementRequestHeaderManager.GetAll();
            Assert.IsTrue(headers.Count > 0, "No se pudo cargar la información de los encabezados de solicitudes de movimiento");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
