using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class UserRoleInsert
{
    [TestMethod]
    public void UserRoleInsert_NewRole()
    {
        try
        {
            UserRoleManager role = new UserRoleManager(
                Guid.NewGuid(),
                $"Role_{UtilsTestService.GetRandomString(5)}",
                "SUCCESS ROLE CREATION TEST"
            );

            Assert.IsTrue(role.Insert(), "No se pudo insertar el rol de usuario");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
