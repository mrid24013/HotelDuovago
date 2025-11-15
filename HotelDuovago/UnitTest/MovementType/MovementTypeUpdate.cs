using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class MovementTypeUpdate
{
    [TestMethod]
    public void MovementTypeUpdate_ExistingType()
    {
        try
        {
            Guid id = MovementTypeTestService.GetExistingId();

            MovementTypeManager movementType = new MovementTypeManager(
                id,
                UtilsTestService.GetRandomNumber(1, 9999),
                $"Actualizado {UtilsTestService.GetRandomString(5)}",
                $"Nueva descripción {UtilsTestService.GetRandomString(10)}"
            );

            Assert.IsTrue(movementType.Update(), "No se pudo actualizar el tipo de movimiento");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void MovementTypeUpdate_NonExistentType()
    {
        try
        {
            MovementTypeManager movementType = new MovementTypeManager(
                Guid.NewGuid(),
                UtilsTestService.GetRandomNumber(1, 9999),
                "Tipo Inexistente",
                "Descripción ficticia"
            );

            Assert.IsFalse(movementType.Update(), "Se actualizó un tipo de movimiento inexistente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
