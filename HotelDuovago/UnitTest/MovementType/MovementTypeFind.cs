using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class MovementTypeFind
{
    [TestMethod]
    public void MovementTypeFind_ExistingId()
    {
        try
        {
            Guid id = MovementTypeTestService.GetExistingId();
            MovementTypeManager movementType = new MovementTypeManager(id);

            Assert.IsTrue(movementType.Find(), "No se pudo cargar la información del tipo de movimiento");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void MovementTypeFind_NonExistentId()
    {
        try
        {
            MovementTypeManager movementType = new MovementTypeManager(Guid.NewGuid());

            Assert.IsFalse(movementType.Find(), "Se cargó un tipo de movimiento inexistente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
