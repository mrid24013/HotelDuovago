using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class ReservacionInsert
{
    [TestMethod]
    public void ReservacionInsert_NonExistentHabitacion()
    {
        try
        {

            string randomString = UtilsTestService.GetRandomString(10);
            Random random = new Random();

            ReservacionManager reservacion = new ReservacionManager(
                random.Next(500, 1000),
                1,
                random.Next(400, 500),
                DateOnly.FromDateTime(DateTime.Now),
                DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                1,
                random.Next(500, 1000),
                "FAILED RESERVACION UPDATE TEST"
            );

            Assert.IsFalse(reservacion.Insert(), "Se inserto correctamente la reservacion");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void ReservacionInsert_ExistingHabitacion()
    {
        try
        {
            int reservacionId = ReservacionTestService.GetExistingId();
            int habitacionId = ReservacionTestService.GetExistingHabitacionId();
            Random random = new Random();

            ReservacionManager reservacion = new ReservacionManager(
                reservacionId,
                1,
                habitacionId,
                DateOnly.FromDateTime(DateTime.Now),
                DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                1,
                random.Next(500, 1000),
                "SUCCESS RESERVACION UPDATE TEST"
            );

            Assert.IsTrue(reservacion.Insert(), "No se pudo insertar la reservacion correctamente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
