using ApplicationLogic.Enums;
using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class MovementRequestHeaderInsert
{
    [TestMethod]
    public void MovementRequestHeaderInsert_ValidData()
    {
        try
        {
            Guid newId = Guid.NewGuid();

            Guid requestedById = UserTestService.GetExistingId();
            Guid authorizedById = UserTestService.GetExistingId();

            UserManager requestedBy = new UserManager(requestedById);
            UserManager authorizedBy = new UserManager(authorizedById);

            MovementRequestHeaderManager header = new MovementRequestHeaderManager(
                newId,
                requestedBy,
                authorizedBy,
                number: 0,
                description: "Prueba de inserción",
                status: RequestHeaderStatus.Open,
                createdAt: DateTime.Now,
                authorizedAt: DateTime.MinValue
            );

            Assert.IsTrue(header.Insert(), "No se pudo insertar el encabezado de solicitud de movimiento");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
