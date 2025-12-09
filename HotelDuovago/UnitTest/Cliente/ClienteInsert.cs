using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class ClienteInsert
{
    [TestMethod]
    public void ClienteInsert_NonExistentEmail()
    {
        try
        {
            string randomString = UtilsTestService.GetRandomString(10);
            Random random = new Random();

            ClienteManager cliente = new ClienteManager(
                random.Next(500, 1000),
                "SUCCESS CLIENTE CREATION TEST",
                randomString,
                randomString,
                "SUCCESS CLIENTE CREATION TEST",
                DateTime.Now
            );

            Assert.IsTrue(cliente.Insert(), "No se pudo insertar el cliente");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void ClienteInsert_ExistingEmail()
    {
        try
        {
            string clienteEmail = ClienteTestService.GetExistingEmail();
            Random random = new Random();

            ClienteManager cliente = new ClienteManager(
                random.Next(500, 1000),
                "FAILED CLIENTE CREATION TEST",
                "FAILED CLIENTE CREATION TEST",
                clienteEmail,
                "FAILED CLIENTE CREATION TEST",
                DateTime.Now
            );

            Assert.IsFalse(cliente.Insert(), "Se insertó el cliente correctamente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
