using ApplicationLogic.Managers;

namespace UnitTest;

[TestClass]
public class InventoryGetAll
{
    [TestMethod]
    public void GetAll_Success()
    {
        try
        {
            List<InventoryManager> inventories = InventoryManager.GetAll();
            Assert.IsTrue(inventories.Count > 0, "No se pudo cargar la información de los inventarios");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
