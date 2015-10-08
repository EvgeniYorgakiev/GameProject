using System;

namespace Mage
{
    public class MageClass : HeroesAbilties.HeroAbilties
    {
        public static Ability fireballAbility = new Ability(28, 12, 1);
        public static Ability iceBlastAbility = new Ability(28, 11,3);
        public static Ability shockAbility = new Ability(28, 13,4);
        public static Ability iceLanceAbility = new Ability(40, 18,6);
        public static Ability immolateAbility = new Ability(50, 20, 8);
        public static Ability iceGraspAbility = new Ability(60, 25, 10);
        public static Ability lightningAbility = new Ability(75, 35, 13);
        public static Ability infernoAbility = new Ability(90, 40, 15);
        public static Ability blizzardAbility = new Ability(100, 45, 18);
        public static Ability stormAbility = new Ability(120, 50, 20);

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
            if (level >= fireballAbility.levelRequired)
            {
                mana -= fireballAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int fireballAfterResistance = fireballAbility.averageDamage * (100 - enemy.fireResistance) / 100;
            return damageDealt.Next(fireballAfterResistance - 2, fireballAfterResistance + 2);
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
            int iceBlastAfterResistance = iceBlastAbility.averageDamage * (100 - enemy.iceResistance) / 100;
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
            int shockAfterResistance = shockAbility.averageDamage * (100 - enemy.lightningResistance) / 100;
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
            int iceLanceAfterResistance = iceLanceAbility.averageDamage * (100 - enemy.iceResistance) / 100;
            return damageDealt.Next(iceLanceAfterResistance - 5, iceLanceAfterResistance + 10);
        }

        public int immolate(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= immolateAbility.levelRequired)
            {
                mana -= immolateAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int immolateAfterResistance = immolateAbility.averageDamage * (100 - enemy.fireResistance) / 100;
            return damageDealt.Next(immolateAfterResistance - 7, immolateAfterResistance + 7);
        }

        public int icegrasp(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= iceGraspAbility.levelRequired)
            {
                mana -= iceGraspAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int iceGraspAfterResistance = iceGraspAbility.averageDamage * (100 - enemy.iceResistance) / 100;
            return damageDealt.Next(iceGraspAfterResistance - 10, iceGraspAfterResistance + 10);
        }

        public int lightning(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= lightningAbility.levelRequired)
            {
                mana -= lightningAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int lightningAfterResistance = lightningAbility.averageDamage * (100 - enemy.lightningResistance) / 100;
            return damageDealt.Next(lightningAfterResistance - 15, lightningAfterResistance + 15);
        }

        public int inferno(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= infernoAbility.levelRequired)
            {
                mana -= infernoAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int infernoAfterResistance = infernoAbility.averageDamage * (100 - enemy.fireResistance) / 100;
            return damageDealt.Next(infernoAfterResistance - 10, infernoAfterResistance + 10);
        }

        public int blizzard(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= blizzardAbility.levelRequired)
            {
                mana -= blizzardAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int blizzardAfterResistance = blizzardAbility.averageDamage * (100 - enemy.iceResistance) / 100;
            return damageDealt.Next(blizzardAfterResistance - 15, blizzardAfterResistance + 15);
        }

        public int storm(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= stormAbility.levelRequired)
            {
                mana -= stormAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int stormAfterResistance = stormAbility.averageDamage * (100 - enemy.lightningResistance) / 100;
            return damageDealt.Next(stormAfterResistance - 8, stormAfterResistance + 8);
        }
    }
}
