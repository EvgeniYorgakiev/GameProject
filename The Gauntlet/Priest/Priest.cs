using System;

namespace Priest
{
    public class PriestClass : Heroes.Hero
    {
        public static Ability flashHealAbility = new Ability(15, 12,1);
        public static Ability rejuvanateAbility = new Ability(16, 11,3);
        public static Ability regenarationAbility = new Ability(25, 18,4);
        public static Ability reviveAbility = new Ability(0, 30,6);
        public static Ability healAbility = new Ability(30, 20, 8);
        public static Ability renewAbility = new Ability(35, 22, 10);
        public static Ability serendipityAbility = new Ability(40, 25, 13);
        public static Ability redemptionAbility = new Ability(45, 30, 15);
        public static Ability godHelpAbility = new Ability(50, 35, 18);
        public static Ability godInterventionAbility = new Ability(60, 40, 20);

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

        public int flashheal()
        {
            Random damageDealt = new Random();
            if (level >= flashHealAbility.levelRequired)
            {
                mana -= flashHealAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            return damageDealt.Next(flashHealAbility.averageDamage - 2, flashHealAbility.averageDamage + 2);
        }

        public int rejuvanate()
        {
            if (level >= rejuvanateAbility.levelRequired)
            {
                mana -= rejuvanateAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            return rejuvanateAbility.averageDamage;
        }

        public int regenerate()
        {
            Random damageDealt = new Random();
            if (level >= regenarationAbility.levelRequired)
            {
                mana -= regenarationAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            return damageDealt.Next(regenarationAbility.averageDamage - 8, regenarationAbility.averageDamage + 8);
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
            if(hero != null)
            {
                hero.health = hero.maxHealth / 2;
                hero.mana = hero.maxMana / 4;
            }
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
            return damageDealt.Next(healAbility.averageDamage - 5, healAbility.averageDamage + 5);
        }

        public int renew()
        {
            Random damageDealt = new Random();
            if (level >= renewAbility.levelRequired)
            {
                mana -= renewAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            return damageDealt.Next(renewAbility.averageDamage - 5, renewAbility.averageDamage + 5);
        }

        public int serendipity()
        {
            Random damageDealt = new Random();
            if (level >= serendipityAbility.levelRequired)
            {
                mana -= serendipityAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            return damageDealt.Next(serendipityAbility.averageDamage - 6, serendipityAbility.averageDamage + 6);
        }

        public int redemption()
        {
            Random damageDealt = new Random();
            if (level >= redemptionAbility.levelRequired)
            {
                mana -= redemptionAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            return damageDealt.Next(redemptionAbility.averageDamage - 8, redemptionAbility.averageDamage + 8);
        }

        public int godhelp()
        {
            Random damageDealt = new Random();
            if (level >= godHelpAbility.levelRequired)
            {
                mana -= godHelpAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            return damageDealt.Next(godHelpAbility.averageDamage - 12, godHelpAbility.averageDamage + 12);
        }

        public int godintervention()
        {
            Random damageDealt = new Random();
            if (level >= godInterventionAbility.levelRequired)
            {
                mana -= godInterventionAbility.mana;
            }
            else
            {
                mana -= 1000;
            }
            return damageDealt.Next(godInterventionAbility.averageDamage - 14, godInterventionAbility.averageDamage + 14);
        }
    }
}
