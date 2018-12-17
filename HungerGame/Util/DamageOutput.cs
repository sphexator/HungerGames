using System;
using HungerGame.Entities;
using HungerGame.Entities.Internal;

namespace HungerGame.Util
{
    internal class DamageOutput : IRequired
    {
        private readonly Random _random;
        internal DamageOutput(Random random)
        {
            _random = random;
        }

        internal int PistolDamage(int stamina, bool bleeding)
        {
            var damage = _random.Next(Pistol.Damage - 20, Pistol.Damage);
            var critChance = CriticalDamage();
            if (critChance) damage = damage + _random.Next(10, 20);
            if (bleeding) return damage / 2;
            return damage;
        }

        internal int BowDamage(int stamina, bool bleeding)
        {
            var damage = _random.Next(Bow.Damage - 20, Bow.Damage);
            var critChance = CriticalDamage();
            if (critChance) damage = damage + _random.Next(10, 20);
            if (bleeding) return damage / 2;
            return damage;
        }

        internal  int AxeDamage(int stamina, bool bleeding)
        {
            var damage = _random.Next(Axe.Damage - 20, Axe.Damage);
            var critChance = CriticalDamage();
            if (critChance) damage = damage + _random.Next(10, 20);
            if (bleeding) return damage / 2;
            return damage;
        }

        internal  int TrapDamage(int stamina, bool bleeding)
        {
            var damage = _random.Next(Trap.Damage - 20, Trap.Damage);
            var critChance = CriticalDamage();
            if (critChance) damage = damage + _random.Next(10, 20);
            if (bleeding) return damage / 2;
            return damage;
        }

        internal  int FistDamage(int stamina, bool bleeding)
        {
            var damage = _random.Next(Fist.Damage - 10, Fist.Damage);
            var critChance = CriticalDamage();
            if (critChance) damage = damage + _random.Next(10, 20);
            if (bleeding) return damage / 2;
            return damage;
        }

        private  bool CriticalDamage()
        {
            var chance = _random.Next(0, 100);
            return chance >= 70;
        }
    }
}