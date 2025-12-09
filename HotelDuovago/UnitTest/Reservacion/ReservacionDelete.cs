using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class ReservacionDelete
{
    [TestMethod]
    public void ReservacionDelete_ExistingReservacion()
    {
        try
        {
            int reservacionId = ReservacionTestService.GetExistingId();

            ReservacionManager reservacion = new ReservacionManager(
                reservacionId
            );

            Assert.IsTrue(reservacion.Delete(), "No se pudo borrar la reservacion");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void ReservacionDelete_NonExistentReservacion()
    {
        try
        {
            Random random = new Random();

            ReservacionManager reservacion = new ReservacionManager(
                random.Next(500, 1000)
            );

            Assert.IsFalse(reservacion.Delete(), "Se borro la reservacion correctamente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}