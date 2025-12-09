using ApplicationLogic.Managers;

namespace UnitTest.Services
{
    public static class ClienteTestService
    {
        public static int GetExistingId()
        {
            List<int> existingIds = ClienteManager.GetAll().Select(c => c.Id).ToList();
            Random random = new Random();
            int randomIndex = random.Next(existingIds.Count);
            return existingIds[randomIndex];
        }

        public static string GetExistingPhone()
        {
            List<string> existingPhones = ClienteManager.GetAll().Select(c => c.Telefono).ToList();
            Random random = new Random();
            int randomIndex = random.Next(existingPhones.Count);
            return existingPhones[randomIndex];
        }

        public static string GetExistingEmail()
        {
            List<string> existingEmails = ClienteManager.GetAll().Select(c => c.Email).ToList();
            Random random = new Random();
            int randomIndex = random.Next(existingEmails.Count);
            return existingEmails[randomIndex];
        }
    }
}
