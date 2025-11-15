using ApplicationLogic.Managers;

namespace UnitTest.Services
{
    public static class MovementTypeTestService
    {
        public static Guid GetExistingId()
        {
            List<Guid> existingIds = MovementTypeManager.GetAll().Select(m => m.Id).ToList();
            Random random = new Random();
            int randomIndex = random.Next(existingIds.Count);
            return existingIds[randomIndex];
        }
    }
}
