using System;

namespace Skeleton
{
    public class Skeleton : Enemy.Enemy
    {
        private static Ability shieldBash = new Ability(27, 8);

        public Skeleton() : base()
        {
            maxHealth = 130;
            maxMana = 16;
            health = 130;
            mana = 16;
            minDamage = 18;
            maxDamage = 23;
        }

        public static int Charge()
        {
            Random damageDealt = new Random();
            mana -= shieldBash.mana;
            return damageDealt.Next(shieldBash.damage - 2, shieldBash.damage + 2);
        }
    }
}
