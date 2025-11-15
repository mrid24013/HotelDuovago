using ApplicationLogic.Managers;

namespace UnitTest.Services
{
    public static class MovementTestService
    {
        public static Guid GetExistingId()
        {
            List<Guid> existingIds = MovementManager.GetAll().Select(m => m.Id).ToList();
            Random random = new Random();
            int randomIndex = random.Next(existingIds.Count);
            return existingIds[randomIndex];
        }
    }
}
