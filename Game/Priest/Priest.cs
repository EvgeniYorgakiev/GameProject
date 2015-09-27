using System;

namespace Priest
{
    public class Priest : Heroes.Heroes
    {
        private static Ability healAbility = new Ability(15, 12);
        private static Ability rejuvanateAbility = new Ability(16, 11);
        private static Ability regenarateAbility = new Ability(15, 18);
        private static Ability reviveAbility = new Ability(0, 30);

        public Priest() : base()
        {
            maxHealth = 70;
            maxMana = 80;
            health = 70;
            mana = 80;
            minDamage = 11;
            maxDamage = 16;
        }

        public int heal()
        {
            Random damageDealt = new Random();
            mana -= healAbility.mana;
            return damageDealt.Next(healAbility.damage - 2, healAbility.damage + 2);
        }

        public int rejuvanate()
        {
            Random damageDealt = new Random();
            mana -= rejuvanateAbility.mana;
            return damageDealt.Next(rejuvanateAbility.damage, rejuvanateAbility.damage);
        }

        public int regenerate()
        {
            Random damageDealt = new Random();
            mana -= regenarateAbility.mana;
            return damageDealt.Next(regenarateAbility.damage - 8, regenarateAbility.damage + 8);
        }

        public void revive(Heroes.Heroes hero)
        {
            hero.health = 40;
            hero.mana = 20;
        }
    }
}
