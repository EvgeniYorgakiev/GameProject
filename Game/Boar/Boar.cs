using System;

namespace Boar
{
    public class Boar : Enemy.Enemy
    {
        private static Ability chargeAbility = new Ability(25, 8);

        public Boar() : base()
        {
            maxHealth = 120;
            maxMana = 16;
            health = 120;
            mana = 16;
            minDamage = 18;
            maxDamage = 23;
            difficulty = 1;
        }

        public int charge()
        {
            Random damageDealt = new Random();
            mana -= chargeAbility.mana;
            return damageDealt.Next(chargeAbility.damage - 2, chargeAbility.damage + 2);
        }
    }
}
