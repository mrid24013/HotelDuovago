using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class HabitacionFind
{
    [TestMethod]
    public void HabitacionFind_ExistingId()
    {
        try
        {
            int habitacionId = HabitacionTestService.GetExistingId();
            HabitacionManager habitacion = new HabitacionManager(habitacionId);

            Assert.IsTrue(habitacion.Find(), "No se pudo cargar la información de la habitacion");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void HabitacionFind_NonExistentId()
    {
        try
        {
            int habitacionId = 9999999;
            HabitacionManager habitacion = new HabitacionManager(habitacionId);

            Assert.IsFalse(habitacion.Find(), "Se cargó la información de la habitacion");
        }
        catch (Exception)
        {
            throw;
        }
    }
}