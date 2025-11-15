using ApplicationLogic.Managers;

namespace UnitTest.Services
{
    public static class InspectionHeaderTestService
    {
        public static Guid GetExistingId()
        {
            List<Guid> existingIds = InspectionHeaderManager.GetAll().Select(m => m.Id).ToList();
            Random random = new Random();
            int randomIndex = random.Next(existingIds.Count);
            return existingIds[randomIndex];
        }
    }
}
