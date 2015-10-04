using System;

namespace Rogue
{
    public class RogueClass : Heroes.Hero
    {
        public static Ability Backstab = new Ability(32, 10, 1);
        public static Ability Multistrike = new Ability(35, 10,3);
        public static Ability BelowTheBelt = new Ability(30, 8, 4);
        public static Ability StealthKill = new Ability(35, 16, 6);

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
            if(level >= Backstab.levelRequired)
            {
                mana -= Backstab.mana;
            }
            else
            {
                mana -= 1000;
            }
            int backstabAbilityAfterArmor = Backstab.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(backstabAbilityAfterArmor - 2, backstabAbilityAfterArmor + 2);
        }

        public int multistrike(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= Multistrike.levelRequired)
            {
                mana -= Multistrike.mana;
            }
            else
            {
                mana -= 1000;
            }
            int multistrikeAfterArmor = Multistrike.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(multistrikeAfterArmor - 8, multistrikeAfterArmor + 8);
        }

        public int belowthebelt(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= BelowTheBelt.levelRequired)
            {
                mana -= BelowTheBelt.mana;
            }
            else
            {
                mana -= 1000;
            }
            int shootAbilityAfterArmor = BelowTheBelt.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(shootAbilityAfterArmor - 3, shootAbilityAfterArmor + 3);
        }

        public int stealthkill(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= StealthKill.levelRequired)
            {
                mana -= StealthKill.mana;
            }
            else
            {
                mana -= 1000;
            }
            int stealthKillAbilityAfterArmor = StealthKill.averageDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(stealthKillAbilityAfterArmor - 5, stealthKillAbilityAfterArmor + 10);
        }
    }
}
