using ApplicationLogic.Enums;
using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class MovementRequestHeaderUpdate
{
    [TestMethod]
    public void MovementRequestHeaderUpdate_ExistingHeader()
    {
        try
        {
            Guid existingId = MovementRequestHeaderTestService.GetExistingId();

            Guid requestedById = UserTestService.GetExistingId();
            Guid authorizedById = UserTestService.GetExistingId();

            UserManager requestedBy = new UserManager(requestedById);
            UserManager authorizedBy = new UserManager(authorizedById);

            MovementRequestHeaderManager header = new MovementRequestHeaderManager(
                existingId,
                requestedBy,
                authorizedBy,
                number: 123,
                description: "Prueba de actualización",
                status: RequestHeaderStatus.Close,
                createdAt: DateTime.Now.AddDays(-1),
                authorizedAt: DateTime.Now
            );

            Assert.IsTrue(header.Update(), "No se pudo actualizar el encabezado de solicitud de movimiento");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void MovementRequestHeaderUpdate_NonExistentHeader()
    {
        try
        {
            Guid nonExistentId = Guid.NewGuid();

            Guid requestedById = UserTestService.GetExistingId();
            Guid authorizedById = UserTestService.GetExistingId();

            UserManager requestedBy = new UserManager(requestedById);
            UserManager authorizedBy = new UserManager(authorizedById);

            MovementRequestHeaderManager header = new MovementRequestHeaderManager(
                nonExistentId,
                requestedBy,
                authorizedBy,
                number: 999,
                description: "Intento de actualización fallida",
                status: RequestHeaderStatus.Open,
                createdAt: DateTime.Now,
                authorizedAt: DateTime.MinValue
            );

            Assert.IsFalse(header.Update(), "Se actualizó un encabezado inexistente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
