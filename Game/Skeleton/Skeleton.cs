using System;

namespace Skeleton
{
    public class Skeleton : Enemy.Enemy
    {
        private static Ability shieldbashAbility = new Ability(27, 8);

        public Skeleton() : base()
        {
            maxHealth = 130;
            maxMana = 16;
            health = 130;
            mana = 16;
            minDamage = 18;
            maxDamage = 23;
            difficulty = 1.2f;
        }

        public int shieldbash()
        {
            Random damageDealt = new Random();
            mana -= shieldbashAbility.mana;
            return damageDealt.Next(shieldbashAbility.damage - 2, shieldbashAbility.damage + 2);
        }
    }
}
