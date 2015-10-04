using System;

namespace Warrior
{
    public class WarriorClass : Heroes.Hero
    {
        public static Ability Shieldbash = new Ability(30, 8, 1);
        public static Ability Assault = new Ability(35, 10, 3);
        public static Ability Overpower = new Ability(30, 9, 4);
        public static Ability Execute = new Ability(35, 6, 6);
        public static Ability HeartStrike = new Ability(40, 9, 7);

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
            maxManaOnLevelUp = 2f;
            minDamageOnLevelUp = 1.5f;
            maxDamageOnLevelUp = 1.5f;
        }

        public int shieldbash(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= Shieldbash.levelRequired)
            {
                mana -= Shieldbash.mana;
            }
            else
            {
                mana -= 1000;
            }
            int shieldBashAbilityAfterArmor = Shieldbash.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(shieldBashAbilityAfterArmor - 2, shieldBashAbilityAfterArmor + 2);
        }

        public int assault(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= Assault.levelRequired)
            {
                mana -= Assault.mana;
            }
            else
            {
                mana -= 1000;
            }
            int assaultAbilityAfterArmor = Assault.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(assaultAbilityAfterArmor - 8, assaultAbilityAfterArmor + 8);
        }

        public int overpower(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= Overpower.levelRequired)
            {
                mana -= Overpower.mana;
            }
            else
            {
                mana -= 1000;
            }
            int overpowerBashAbilityAfterArmor = Overpower.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(overpowerBashAbilityAfterArmor - 3, overpowerBashAbilityAfterArmor+ 3);
        }

        public int execute(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= Execute.levelRequired)
            {
                mana -= Execute.mana;
            }
            else
            {
                mana -= 1000;
            }
            int executeAbilityAfterArmor = Execute.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(executeAbilityAfterArmor - 5, executeAbilityAfterArmor + 10);
        }

        public int hearthstrike(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= Execute.levelRequired)
            {
                mana -= Execute.mana;
            }
            else
            {
                mana -= 1000;
            }
            int hearthStrikeAbilityAfterArmor = HeartStrike.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(hearthStrikeAbilityAfterArmor - 5, hearthStrikeAbilityAfterArmor + 10);
        }
    }
}
