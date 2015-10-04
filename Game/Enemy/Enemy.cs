using System;
using IHeroes;
using System.Collections.Generic;

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
        public List<ItemPool.Item> itemsOnDrop = new List<ItemPool.Item>();
        public static List<EnemyClass> listOfAllEnemies = new List<EnemyClass>()
        {
            new Boar(),
            new Skeleton(),
            new SkeletonMage()
        };

        public int attack(IHero hero)
        {
            Random damageDealt = new Random();
            int minDamageAfterArmor = minDamage * (100 - hero.ArmorWithEquipment()) / 100;
            int maxDamageAfterArmor = maxDamage * (100 - hero.ArmorWithEquipment()) / 100;
            return damageDealt.Next(minDamageAfterArmor, maxDamageAfterArmor);
        }

        public void OnDeath()
        {
            health = 0;
            Random random = new Random();
            ItemPool.inventory.Add(itemsOnDrop[random.Next(0, itemsOnDrop.Count)]);
        }
    }

    public class Skeleton : EnemyClass
    {
        private static Ability shieldbashAbility = new Ability(27, 8);

        public Skeleton() : base()
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
            itemsOnDrop = ItemPool.itemsPool.FindAll(item => item.name.Equals ("Chainmail helm") || item.name.Equals("Chainmail boots") ||
                                                                 item.name.Equals("Chainmail gloves") || item.name.Equals("Chainmail armor") ||
                                                                 item.name.Equals("Longsword"));
        }

        public int shieldbash(IHero hero)
        {
            Random damageDealt = new Random();
            mana -= shieldbashAbility.mana;
            int shieldBashDamageAfterArmor = shieldbashAbility.damage * (100 - hero.ArmorWithEquipment()) / 100;
            return damageDealt.Next(shieldBashDamageAfterArmor - 2, shieldBashDamageAfterArmor + 2);
        }
    }

    public class SkeletonMage : EnemyClass
    {
        private static Ability fireBlastAbility = new Ability(27, 8);

        public SkeletonMage() : base()
        {
            maxHealth = 105;
            maxMana = 24;
            health = 105;
            mana = 24;
            minDamage = 15;
            maxDamage = 21;
            armor = 0;
            fireResistance = 5;
            iceResistance = 5;
            lightningResistance = 5;
            experienceWorth = 15;
            itemsOnDrop = ItemPool.itemsPool.FindAll(item => item.name.Equals("Cloth helm") || item.name.Equals("Cloth boots") ||
                                                                 item.name.Equals("Cloth gloves") || item.name.Equals("Cloth armor") ||
                                                                 item.name.Equals("Staff"));
        }

        public int fireblast(IHero hero)
        {
            Random damageDealt = new Random();
            mana -= fireBlastAbility.mana;
            int shieldBashDamageAfterArmor = fireBlastAbility.damage * (100 - hero.ArmorWithEquipment()) / 100;
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
            itemsOnDrop = ItemPool.itemsPool.FindAll(item => item.name.Equals("Leather helm") || item.name.Equals("Leather boots") ||
                                                             item.name.Equals("Leather gloves") || item.name.Equals("Leather armor") ||
                                                             item.name.Equals("Dagger (right hand)"));
        }

        public int charge(IHero hero)
        {
            Random damageDealt = new Random();
            mana -= chargeAbility.mana;
            int chargeDamageAfterArmor = chargeAbility.damage * (100 - hero.baseArmor) / 100;
            return damageDealt.Next(chargeDamageAfterArmor - 2, chargeDamageAfterArmor + 2);
        }
    }
}
