using ApplicationLogic.Managers;
using DataAccess.Models;

namespace UnitTest;

[TestClass]
public class ReservacionGetAll
{
    [TestMethod]
    public void GetAll_Success()
    {
        try
        {
            List<ReservacionManager> reservaciones = ReservacionManager.GetAll();
            Assert.IsTrue(reservaciones.Count > 0, "No se pudo cargar la información de las reservaciones");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
