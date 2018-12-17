using HungerGame.Calculate;
using HungerGame.Entities;
using HungerGame.Events.Types;

namespace HungerGame.Events
{
    internal class EventHandler
    {
        private ChanceGenerator _chance;
        private Loot _loot;
        private Kill _kill;
        private Idle _idle;
        private Meet _meet;
        private Hack _hack;
        private Die _die;
        private Sleep _sleep;
        private Eat _eat;

        public EventHandler(ChanceGenerator chance, Loot loot, Kill kill, Idle idle, Hack hack, Meet meet, Die die, Sleep sleep, Eat eat)
        {
            _chance = chance;
            _loot = loot;
            _kill = kill;
            _idle = idle;
            _hack = hack;
            _meet = meet;
            _die = die;
            _sleep = sleep;
            _eat = eat;
        }
        internal string EventManager(HungerGameProfile profile)
        {
            var evt = _chance.EventDetermination(profile);
            switch (evt)
            {
                case _chance.LootName:
                {
                    var response = _loot.LootEvent(profile);
                    return response;
                }
                case _chance.KillName:
                {
                    var response = _kill.KillEvent(profile);
                    return response;
                }
                case _chance.IdleName:
                {
                    var response = _idle.IdleEvent();
                    return response;
                }
                case _chance.MeetName:
                {
                    var response = _meet.MeetEvent();
                    return response;
                }
                case _chance.HackName:
                {
                    return _hack.HackEvent(profile);
                }
                case _chance.DieName:
                {
                    return _die.DieEvent(profile);
                }
                case _chance.SleepName:
                {
                    return _sleep.SleepEvent(profile);
                }
                case _chance.EatName:
                {
                    return _eat.EatEvent(profile);
                }
                default:
                    return _idle.IdleEvent();

            }

            var msg = Idle.IdleEvent();
            return msg;
        }
    }
}