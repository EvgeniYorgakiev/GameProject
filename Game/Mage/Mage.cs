using System;

namespace Mage
{
    public class Mage : Heroes.Heroes
    {
        private static Ability fireBall = new Ability(28, 12);
        private static Ability iceBlast = new Ability(28, 11);
        private static Ability shock = new Ability(28, 13);
        private static Ability iceLance = new Ability(40, 18);

        public Mage() : base()
        {
            maxHealth = 60;
            maxMana = 90;
            health = 60;
            mana = 90;
            minDamage = 13;
            maxDamage = 19;
        }

        public static int FireBall()
        {
            Random damageDealt = new Random();
            mana -= fireBall.mana;
            return damageDealt.Next(fireBall.damage - 2, fireBall.damage + 2);
        }

        public static int IceBlast()
        {
            Random damageDealt = new Random();
            mana -= iceBlast.mana;
            return damageDealt.Next(iceBlast.damage, iceBlast.damage);
        }

        public static int Shock()
        {
            Random damageDealt = new Random();
            mana -= shock.mana;
            return damageDealt.Next(shock.damage - 8, shock.damage + 8);
        }

        public static int IceLance()
        {
            Random damageDealt = new Random();
            mana -= iceLance.mana;
            return damageDealt.Next(iceLance.damage - 5, iceLance.damage + 10);
        }
    }
}
