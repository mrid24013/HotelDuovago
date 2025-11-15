using ApplicationLogic.Managers;

namespace UnitTest.Services
{
    public static class MovementRequestHeaderTestService
    {
        public static Guid GetExistingId()
        {
            List<Guid> existingIds = MovementRequestHeaderManager.GetAll().Select(m => m.Id).ToList();
            Random random = new Random();
            int randomIndex = random.Next(existingIds.Count);
            return existingIds[randomIndex];
        }
    }
}
