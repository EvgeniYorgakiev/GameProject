using System;

namespace Mage
{
    public class Mage : Heroes.Heroes
    {
        private static Ability fireBallAbility = new Ability(28, 12);
        private static Ability iceBlastAbility = new Ability(28, 11);
        private static Ability shockAbility = new Ability(28, 13);
        private static Ability iceLanceAbility = new Ability(40, 18);

        public Mage() : base()
        {
            maxHealth = 60;
            maxMana = 90;
            health = 60;
            mana = 90;
            minDamage = 13;
            maxDamage = 19;
        }

        public int fireball(Enemy.Enemy enemy)
        {
            Random damageDealt = new Random();
            mana -= fireBallAbility.mana;
            return damageDealt.Next(fireBallAbility.damage - 2, fireBallAbility.damage + 2);
        }

        public int iceblast(Enemy.Enemy enemy)
        {
            Random damageDealt = new Random();
            mana -= iceBlastAbility.mana;
            return damageDealt.Next(iceBlastAbility.damage, iceBlastAbility.damage);
        }

        public int shock(Enemy.Enemy enemy)
        {
            Random damageDealt = new Random();
            mana -= shockAbility.mana;
            return damageDealt.Next(shockAbility.damage - 8, shockAbility.damage + 8);
        }

        public int icelance(Enemy.Enemy enemy)
        {
            Random damageDealt = new Random();
            mana -= iceLanceAbility.mana;
            return damageDealt.Next(iceLanceAbility.damage - 5, iceLanceAbility.damage + 10);
        }
    }
}
