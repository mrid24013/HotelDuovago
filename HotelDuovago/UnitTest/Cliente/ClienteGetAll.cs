using ApplicationLogic.Managers;

namespace UnitTest;

[TestClass]
public class ClienteGetAll
{
    [TestMethod]
    public void GetAll_Success()
    {
        try
        {
            List<ClienteManager> clientes = ClienteManager.GetAll();
            Assert.IsTrue(clientes.Count > 0, "No se pudo cargar la información de los clientes");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
