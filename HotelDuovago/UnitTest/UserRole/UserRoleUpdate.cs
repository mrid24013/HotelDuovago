using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class UserRoleUpdate
{
    [TestMethod]
    public void UserRoleUpdate_ExistingRole()
    {
        try
        {
            Guid roleId = UserRoleTestService.GetExistingId();

            UserRoleManager role = new UserRoleManager(
                roleId,
                $"UpdatedRole_{UtilsTestService.GetRandomString(4)}",
                "SUCCESS ROLE UPDATE TEST"
            );

            Assert.IsTrue(role.Update(), "No se pudo actualizar el rol de usuario");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void UserRoleUpdate_NonExistentRole()
    {
        try
        {
            UserRoleManager role = new UserRoleManager(
                Guid.NewGuid(),
                $"NonExistentRole_{UtilsTestService.GetRandomString(4)}",
                "FAILED ROLE UPDATE TEST"
            );

            Assert.IsFalse(role.Update(), "Se actualizó un rol de usuario inexistente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
