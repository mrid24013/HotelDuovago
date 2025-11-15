using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class MovementFind
{
    [TestMethod]
    public void MovementFind_ExistingId()
    {
        try
        {
            Guid existingId = MovementTestService.GetExistingId();
            MovementManager movement = new MovementManager(existingId);
            Assert.IsTrue(movement.Find(), "No se pudo cargar la información del movimiento");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void MovementFind_NonExistentId()
    {
        try
        {
            Guid nonExistentId = Guid.NewGuid();
            MovementManager movement = new MovementManager(nonExistentId);
            Assert.IsFalse(movement.Find(), "Se cargó información para un movimiento inexistente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
