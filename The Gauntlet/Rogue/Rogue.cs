using System;

namespace Rogue
{
    public class RogueClass : HeroesAbilties.HeroAbilties
    {
        public static Ability backstabAbility = new Ability(32, 10, 1);
        public static Ability multistrikeAbility = new Ability(35, 10,3);
        public static Ability belowTheBeltAbility = new Ability(30, 8, 4);
        public static Ability stealthKillAbility = new Ability(35, 16, 6);
        public static Ability twinFangsAbility = new Ability(40, 18, 8);
        public static Ability spinningBladesAbility = new Ability(50, 22, 10);
        public static Ability hookAndTackleAbility = new Ability(60, 30, 13);
        public static Ability ambushAbility = new Ability(65, 35, 15);
        public static Ability shadowStrikeAbility = new Ability(75, 40, 18);
        public static Ability throatCutAbility = new Ability(90, 45, 20);

        public RogueClass() : base()
        {
            name = "Rogue";
            maxHealth = 95;
            maxMana = 55;
            health = 95;
            mana = 55;
            baseMinDamage = 20;
            baseMaxDamage = 25;
            baseArmor = 5;
            baseFireResistance = 0;
            baseIceResistance = 0;
            baseLightningResistance = 0;
            maxHealthOnLevelUp = 5f;
            maxManaOnLevelUp = 3f;
            minDamageOnLevelUp = 1.8f;
            maxDamageOnLevelUp = 1.8f;
        }

        public int backstab(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if(level >= backstabAbility.levelRequired)
            {
                mana -= backstabAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int backstabAbilityAfterArmor = backstabAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(backstabAbilityAfterArmor - 2, backstabAbilityAfterArmor + 2);
        }

        public int multistrike(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= multistrikeAbility.levelRequired)
            {
                mana -= multistrikeAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int multistrikeAfterArmor = multistrikeAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(multistrikeAfterArmor - 8, multistrikeAfterArmor + 8);
        }

        public int belowthebelt(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= belowTheBeltAbility.levelRequired)
            {
                mana -= belowTheBeltAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int shootAbilityAfterArmor = belowTheBeltAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(shootAbilityAfterArmor - 3, shootAbilityAfterArmor + 3);
        }

        public int stealthkill(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= stealthKillAbility.levelRequired)
            {
                mana -= stealthKillAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int stealthKillAbilityAfterArmor = stealthKillAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(stealthKillAbilityAfterArmor - 5, stealthKillAbilityAfterArmor + 10);
        }

        public int twinfangs(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= twinFangsAbility.levelRequired)
            {
                mana -= twinFangsAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int twinFangsAbilityAfterArmor = twinFangsAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(twinFangsAbilityAfterArmor - 5, twinFangsAbilityAfterArmor + 10);
        }

        public int spinningblades(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= spinningBladesAbility.levelRequired)
            {
                mana -= spinningBladesAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int spinningBladesAbilityAfterArmor = spinningBladesAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(spinningBladesAbilityAfterArmor - 5, spinningBladesAbilityAfterArmor + 10);
        }

        public int hookandtackle(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= hookAndTackleAbility.levelRequired)
            {
                mana -= hookAndTackleAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int hookAndTackleAbilityAfterArmor = hookAndTackleAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(hookAndTackleAbilityAfterArmor - 5, hookAndTackleAbilityAfterArmor + 10);
        }

        public int ambush(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= ambushAbility.levelRequired)
            {
                mana -= ambushAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int ambushAbilityAfterArmor = ambushAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(ambushAbilityAfterArmor - 5, ambushAbilityAfterArmor + 10);
        }

        public int shadowstrike(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= shadowStrikeAbility.levelRequired)
            {
                mana -= shadowStrikeAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int shadowStrikeAbilityAfterArmor = shadowStrikeAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(shadowStrikeAbilityAfterArmor - 5, shadowStrikeAbilityAfterArmor + 10);
        }

        public int throatcut(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= throatCutAbility.levelRequired)
            {
                mana -= throatCutAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int throatCutAbilityAfterArmor = throatCutAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(throatCutAbilityAfterArmor - 5, throatCutAbilityAfterArmor + 10);
        }
    }
}
