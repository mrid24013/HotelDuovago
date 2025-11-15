using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class MovementTypeInsert
{
    [TestMethod]
    public void MovementTypeInsert_NewType()
    {
        try
        {
            Guid id = Guid.NewGuid();
            int number = UtilsTestService.GetRandomNumber(1, 9999);

            MovementTypeManager movementType = new MovementTypeManager(
                id,
                number,
                $"Tipo {UtilsTestService.GetRandomString(5)}",
                $"Descripción {UtilsTestService.GetRandomString(10)}"
            );

            Assert.IsTrue(movementType.Insert(), "No se pudo insertar el tipo de movimiento");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
