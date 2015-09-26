using System;

namespace Rogue
{
    public class Rogue : Heroes.Heroes
    {
        private static Ability backstab = new Ability(25, 10);
        private static Ability multiStrike = new Ability(25, 10);
        private static Ability shoot = new Ability(23, 8);
        private static Ability stealthKill = new Ability(35, 16);

        public Rogue() : base()
        {
            maxHealth = 95;
            maxMana = 55;
            health = 95;
            mana = 55;
            minDamage = 16;
            maxDamage = 21;
        }

        public static int Backstab()
        {
            Random damageDealt = new Random();
            mana -= backstab.mana;
            return damageDealt.Next(backstab.damage - 2, backstab.damage + 2);
        }

        public static int MultiStrike()
        {
            Random damageDealt = new Random();
            mana -= multiStrike.mana;
            return damageDealt.Next(multiStrike.damage - 8, multiStrike.damage + 8);
        }

        public static int Shoot()
        {
            Random damageDealt = new Random();
            mana -= shoot.mana;
            return damageDealt.Next(shoot.damage - 3, shoot.damage + 3);
        }

        public static int StealthKill()
        {
            Random damageDealt = new Random();
            mana -= stealthKill.mana;
            return damageDealt.Next(stealthKill.damage - 5, stealthKill.damage + 10);
        }
    }
}
