using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class ClienteDelete
{
    [TestMethod]
    public void ClienteDelete_ExistingCliente()
    {
        try
        {
            int clienteId = ClienteTestService.GetExistingId();

            ClienteManager cliente = new ClienteManager(
                clienteId
            );

            Assert.IsTrue(cliente.Delete(), "No se pudo borrar el cliente");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void ClienteDelete_NonExistentCliente()
    {
        try
        {
            Random random = new Random();

            ClienteManager cliente = new ClienteManager(
                random.Next(500, 1000)
            );

            Assert.IsFalse(cliente.Delete(), "Se borro el cliente correctamente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}