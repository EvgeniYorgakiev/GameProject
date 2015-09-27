using System;

namespace Rogue
{
    public class Rogue : Heroes.Heroes
    {
        private static Ability backstabAbility = new Ability(25, 10);
        private static Ability multiStrikeAbility = new Ability(25, 10);
        private static Ability shootAbility = new Ability(23, 8);
        private static Ability stealthKillAbility = new Ability(35, 16);

        public Rogue() : base()
        {
            name = "Rogue";
            maxHealth = 95;
            maxMana = 55;
            health = 95;
            mana = 55;
            minDamage = 16;
            maxDamage = 21;
        }

        public int backstab(Enemy.Enemy enemy)
        {
            Random damageDealt = new Random();
            mana -= backstabAbility.mana;
            return damageDealt.Next(backstabAbility.damage - 2, backstabAbility.damage + 2);
        }

        public int multistrike(Enemy.Enemy enemy)
        {
            Random damageDealt = new Random();
            mana -= multiStrikeAbility.mana;
            return damageDealt.Next(multiStrikeAbility.damage - 8, multiStrikeAbility.damage + 8);
        }

        public int shoot(Enemy.Enemy enemy)
        {
            Random damageDealt = new Random();
            mana -= shootAbility.mana;
            return damageDealt.Next(shootAbility.damage - 3, shootAbility.damage + 3);
        }

        public int stealthkill(Enemy.Enemy enemy)
        {
            Random damageDealt = new Random();
            mana -= stealthKillAbility.mana;
            return damageDealt.Next(stealthKillAbility.damage - 5, stealthKillAbility.damage + 10);
        }
    }
}
