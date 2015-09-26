using System;

namespace Boar
{
    public class Boar : Enemy.Enemy
    {
        private static Ability charge = new Ability(25, 8);

        public Boar() : base()
        {
            maxHealth = 120;
            maxMana = 16;
            health = 120;
            mana = 16;
            minDamage = 18;
            maxDamage = 23;
        }

        public static int Charge()
        {
            Random damageDealt = new Random();
            mana -= charge.mana;
            return damageDealt.Next(charge.damage - 2, charge.damage + 2);
        }
    }
}
