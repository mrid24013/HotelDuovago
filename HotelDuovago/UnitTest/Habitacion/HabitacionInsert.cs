using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class HabitacionInsert
{
    [TestMethod]
    public void HabitacionUpdate_ExistingNumero()
    {
        try
        {
            int habitacionId = HabitacionTestService.GetExistingId();
            int habitacionNumero = HabitacionTestService.GetExistingNumero();
            Random random = new Random();

            HabitacionManager habitacion = new HabitacionManager(
                random.Next(0, 1000),
                habitacionNumero,
                "SUCCESS HABITACION UPDATE TEST",
                random.Next(400, 500),
                random.Next(0, 1000),
                "SUCCESS HABITACION UPDATE TEST",
                true
            );

            Assert.IsFalse(habitacion.Insert(), "No se pudo insertar la habitacion");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void HabitacionUpdate_NonExistentNumero()
    {
        try
        {
            Random random = new Random();

            HabitacionManager habitacion = new HabitacionManager(
                random.Next(500, 1000),
                random.Next(400, 1000),
                "SUCCESS HABITACION UPDATE TEST",
                random.Next(400, 1000),
                random.Next(0, 1000),
                "SUCCESS HABITACION UPDATE TEST",
                true
            );

            Assert.IsTrue(habitacion.Insert(), "Se inserto la habitacion correctamente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
