using System;

namespace Priest
{
    public class Priest : Heroes.Heroes
    {
        private static Ability heal = new Ability(15, 12);
        private static Ability rejuvanate = new Ability(16, 11);
        private static Ability regenarate = new Ability(15, 18);
        private static Ability revive = new Ability(0, 30);

        public Priest() : base()
        {
            maxHealth = 70;
            maxMana = 80;
            health = 70;
            mana = 80;
            minDamage = 11;
            maxDamage = 16;
        }

        public static int Heal()
        {
            Random damageDealt = new Random();
            mana -= heal.mana;
            return damageDealt.Next(heal.damage - 2, heal.damage + 2);
        }

        public static int Rejuvanate()
        {
            Random damageDealt = new Random();
            mana -= rejuvanate.mana;
            return damageDealt.Next(rejuvanate.damage, rejuvanate.damage);
        }

        public static int Regenerate()
        {
            Random damageDealt = new Random();
            mana -= regenarate.mana;
            return damageDealt.Next(regenarate.damage - 8, regenarate.damage + 8);
        }

        public static void Revive(Heroes.Heroes hero)
        {
            hero.SetHealth(40);
            hero.SetMana(20);
        }
    }
}
