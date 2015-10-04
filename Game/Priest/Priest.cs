using System;

namespace Priest
{
    public class PriestClass : Heroes.Hero
    {
        public static Ability Heal = new Ability(15, 12,1);
        public static Ability Rejuvanate = new Ability(16, 11,3);
        public static Ability Regenaration = new Ability(15, 18,4);
        public static Ability Revive = new Ability(0, 30,6);

        public PriestClass() : base()
        {
            name = "Priest";
            maxHealth = 70;
            maxMana = 80;
            health = 70;
            mana = 80;
            baseMinDamage = 11;
            baseMaxDamage = 16;
            baseArmor = 0;
            baseFireResistance = 0;
            baseIceResistance = 0;
            baseLightningResistance = 0;
            maxHealthOnLevelUp = 4f;
            maxManaOnLevelUp = 4f;
            minDamageOnLevelUp = 1.1f;
            maxDamageOnLevelUp = 1.1f;
        }

        public int heal()
        {
            Random damageDealt = new Random();
            if (level >= Heal.levelRequired)
            {
                mana -= Heal.mana;
            }
            else
            {
                mana -= 1000;
            }
            return damageDealt.Next(Heal.averageDamage - 2, Heal.averageDamage + 2);
        }

        public int rejuvanate()
        {
            if (level >= Rejuvanate.levelRequired)
            {
                mana -= Rejuvanate.mana;
            }
            else
            {
                mana -= 1000;
            }
            return Rejuvanate.averageDamage;
        }

        public int regenerate()
        {
            Random damageDealt = new Random();
            if (level >= Regenaration.levelRequired)
            {
                mana -= Regenaration.mana;
            }
            else
            {
                mana -= 1000;
            }
            return damageDealt.Next(Regenaration.averageDamage - 8, Regenaration.averageDamage + 8);
        }

        public void revive(Heroes.Hero hero)
        {
            if (level >= Revive.levelRequired)
            {
                mana -= Revive.mana;
            }
            else
            {
                mana -= 1000;
            }
            if(hero != null)
            {
                hero.health = hero.maxHealth / 2;
                hero.mana = hero.maxMana / 4;
            }
        }
    }
}
