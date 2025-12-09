using ApplicationLogic.Managers;

namespace UnitTest;

[TestClass]
public class HabitacionGetAll
{
    [TestMethod]
    public void GetAll_Success()
    {
        try
        {
            List<HabitacionManager> habitaciones = HabitacionManager.GetAll();
            Assert.IsTrue(habitaciones.Count > 0, "No se pudo cargar la información de las habitaciones");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
