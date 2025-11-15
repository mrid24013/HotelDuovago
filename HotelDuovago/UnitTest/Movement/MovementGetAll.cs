using ApplicationLogic.Managers;

namespace UnitTest;

[TestClass]
public class MovementGetAll
{
    [TestMethod]
    public void MovementGetAll_Success()
    {
        try
        {
            List<MovementManager> movements = MovementManager.GetAll();
            Assert.IsTrue(movements.Count > 0, "No se pudo cargar la información de los movimientos");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
