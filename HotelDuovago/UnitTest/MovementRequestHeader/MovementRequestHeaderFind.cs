using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class MovementRequestHeaderFind
{
    [TestMethod]
    public void Find_ExistingId()
    {
        try
        {
            Guid existingId = MovementRequestHeaderTestService.GetExistingId(); // Implementar en tu test service
            MovementRequestHeaderManager header = new MovementRequestHeaderManager(existingId);
            Assert.IsTrue(header.Find(), "No se pudo encontrar el encabezado de solicitud de movimiento");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void Find_NonExistentId()
    {
        try
        {
            Guid nonExistentId = Guid.NewGuid();
            MovementRequestHeaderManager header = new MovementRequestHeaderManager(nonExistentId);
            Assert.IsFalse(header.Find(), "Se encontró un encabezado inexistente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
