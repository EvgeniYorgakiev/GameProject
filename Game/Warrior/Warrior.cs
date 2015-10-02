using System;

namespace Warrior
{
    public class WarriorClass : Heroes.Hero
    {
        private static Ability shieldBashAbility = new Ability(30, 8, 1);
        private static Ability assaultAbility = new Ability(35, 10, 3);
        private static Ability overpowerAbility = new Ability(30, 9, 4);
        private static Ability executeAbility = new Ability(35, 6, 6);
        private static Ability hearthStrikeAbility = new Ability(40, 9, 7);

        public WarriorClass() : base()
        {
            name = "Warrior";
            maxHealth = 115;
            maxMana = 35;
            health = 115;
            mana = 35;
            minDamage = 18;
            maxDamage = 23;
            armor = 10;
            fireResistance = 10;
            iceResistance = 10;
            lightningResistance = 10;
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
            int shieldBashAbilityAfterArmor = shieldBashAbility.damage * (100 - enemy.armor) / 100;
            return damageDealt.Next(shieldBashAbilityAfterArmor - 2, shieldBashAbilityAfterArmor + 2);
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
            int assaultAbilityAfterArmor = assaultAbility.damage * (100 - enemy.armor) / 100;
            return damageDealt.Next(assaultAbilityAfterArmor - 8, assaultAbilityAfterArmor + 8);
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
            int overpowerBashAbilityAfterArmor = overpowerAbility.damage * (100 - enemy.armor) / 100;
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
            int executeAbilityAfterArmor = executeAbility.damage * (100 - enemy.armor) / 100;
            return damageDealt.Next(executeAbilityAfterArmor - 5, executeAbilityAfterArmor + 10);
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
            int hearthStrikeAbilityAfterArmor = hearthStrikeAbility.damage * (100 - enemy.armor) / 100;
            return damageDealt.Next(hearthStrikeAbilityAfterArmor - 5, hearthStrikeAbilityAfterArmor + 10);
        }
    }
}
