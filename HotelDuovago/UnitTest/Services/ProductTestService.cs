using ApplicationLogic.Managers;

namespace UnitTest.Services
{
    public static class ProductTestService
    {
        public static Guid GetExistingId()
        {
            List<Guid> existingIds = ProductManager.GetAll().Select(m => m.Id).ToList();
            Random random = new Random();
            int randomIndex = random.Next(existingIds.Count);
            return existingIds[randomIndex];
        }

        public static string GetExistingCode()
        {
            List<string> existingCodes = ProductManager.GetAll().Select(m => m.Code).ToList();
            Random random = new Random();
            int randomIndex = random.Next(existingCodes.Count);
            return existingCodes[randomIndex];
        }
    }
}
