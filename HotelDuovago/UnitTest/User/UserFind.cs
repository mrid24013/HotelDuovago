using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class UserFind
{
    [TestMethod]
    public void UserFind_ExistingId()
    {
        try
        {
            Guid userId = UserTestService.GetExistingId();
            UserManager user = new UserManager(userId);

            Assert.IsTrue(user.Find(), "No se pudo cargar la información del usuario");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void UserFind_NonExistentId()
    {
        try
        {
            UserManager user = new UserManager(Guid.NewGuid());

            Assert.IsFalse(user.Find(), "Se cargó información de un usuario inexistente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}