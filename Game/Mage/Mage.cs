using System;

namespace Mage
{
    public class MageClass : Heroes.Hero
    {
        public static Ability Fireball = new Ability(28, 12, 1);
        public static Ability IceBlast = new Ability(28, 11,3);
        public static Ability Shock = new Ability(28, 13,4);
        public static Ability IceLance = new Ability(40, 18,6);

        public MageClass() : base()
        {
            name = "Mage";
            maxHealth = 60;
            maxMana = 90;
            health = 60;
            mana = 90;
            baseMinDamage = 13;
            baseMaxDamage = 19;
            baseArmor = 0;
            baseFireResistance = 15;
            baseIceResistance = 15;
            baseLightningResistance = 15;
            maxHealthOnLevelUp = 3.5f;
            maxManaOnLevelUp = 4.5f;
            minDamageOnLevelUp = 1.2f;
            maxDamageOnLevelUp = 1.2f;
        }

        public int fireball(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= Fireball.levelRequired)
            {
                mana -= Fireball.mana;
            }
            else
            {
                mana -= 1000;
            }
            int fireballAfterResistance = Fireball.averageDamage * (100 - enemy.fireResistance) / 100;
            return damageDealt.Next(Fireball.averageDamage - 2, Fireball.averageDamage + 2);
        }

        public int iceblast(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= IceBlast.levelRequired)
            {
                mana -= IceBlast.mana;
            }
            else
            {
                mana -= 1000;
            }
            int iceBlastAfterResistance = IceBlast.averageDamage * (100 - enemy.iceResistance) / 100;
            return damageDealt.Next(iceBlastAfterResistance - 4, iceBlastAfterResistance + 4);
        }

        public int shock(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= Shock.levelRequired)
            {
                mana -= Shock.mana;
            }
            else
            {
                mana -= 1000;
            }
            int shockAfterResistance = Shock.averageDamage * (100 - enemy.lightningResistance) / 100;
            return damageDealt.Next(shockAfterResistance - 8, shockAfterResistance + 8);
        }

        public int icelance(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= IceLance.levelRequired)
            {
                mana -= IceLance.mana;
            }
            else
            {
                mana -= 1000;
            }
            int iceLanceAfterResistance = IceLance.averageDamage * (100 - enemy.iceResistance) / 100;
            return damageDealt.Next(iceLanceAfterResistance - 5, iceLanceAfterResistance + 10);
        }
    }
}
