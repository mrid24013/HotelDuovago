using ApplicationLogic.Managers;

namespace UnitTest;

[TestClass]
public class UserGetAll
{
    [TestMethod]
    public void GetAll_Success()
    {
        try
        {
            List<UserManager> users = UserManager.GetAll();
            Assert.IsTrue(users.Count > 0, "No se pudo cargar la información de los usuarios");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
