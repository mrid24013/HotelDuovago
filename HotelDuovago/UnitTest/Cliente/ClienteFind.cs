using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class ClienteFind
{
    [TestMethod]
    public void ClienteFind_ExistingId()
    {
        try
        {
            int clienteId = ClienteTestService.GetExistingId();
            ClienteManager cliente = new ClienteManager(clienteId);

            Assert.IsTrue(cliente.Find(), "No se pudo cargar la información del cliente");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void ClienteFind_NonExistentId()
    {
        try
        {
            int clienteId = 9999999;
            ClienteManager cliente = new ClienteManager(clienteId);

            Assert.IsFalse(cliente.Find(), "Se cargó la información del cliente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}