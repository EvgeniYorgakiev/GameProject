using System;

namespace Rogue
{
    public class RogueClass : Heroes.Hero
    {
        private static Ability backstabAbility = new Ability(32, 10, 1);
        private static Ability multiStrikeAbility = new Ability(35, 10,3);
        private static Ability shootAbility = new Ability(30, 8, 4);
        private static Ability stealthKillAbility = new Ability(35, 16, 6);

        public RogueClass() : base()
        {
            name = "Rogue";
            maxHealth = 95;
            maxMana = 55;
            health = 95;
            mana = 55;
            minDamage = 20;
            maxDamage = 25;
            armor = 5;
            fireResistance = 0;
            iceResistance = 0;
            lightningResistance = 0;
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
            int backstabAbilityAfterArmor = backstabAbility.damage * (100 - enemy.armor) / 100;
            return damageDealt.Next(backstabAbilityAfterArmor - 2, backstabAbilityAfterArmor + 2);
        }

        public int multistrike(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= multiStrikeAbility.levelRequired)
            {
                mana -= multiStrikeAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int multistrikeAfterArmor = multiStrikeAbility.damage * (100 - enemy.armor) / 100;
            return damageDealt.Next(multistrikeAfterArmor - 8, multistrikeAfterArmor + 8);
        }

        public int shoot(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            if (level >= shootAbility.levelRequired)
            {
                mana -= shootAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            int shootAbilityAfterArmor = shootAbility.damage * (100 - enemy.armor) / 100;
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
            int stealthKillAbilityAfterArmor = stealthKillAbility.damage * (100 - enemy.armor) / 100;
            return damageDealt.Next(stealthKillAbilityAfterArmor - 5, stealthKillAbilityAfterArmor + 10);
        }
    }
}
