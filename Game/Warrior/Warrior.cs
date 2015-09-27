using System;

namespace Warrior
{
    public class Warrior : Heroes.Heroes
    {
        private static Ability shieldBashAbility = new Ability(30, 8);
        private static Ability assaultAbility = new Ability(35, 10);
        private static Ability overpowerAbility = new Ability(30, 9);
        private static Ability executeAbility = new Ability(35, 6);

        public Warrior() : base()
        {
            maxHealth = 115;
            maxMana = 35;
            health = 115;
            mana = 35;
            minDamage = 18;
            maxDamage = 23;
        }

        public int shieldbash(Enemy.Enemy enemy)
        {
            Random damageDealt = new Random();
            mana -= shieldBashAbility.mana;
            return damageDealt.Next(shieldBashAbility.damage - 2, shieldBashAbility.damage + 2);
        }

        public int assault(Enemy.Enemy enemy)
        {
            Random damageDealt = new Random();
            mana -= assaultAbility.mana;
            return damageDealt.Next(assaultAbility.damage - 8, assaultAbility.damage + 8);
        }

        public int overpower(Enemy.Enemy enemy)
        {
            Random damageDealt = new Random();
            mana -= overpowerAbility.mana;
            return damageDealt.Next(overpowerAbility.damage - 3, overpowerAbility.damage + 3);
        }

        public int execute(Enemy.Enemy enemy)
        {
            Random damageDealt = new Random();
            mana -= executeAbility.mana;
            return damageDealt.Next(executeAbility.damage - 5, executeAbility.damage + 10);
        }
    }
}
