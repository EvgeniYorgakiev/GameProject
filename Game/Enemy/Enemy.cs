using System;

namespace Enemy
{
    public class EnemyClass
    {
        public struct Ability
        {
            public int damage;
            public int mana;

            public Ability(int damage, int mana)
            {
                this.damage = damage;
                this.mana = mana;
            }
        }

        public string name;
        public int health;
        public int maxHealth;
        public int mana;
        public int maxMana;
        public int minDamage;
        public int maxDamage;
        public int armor;
        public int fireResistance;
        public int iceResistance;
        public int lightningResistance;
        public int experienceWorth;

        public int attack(IHeroes.IHero hero)
        {
            Random damageDealt = new Random();
            int minDamageAfterArmor = minDamage * (100 - hero.armor) / 100;
            int maxDamageAfterArmor = maxDamage * (100 - hero.armor) / 100;
            return damageDealt.Next(minDamageAfterArmor, maxDamageAfterArmor);
        }
    }

    public class SkeletonClass : EnemyClass
    {
        private static Ability shieldbashAbility = new Ability(27, 8);

        public SkeletonClass() : base()
        {
            maxHealth = 130;
            maxMana = 16;
            health = 130;
            mana = 16;
            minDamage = 18;
            maxDamage = 23;
            armor = 10;
            fireResistance = 0;
            iceResistance = 0;
            lightningResistance = 0;
            experienceWorth = 12;
        }

        public int shieldbash(IHeroes.IHero hero)
        {
            Random damageDealt = new Random();
            mana -= shieldbashAbility.mana;
            int shieldBashDamageAfterArmor = shieldbashAbility.damage * (100 - hero.armor) / 100;
            return damageDealt.Next(shieldBashDamageAfterArmor - 2, shieldBashDamageAfterArmor + 2);
        }
    }

    public class Boar : EnemyClass
    {
        private static Ability chargeAbility = new Ability(25, 8);

        public Boar() : base()
        {
            maxHealth = 120;
            maxMana = 16;
            health = 120;
            mana = 16;
            minDamage = 18;
            maxDamage = 23;
            armor = 0;
            fireResistance = 0;
            iceResistance = 0;
            lightningResistance = 0;
            experienceWorth = 10;
        }

        public int charge(IHeroes.IHero hero)
        {
            Random damageDealt = new Random();
            mana -= chargeAbility.mana;
            int chargeDamageAfterArmor = chargeAbility.damage * (100 - hero.armor) / 100;
            return damageDealt.Next(chargeDamageAfterArmor - 2, chargeDamageAfterArmor + 2);
        }
    }
}
