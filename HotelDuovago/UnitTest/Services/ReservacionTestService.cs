using ApplicationLogic.Managers;

namespace UnitTest.Services
{
    public static class ReservacionTestService
    {
        public static int GetExistingId()
        {
            List<int> existingIds = ReservacionManager.GetAll().Select(r => r.Id).ToList();
            Random random = new Random();
            int randomIndex = random.Next(existingIds.Count);
            return existingIds[randomIndex];
        }

        public static int GetExistingClienteId()
        {
            List<int> existingPhones = ReservacionManager.GetAll().Select(r => r.ClienteId).ToList();
            Random random = new Random();
            int randomIndex = random.Next(existingPhones.Count);
            return existingPhones[randomIndex];
        }

        public static int GetExistingHabitacionId()
        {
            List<int> existingEmails = ReservacionManager.GetAll().Select(r => r.HabitacionId).ToList();
            Random random = new Random();
            int randomIndex = random.Next(existingEmails.Count);
            return existingEmails[randomIndex];
        }
    }
}
