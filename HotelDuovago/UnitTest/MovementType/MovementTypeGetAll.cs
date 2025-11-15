using ApplicationLogic.Managers;

namespace UnitTest;

[TestClass]
public class MovementTypeGetAll
{
    [TestMethod]
    public void GetAll_Success()
    {
        try
        {
            List<MovementTypeManager> movementTypes = MovementTypeManager.GetAll();
            Assert.IsTrue(movementTypes.Count > 0, "No se pudo cargar la información de tipos de movimiento");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
