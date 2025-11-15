using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class UserRoleFind
{
    [TestMethod]
    public void UserRoleFind_ExistingId()
    {
        try
        {
            Guid roleId = UserRoleTestService.GetExistingId();
            UserRoleManager role = new UserRoleManager(roleId);
            Assert.IsTrue(role.Find(), "No se pudo cargar la información del rol de usuario");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void UserRoleFind_NonExistentId()
    {
        try
        {
            UserRoleManager role = new UserRoleManager(Guid.NewGuid());
            Assert.IsFalse(role.Find(), "Se cargó la información de un rol de usuario inexistente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
