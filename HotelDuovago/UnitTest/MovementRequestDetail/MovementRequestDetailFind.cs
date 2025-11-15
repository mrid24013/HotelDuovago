using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class MovementRequestDetailFind
{
    [TestMethod]
    public void MovementRequestDetailFind_ExistingId()
    {
        try
        {
            Guid existingId = MovementRequestDetailTestService.GetExistingId();
            MovementRequestDetailManager detail = new MovementRequestDetailManager(existingId);
            Assert.IsTrue(detail.Find(), "No se pudo cargar la información del detalle de solicitud de movimiento");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void MovementRequestDetailFind_NonExistentId()
    {
        try
        {
            Guid nonExistentId = Guid.NewGuid();
            MovementRequestDetailManager detail = new MovementRequestDetailManager(nonExistentId);
            Assert.IsFalse(detail.Find(), "Se cargó información para un detalle inexistente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}