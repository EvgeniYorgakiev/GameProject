using System;

namespace Mage
{
    public class MageClass : Heroes.Hero
    {
        private static Ability fireBallAbility = new Ability(28, 12, 1);
        private static Ability iceBlastAbility = new Ability(28, 11,3);
        private static Ability shockAbility = new Ability(28, 13,4);
        private static Ability iceLanceAbility = new Ability(40, 18,6);

        public MageClass() : base()
        {
            name = "Mage";
            maxHealth = 60;
            maxMana = 90;
            health = 60;
            mana = 90;
            minDamage = 13;
            maxDamage = 19;
            armor = 0;
            fireResistance = 15;
            iceResistance = 15;
            lightningResistance = 15;
            maxHealthOnLevelUp = 3.5f;
            maxManaOnLevelUp = 4.5f;
            minDamageOnLevelUp = 1.2f;
            maxDamageOnLevelUp = 1.2f;
        }

        public int fireball(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= fireBallAbility.levelRequired)
            {
                mana -= fireBallAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int fireballAfterResistance = fireBallAbility.damage * (100 - enemy.fireResistance) / 100;
            return damageDealt.Next(fireBallAbility.damage - 2, fireBallAbility.damage + 2);
        }

        public int iceblast(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= iceBlastAbility.levelRequired)
            {
                mana -= iceBlastAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int iceBlastAfterResistance = iceBlastAbility.damage * (100 - enemy.iceResistance) / 100;
            return damageDealt.Next(iceBlastAfterResistance - 4, iceBlastAfterResistance + 4);
        }

        public int shock(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= shockAbility.levelRequired)
            {
                mana -= shockAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int shockAfterResistance = shockAbility.damage * (100 - enemy.lightningResistance) / 100;
            return damageDealt.Next(shockAfterResistance - 8, shockAfterResistance + 8);
        }

        public int icelance(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= iceLanceAbility.levelRequired)
            {
                mana -= iceLanceAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int iceLanceAfterResistance = iceLanceAbility.damage * (100 - enemy.iceResistance) / 100;
            return damageDealt.Next(iceLanceAfterResistance - 5, iceLanceAfterResistance + 10);
        }
    }
}
