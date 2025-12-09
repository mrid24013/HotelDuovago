using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class ClienteUpdate
{
    [TestMethod]
    public void ClienteUpdate_ExistingCliente()
    {
        try
        {
            int clienteId = ClienteTestService.GetExistingId();
            string randomString = UtilsTestService.GetRandomString(10);

            ClienteManager cliente = new ClienteManager(
                8,
                "SUCCESS CLIENTE UPDATE TEST",
                randomString,
                randomString,
                "SUCCESS CLIENTE UPDATE TEST",
                DateTime.Now
            );

            Assert.IsTrue(cliente.Update(), "No se pudo actualizar el cliente");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void ClienteUpdate_NonExistentCliente()
    {
        try
        {
            Random random = new Random();
            string clienteData = UtilsTestService.GetRandomString(3);

            ClienteManager cliente = new ClienteManager(
                random.Next(500, 1000),
                "FAILED CLIENTE UPDATE TEST",
                clienteData,
                clienteData,
                "FAILED CLIENTE UPDATE TEST",
                DateTime.Now
            );

            Assert.IsFalse(cliente.Update(), "Se actualizó el cliente correctamente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}