using System;

namespace Priest
{
    public class PriestClass : Heroes.Hero
    {
        private static Ability healAbility = new Ability(15, 12,1);
        private static Ability rejuvanateAbility = new Ability(16, 11,3);
        private static Ability regenarateAbility = new Ability(15, 18,4);
        private static Ability reviveAbility = new Ability(0, 30,6);

        public PriestClass() : base()
        {
            name = "Priest";
            maxHealth = 70;
            maxMana = 80;
            health = 70;
            mana = 80;
            minDamage = 11;
            maxDamage = 16;
            armor = 0;
            fireResistance = 0;
            iceResistance = 0;
            lightningResistance = 0;
            maxHealthOnLevelUp = 4f;
            maxManaOnLevelUp = 4f;
            minDamageOnLevelUp = 1.1f;
            maxDamageOnLevelUp = 1.1f;
        }

        public int heal()
        {
            Random damageDealt = new Random();
            if (level >= healAbility.levelRequired)
            {
                mana -= healAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            return damageDealt.Next(healAbility.damage - 2, healAbility.damage + 2);
        }

        public int rejuvanate()
        {
            Random damageDealt = new Random();
            if (level >= rejuvanateAbility.levelRequired)
            {
                mana -= rejuvanateAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            return damageDealt.Next(rejuvanateAbility.damage, rejuvanateAbility.damage);
        }

        public int regenerate()
        {
            Random damageDealt = new Random();
            if (level >= regenarateAbility.levelRequired)
            {
                mana -= regenarateAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            return damageDealt.Next(regenarateAbility.damage - 8, regenarateAbility.damage + 8);
        }

        public void revive(Heroes.Hero hero)
        {
            if (level >= reviveAbility.levelRequired)
            {
                mana -= reviveAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            hero.health = hero.maxHealth / 2;
            hero.mana = hero.maxMana / 4;
        }
    }
}
