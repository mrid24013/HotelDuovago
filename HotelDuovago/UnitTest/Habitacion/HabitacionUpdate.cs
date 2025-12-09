using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class HabitacionUpdate
{
    [TestMethod]
    public void HabitacionUpdate_ExistingHabitacion()
    {
        try
        {
            int habitacionId = HabitacionTestService.GetExistingId();
            Random random = new Random();

            HabitacionManager habitacion = new HabitacionManager(
                habitacionId,
                random.Next(400, 500),
                "SUCCESS HABITACION UPDATE TEST",
                random.Next(400, 500),
                random.Next(0, 1000),
                "SUCCESS HABITACION UPDATE TEST",
                true
            );

            Assert.IsTrue(habitacion.Update(), "No se pudo actualizar la habitacion");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void HabitacionUpdate_NonExistentHabitacion()
    {
        try
        {
            int habitacionId = HabitacionTestService.GetExistingId();
            Random random = new Random();

            HabitacionManager cliente = new HabitacionManager(
                random.Next(500, 1000),
                random.Next(400, 500),
                "SUCCESS HABITACION UPDATE TEST",
                random.Next(400, 500),
                random.Next(0, 1000),
                "SUCCESS HABITACION UPDATE TEST",
                true
            );

            Assert.IsFalse(cliente.Update(), "Se actualizó la habitacion correctamente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}