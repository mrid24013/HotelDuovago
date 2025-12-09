using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class HabitacionDelete
{
    [TestMethod]
    public void HabitacionDelete_ExistingHabitacion()
    {
        try
        {
            int habitacionId = HabitacionTestService.GetExistingId();

            HabitacionManager habitacion = new HabitacionManager(
                habitacionId
            );

            Assert.IsTrue(habitacion.Delete(), "No se pudo borrar la habitacion");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void HabitacionDelete_NonExistentHabitacion()
    {
        try
        {
            Random random = new Random();

            HabitacionManager habitacion = new HabitacionManager(
                random.Next(500, 1000)
            );

            Assert.IsFalse(habitacion.Delete(), "Se borro la habitacion correctamente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}