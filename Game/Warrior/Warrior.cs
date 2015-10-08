using System;

namespace Warrior
{
    public class WarriorClass : Heroes.Hero
    {
        public static Ability rendAbility = new Ability(30, 8, 1);
        public static Ability assaultAbility = new Ability(35, 10, 3);
        public static Ability overpowerAbility = new Ability(40, 12, 4);
        public static Ability executeAbility = new Ability(45, 15, 6);
        public static Ability heartStrikeAbility = new Ability(40, 17, 8);
        public static Ability mightyBlowAbility = new Ability(45, 20, 10);
        public static Ability whirlwindAbility = new Ability(55, 25, 13);
        public static Ability rampageAbility = new Ability(60, 28, 15);
        public static Ability shieldBashAbility = new Ability(70, 32, 18);
        public static Ability mortalStrikeAbility = new Ability(80, 35, 20);

        public WarriorClass() : base()
        {
            name = "Warrior";
            maxHealth = 115;
            maxMana = 35;
            health = 115;
            mana = 35;
            baseMinDamage = 18;
            baseMaxDamage = 23;
            baseArmor = 10;
            baseFireResistance = 10;
            baseIceResistance = 10;
            baseLightningResistance = 10;
            maxHealthOnLevelUp = 7f;
            maxManaOnLevelUp = 1f;
            minDamageOnLevelUp = 1.5f;
            maxDamageOnLevelUp = 1.5f;
        }

        public int rend(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= rendAbility.levelRequired)
            {
                mana -= rendAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int rendAbilityAfterArmor = rendAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(rendAbilityAfterArmor - 2, rendAbilityAfterArmor + 2);
        }

        public int assault(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= assaultAbility.levelRequired)
            {
                mana -= assaultAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int assaultAbilityAfterArmor = assaultAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(assaultAbilityAfterArmor - 5, assaultAbilityAfterArmor + 5);
        }

        public int overpower(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= overpowerAbility.levelRequired)
            {
                mana -= overpowerAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int overpowerBashAbilityAfterArmor = overpowerAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(overpowerBashAbilityAfterArmor - 3, overpowerBashAbilityAfterArmor+ 3);
        }

        public int execute(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= executeAbility.levelRequired)
            {
                mana -= executeAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int executeAbilityAfterArmor = executeAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(executeAbilityAfterArmor - 4, executeAbilityAfterArmor + 4);
        }

        public int hearthstrike(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= executeAbility.levelRequired)
            {
                mana -= executeAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int hearthStrikeAbilityAfterArmor = heartStrikeAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(hearthStrikeAbilityAfterArmor - 5, hearthStrikeAbilityAfterArmor + 5);
        }

        public int mightyblow(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= mightyBlowAbility.levelRequired)
            {
                mana -= mightyBlowAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int mightyBlowAbilityAfterArmor = mightyBlowAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(mightyBlowAbilityAfterArmor - 7, mightyBlowAbilityAfterArmor + 7);
        }

        public int whirlwind(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= whirlwindAbility.levelRequired)
            {
                mana -= whirlwindAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int whirlwindAbilityAfterArmor = whirlwindAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(whirlwindAbilityAfterArmor - 8, whirlwindAbilityAfterArmor + 8);
        }

        public int rampage(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= rampageAbility.levelRequired)
            {
                mana -= rampageAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int rampageAbilityAfterArmor = rampageAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(rampageAbilityAfterArmor - 10, rampageAbilityAfterArmor + 10);
        }

        public int shieldbash(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= shieldBashAbility.levelRequired)
            {
                mana -= shieldBashAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int shieldBashAbilityAfterArmor = shieldBashAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(shieldBashAbilityAfterArmor - 12, shieldBashAbilityAfterArmor + 12);
        }

        public int mortalstrike(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= mortalStrikeAbility.levelRequired)
            {
                mana -= mortalStrikeAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int mortalStrikeAbilityAfterArmor = mortalStrikeAbility.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(mortalStrikeAbilityAfterArmor - 15, mortalStrikeAbilityAfterArmor + 15);
        }
    }
}
