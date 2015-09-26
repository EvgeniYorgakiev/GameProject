using System;

namespace Warrior
{
    public class Warrior : Heroes.Heroes
    {
        private static Ability shieldBash = new Ability(30, 8);
        private static Ability assault = new Ability(35, 10);
        private static Ability overpower = new Ability(30, 9);
        private static Ability execute = new Ability(35, 6);

        public Warrior() : base()
        {
            maxHealth = 115;
            maxMana = 35;
            health = 115;
            mana = 35;
            minDamage = 18;
            maxDamage = 23;
        }

        public static int ShieldBash()
        {
            Random damageDealt = new Random();
            mana -= shieldBash.mana;
            return damageDealt.Next(shieldBash.damage - 2, shieldBash.damage + 2);
        }

        public static int Assault()
        {
            Random damageDealt = new Random();
            mana -= assault.mana;
            return damageDealt.Next(assault.damage - 8, assault.damage + 8);
        }

        public static int Overpower()
        {
            Random damageDealt = new Random();
            mana -= overpower.mana;
            return damageDealt.Next(overpower.damage - 3, overpower.damage + 3);
        }

        public static int Execute()
        {
            Random damageDealt = new Random();
            mana -= execute.mana;
            return damageDealt.Next(execute.damage - 5, execute.damage + 10);
        }
    }
}
