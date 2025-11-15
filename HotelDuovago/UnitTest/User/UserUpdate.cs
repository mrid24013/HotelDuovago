using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class UserUpdate
{
    [TestMethod]
    public void UserUpdate_ExistingUser()
    {
        try
        {
            Guid userId = UserTestService.GetExistingId();
            Guid roleId = UserRoleTestService.GetExistingId();

            UserManager user = new UserManager(
                userId,
                0,
                $"Updated User {UtilsTestService.GetRandomString(4)}",
                $"{UtilsTestService.GetRandomString(5)}@mail.com",
                "5678",
                new UserRoleManager(roleId)
            );

            Assert.IsTrue(user.Update(), "No se pudo actualizar el usuario");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void UserUpdate_NonExistentUser()
    {
        try
        {
            Guid roleId = UserRoleTestService.GetExistingId();

            UserManager user = new UserManager(
                Guid.NewGuid(),
                0,
                $"NonExistent User {UtilsTestService.GetRandomString(4)}",
                "fake@mail.com",
                "5678",
                new UserRoleManager(roleId)
            );

            Assert.IsFalse(user.Update(), "Se actualizó un usuario inexistente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
