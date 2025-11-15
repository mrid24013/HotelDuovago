using ApplicationLogic.Managers;

namespace UnitTest;

[TestClass]
public class ProductGetAll
{
    [TestMethod]
    public void GetAll_Success()
    {
        try
        {
            List<ProductManager> products = ProductManager.GetAll();
            Assert.IsTrue(products.Count > 0, "No se pudo cargar la información de los productos");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
