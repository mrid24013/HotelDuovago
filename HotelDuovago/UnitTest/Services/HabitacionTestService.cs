using ApplicationLogic.Managers;

namespace UnitTest.Services
{
    public static class HabitacionTestService
    {
        public static int GetExistingId()
        {
            List<int> existingIds = HabitacionManager.GetAll().Select(h => h.Id).ToList();
            Random random = new Random();
            int randomIndex = random.Next(existingIds.Count);
            return existingIds[randomIndex];
        }

        public static int GetExistingNumero()
        {
            List<int> existingPhones = HabitacionManager.GetAll().Select(h => h.Numero).ToList();
            Random random = new Random();
            int randomIndex = random.Next(existingPhones.Count);
            return existingPhones[randomIndex];
        }
    }
}
