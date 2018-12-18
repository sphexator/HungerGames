using System.Collections.Generic;
using HungerGame.Entities.Internal;
using HungerGame.Entities.Internal.Events;
using HungerGame.Entities.Items;
using HungerGame.Entities.User;
using HungerGame.Generator;

namespace HungerGame.Handler
{
    internal class EventHandler : IRequired
    {
        private readonly ChanceGenerator _chance;
        private readonly Die _die;
        private readonly Consume _eat;
        private readonly Hack _hack;
        private readonly Idle _idle;
        private readonly Attack _attack;
        private readonly Loot _loot;
        private readonly Meet _meet;
        private readonly Sleep _sleep;

        public EventHandler(ChanceGenerator chance, Loot loot, Attack attack, Idle idle, Hack hack, Meet meet, Die die,
            Sleep sleep, Consume eat)
        {
            _chance = chance;
            _loot = loot;
            _attack = attack;
            _idle = idle;
            _hack = hack;
            _meet = meet;
            _die = die;
            _sleep = sleep;
            _eat = eat;
        }

        internal string EventManager(List<HungerGameProfile> users, HungerGameProfile profile, ItemDrop drops)
        {
            var evt = _chance.EventDetermination(profile);
            switch (evt)
            {
                case ActionType.Loot:
                {
                    return _loot.LootEvent(profile, drops);
                }
                case ActionType.Attack:
                {
                    return _attack.AttackEvent(users, profile);
                }
                case ActionType.Idle:
                {
                    return _idle.IdleEvent();
                }
                case ActionType.Meet:
                {
                    return _meet.MeetEvent();
                }
                case ActionType.Hack:
                {
                    return _hack.HackEvent(profile, drops);
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