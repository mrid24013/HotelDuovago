using ApplicationLogic.Managers;

using UnitTest.Services;

namespace UnitTest;

[TestClass]
public class InspectionHeaderUpdate
{
    [TestMethod]
    public void InspectionHeaderUpdate_ExistingInspection()
    {
        try
        {
            Guid existingId = InspectionHeaderTestService.GetExistingId();
            UserManager createdBy = new UserManager(UserTestService.GetExistingId());
            LocationManager location = new LocationManager(LocationTestService.GetExistingId());
            int number = 0; // En caso de actualización puedes mantener el mismo
            string description = "INSPECTION UPDATE TEST";
            DateTime createdAt = DateTime.Now;

            InspectionHeaderManager header = new InspectionHeaderManager(
                existingId,
                createdBy,
                location,
                number,
                description,
                createdAt
            );

            Assert.IsTrue(header.Update(), "No se pudo actualizar la inspección");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void InspectionHeaderUpdate_NonExistentInspection()
    {
        try
        {
            Guid nonExistentId = Guid.NewGuid();
            UserManager createdBy = new UserManager(UserTestService.GetExistingId());
            LocationManager location = new LocationManager(LocationTestService.GetExistingId());
            int number = 0;
            string description = "FAILED INSPECTION UPDATE TEST";
            DateTime createdAt = DateTime.Now;

            InspectionHeaderManager header = new InspectionHeaderManager(
                nonExistentId,
                createdBy,
                location,
                number,
                description,
                createdAt
            );

            Assert.IsFalse(header.Update(), "Se actualizó una inspección inexistente");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
