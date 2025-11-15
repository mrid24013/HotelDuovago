using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class UserInsert
{
    [TestMethod]
    public void UserInsert_NewUser()
    {
        try
        {
            Guid userId = Guid.NewGuid();
            Guid roleId = UserRoleTestService.GetExistingId();

            UserManager user = new UserManager(
                userId,
                0,
                $"User {UtilsTestService.GetRandomString(5)}",
                $"{UtilsTestService.GetRandomString(5)}@mail.com",
                "1234",
                new UserRoleManager(roleId)
            );

            Assert.IsTrue(user.Insert(), "No se pudo insertar el usuario");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
