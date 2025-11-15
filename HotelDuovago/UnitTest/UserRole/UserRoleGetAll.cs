using ApplicationLogic.Managers;

namespace UnitTest;

[TestClass]
public class UserRoleGetAll
{
    [TestMethod]
    public void GetAll_Success()
    {
        try
        {
            List<UserRoleManager> roles = UserRoleManager.GetAll();
            Assert.IsTrue(roles.Count > 0, "No se pudo cargar la información de los roles de usuario");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
