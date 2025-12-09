using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class ReservacionUpdate
{
    [TestMethod]
    public void ReservacionUpdate_ExistingReservacion()
    {
        try
        {
            int reservacionId = ReservacionTestService.GetExistingId();
            int clienteId = ReservacionTestService.GetExistingClienteId();
            int habitacionId = ReservacionTestService.GetExistingHabitacionId();
            Random random = new Random();

            ReservacionManager reservacion = new ReservacionManager(
                22,
                clienteId,
                habitacionId,
                DateOnly.FromDateTime(DateTime.Now),
                DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                1,
                random.Next(500, 1000),
                "SUCCESS RESERVACION UPDATE TEST"
            );

            Assert.IsTrue(reservacion.Update(), "No se pudo actualizar la reservacion");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void ReservacionUpdate_NonExistentReservacion()
    {
        try
        {
            Random random = new Random();
            string reservacionData = UtilsTestService.GetRandomString(3);

            ReservacionManager reservacion = new ReservacionManager(
                random.Next(500, 1000),
                random.Next(0, 10),
                random.Next(0, 10),
                DateOnly.FromDateTime(DateTime.Now),
                DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                1,
                random.Next(500, 1000),
                "FAILED RESERVACION UPDATE TEST"
            );

            Assert.IsFalse(reservacion.Update(), "Se actualizó la reservacion correctamente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}