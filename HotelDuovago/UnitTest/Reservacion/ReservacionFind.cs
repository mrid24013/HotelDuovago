using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class ReservacionFind
{
    [TestMethod]
    public void ReservacionFind_ExistingId()
    {
        try
        {
            int reservacionId = ReservacionTestService.GetExistingId();
            ReservacionManager reservacion = new ReservacionManager(reservacionId);

            Assert.IsTrue(reservacion.Find(), "No se pudo cargar la información de la reservacion");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void ReservacionFind_NonExistentId()
    {
        try
        {
            int reservacionId = 9999999;
            ReservacionManager reservacion = new ReservacionManager(reservacionId);

            Assert.IsFalse(reservacion.Find(), "Se cargó la información de la reservacion");
        }
        catch (Exception)
        {
            throw;
        }
    }
}