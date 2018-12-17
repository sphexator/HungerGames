using System.Collections.Generic;
using HungerGame.Entities;
using HungerGame.Entities.Internal;
using HungerGame.Entities.Internal.Events;
using HungerGame.Entities.User;
using HungerGame.Generator;

namespace HungerGame.Handler
{
    internal class EventHandler : IRequired
    {
        private readonly ChanceGenerator _chance;
        private readonly Loot _loot;
        private readonly Kill _kill;
        private readonly Idle _idle;
        private readonly Meet _meet;
        private readonly Hack _hack;
        private readonly Die _die;
        private readonly Sleep _sleep;
        private readonly Eat _eat;

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

        internal string EventManager(List<HungerGameProfile> users, HungerGameProfile profile)
        {
            var evt = _chance.EventDetermination(profile);
            switch (evt)
            {
                case ActionType.Loot:
                {
                    var response = _loot.LootEvent(profile);
                    return response;
                }
                case ActionType.Kill:
                {
                    var response = _kill.KillEvent(users, profile);
                    return response;
                }
                case ActionType.Idle:
                {
                    var response = _idle.IdleEvent();
                    return response;
                }
                case ActionType.Meet:
                {
                    var response = _meet.MeetEvent();
                    return response;
                }
                case ActionType.Hack:
                {
                    return _hack.HackEvent(profile);
                }
                case ActionType.Die:
                {
                    return _die.DieEvent(profile);
                }
                case ActionType.Sleep:
                {
                    return _sleep.SleepEvent(profile);
                }
                case ActionType.Eat:
                {
                    return _eat.EatEvent(profile);
                }
                default:
                    return _idle.IdleEvent();

            }
        }
    }
}