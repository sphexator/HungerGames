using HungerGame.Entities;

namespace HungerGame.Events.Types
{
    public class Sleep : IRequired
    {
        public static string SleepEvent(HungerGameLive profile, DbService db)
        {
            var user = db.HungerGameLives.Find(profile.UserId);
            user.Sleep = 0;
            user.Fatigue = 0;
            db.SaveChanges();
            return "Fell asleep";
        }
    }
}