using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class InspectionHeaderInsert
{
    [TestMethod]
    public void InspectionHeaderInsert_ValidData()
    {
        try
        {
            Guid newId = Guid.NewGuid();
            UserManager createdBy = new UserManager(UserTestService.GetExistingId());
            LocationManager location = new LocationManager(LocationTestService.GetExistingId());
            string description = "INSPECTION INSERT TEST";
            DateTime createdAt = DateTime.Now;

            InspectionHeaderManager header = new InspectionHeaderManager(
                newId,
                createdBy,
                location,
                0,
                description,
                createdAt
            );

            Assert.IsTrue(header.Insert(), "No se pudo insertar la inspección");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
