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
            new ArmoredWolf(),
            new Spider(),
            new Centaur(),
            new Goblin(),
            new OrcCrusher(),
            new Shaman(),
            new Cyclops(),
            new Bandit(),
            new DesertedGuard(),
            new Sorcerer(),
            new BanditLeader()
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

    public class ArmoredWolf : EnemyClass
    {
        private static Ability biteAbility = new Ability(27, 8);

        public ArmoredWolf() : base()
        {
            name = "Armored Wolf";
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
            experienceWorth = 36;
            itemsOnDrop = ItemPool.itemsPool.FindAll(item => item.name.Equals ("Chainmail helm") || item.name.Equals("Chainmail boots") ||
                                                                 item.name.Equals("Chainmail gloves") || item.name.Equals("Chainmail armor") ||
                                                                 item.name.Equals("Longsword"));
        }

        public int bite(IHero hero)
        {
            Random damageDealt = new Random();
            mana -= biteAbility.mana;
            int biteDamageAfterArmor = biteAbility.damage * (100 - hero.ArmorWithEquipment()) / 100;
            return damageDealt.Next(biteDamageAfterArmor - 2, biteDamageAfterArmor + 2);
        }
    }

    public class Spider : EnemyClass
    {
        private static Ability biteAbility = new Ability(30, 8);

        public Spider() : base()
        {
            name = "Spider";
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
            experienceWorth = 45;
            itemsOnDrop = ItemPool.itemsPool.FindAll(item => item.name.Equals("Cloth helm") || item.name.Equals("Cloth boots") ||
                                                                 item.name.Equals("Cloth gloves") || item.name.Equals("Cloth armor") ||
                                                                 item.name.Equals("Staff"));
        }

        public int bite(IHero hero)
        {
            Random damageDealt = new Random();
            mana -= biteAbility.mana;
            int biteDamageAfterArmor = biteAbility.damage * (100 - hero.ArmorWithEquipment()) / 100;
            return damageDealt.Next(biteDamageAfterArmor - 2, biteDamageAfterArmor + 2);
        }
    }

    public class Boar : EnemyClass
    {
        private static Ability chargeAbility = new Ability(25, 8);

        public Boar() : base()
        {
            name = "Boar";
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
            experienceWorth = 30;
            itemsOnDrop = ItemPool.itemsPool.FindAll(item => item.name.Equals("Leather helm") || item.name.Equals("Leather boots") ||
                                                             item.name.Equals("Leather gloves") || item.name.Equals("Leather armor") ||
                                                             item.name.Equals("Dagger (right hand)"));
        }

        public int charge(IHero hero)
        {
            Random damageDealt = new Random();
            mana -= chargeAbility.mana;
            int chargeDamageAfterArmor = chargeAbility.damage * (100 - hero.ArmorWithEquipment()) / 100;
            return damageDealt.Next(chargeDamageAfterArmor - 2, chargeDamageAfterArmor + 2);
        }
    }

    public class Centaur : EnemyClass
    {
        private static Ability chargeAbility = new Ability(40, 8);
        private static Ability stopmAbility = new Ability(50, 12);

        public Centaur() : base()
        {
            name = "Centaur";
            maxHealth = 400;
            maxMana = 50;
            health = 400;
            mana = 50;
            minDamage = 26;
            maxDamage = 30;
            armor = 20;
            fireResistance = 20;
            iceResistance = 20;
            lightningResistance = 20;
            experienceWorth = 150;
            itemsOnDrop = ItemPool.itemsPool.FindAll(item => item.name.Equals("Blessed staff") || item.name.Equals("Blessed longsword") ||
                                                             item.name.Equals("Blessed dagger (right hand)"));
        }

        public int charge(IHero hero)
        {
            Random damageDealt = new Random();
            mana -= chargeAbility.mana;
            int chargeDamageAfterArmor = chargeAbility.damage * (100 - hero.baseArmor) / 100;
            return damageDealt.Next(chargeDamageAfterArmor - 5, chargeDamageAfterArmor + 5);
        }

        public int stomp(IHero hero)
        {
            Random damageDealt = new Random();
            mana -= stopmAbility.mana;
            int stompDamageAfterArmor = stopmAbility.damage * (100 - hero.ArmorWithEquipment()) / 100;
            return damageDealt.Next(stompDamageAfterArmor - 7, stompDamageAfterArmor + 7);
        }
    }
    public class Goblin : EnemyClass
    {
        private static Ability stabAbility = new Ability(35, 10);

        public Goblin() : base()
        {
            name = "Goblin";
            maxHealth = 150;
            maxMana = 20;
            health = 150;
            mana = 20;
            minDamage = 25;
            maxDamage = 30;
            armor = 5;
            fireResistance = 0;
            iceResistance = 0;
            lightningResistance = 0;
            experienceWorth = 60;
            itemsOnDrop = ItemPool.itemsPool.FindAll(item => item.name.Equals("Studded helm") || item.name.Equals("Studded boots") ||
                                                                 item.name.Equals("Studded gloves") || item.name.Equals("Studded armor") ||
                                                                 item.name.Equals("Dagger (left hand)"));
        }

        public int stab(IHero hero)
        {
            Random damageDealt = new Random();
            mana -= stabAbility.mana;
            int stabDamageAfterArmor = stabAbility.damage * (100 - hero.ArmorWithEquipment()) / 100;
            return damageDealt.Next(stabDamageAfterArmor - 6, stabDamageAfterArmor + 6);
        }
    }

    public class OrcCrusher : EnemyClass
    {
        private static Ability smashAbility = new Ability(40, 5);
        private static Ability bashAbility = new Ability(30, 3);

        public OrcCrusher() : base()
        {
            name = "Orc Crusher";
            maxHealth = 170;
            maxMana = 15;
            health = 170;
            mana = 15;
            minDamage = 25;
            maxDamage = 30;
            armor = 10;
            fireResistance = 5;
            iceResistance = 5;
            lightningResistance = 5;
            experienceWorth = 80;
            itemsOnDrop = ItemPool.itemsPool.FindAll(item => item.name.Equals("Splintmail helm") || item.name.Equals("Splintmail boots") ||
                                                                 item.name.Equals("Splintmail gloves") || item.name.Equals("Splintmail armor") ||
                                                                 item.name.Equals("Splintmail"));
        }

        public int smash(IHero hero)
        {
            Random damageDealt = new Random();
            mana -= smashAbility.mana;
            int smashDamageAfterArmor = smashAbility.damage * (100 - hero.ArmorWithEquipment()) / 100;
            return damageDealt.Next(smashDamageAfterArmor - 1, smashDamageAfterArmor + 1);
        }

        public int bash(IHero hero)
        {
            Random damageDealt = new Random();
            mana -= bashAbility.mana;
            int bashDamageAfterArmor = bashAbility.damage * (100 - hero.ArmorWithEquipment()) / 100;
            return damageDealt.Next(bashDamageAfterArmor - 4, bashDamageAfterArmor + 4);
        }
    }

    public class Shaman : EnemyClass
    {
        private static Ability lightningAbility = new Ability(45, 8);

        public Shaman() : base()
        {
            name = "Shaman";
            maxHealth = 140;
            maxMana = 32;
            health = 140;
            mana = 32;
            minDamage = 22;
            maxDamage = 25;
            armor = 0;
            fireResistance = 10;
            iceResistance = 10;
            lightningResistance = 20;
            experienceWorth = 70;
            itemsOnDrop = ItemPool.itemsPool.FindAll(item => item.name.Equals("Leather helm") || item.name.Equals("Leather boots") ||
                                                             item.name.Equals("Leather gloves") || item.name.Equals("Leather armor") ||
                                                             item.name.Equals("Dagger (right hand)"));
        }

        public int lightning(IHero hero)
        {
            Random damageDealt = new Random();
            mana -= lightningAbility.mana;
            int lightningDamageAfterArmor = lightningAbility.damage * (100 - hero.LightningResistanceWithEquipment()) / 100;
            return damageDealt.Next(lightningDamageAfterArmor - 8, lightningDamageAfterArmor + 8);
        }
    }

    public class Cyclops : EnemyClass
    {
        private static Ability boulderBashAbility = new Ability(50, 8);
        private static Ability quakeAbility = new Ability(60, 12);

        public Cyclops() : base()
        {
            name = "Cyclops";
            maxHealth = 600;
            maxMana = 32;
            health = 600;
            mana = 32;
            minDamage = 40;
            maxDamage = 45;
            armor = 30;
            fireResistance = 10;
            iceResistance = 10;
            lightningResistance = 20;
            experienceWorth = 250;
            itemsOnDrop = ItemPool.itemsPool.FindAll(item => item.name.Equals("Blessed shield") || item.name.Equals("Blessed inscribed staff") ||
                                                             item.name.Equals("Blessed dagger (right hand)"));
        }

        public int boulderbash(IHero hero)
        {
            Random damageDealt = new Random();
            mana -= boulderBashAbility.mana;
            int boulderBashDamageAfterArmor = boulderBashAbility.damage * (100 - hero.ArmorWithEquipment()) / 100;
            return damageDealt.Next(boulderBashDamageAfterArmor - 5, boulderBashDamageAfterArmor + 5);
        }

        public int quake(IHero hero)
        {
            Random damageDealt = new Random();
            mana -= quakeAbility.mana;
            int quakeDamageAfterArmor = quakeAbility.damage * (100 - hero.ArmorWithEquipment()) / 100;
            return damageDealt.Next(quakeDamageAfterArmor - 3, quakeDamageAfterArmor + 3);
        }
    }

    public class Bandit : EnemyClass
    {
        private static Ability backstabAbility = new Ability(45, 8);

        public Bandit() : base()
        {
            name = "Bandit";
            maxHealth = 200;
            maxMana = 16;
            health = 200;
            mana = 16;
            minDamage = 30;
            maxDamage = 35;
            armor = 10;
            fireResistance = 0;
            iceResistance = 0;
            lightningResistance = 0;
            experienceWorth = 100;
            itemsOnDrop = ItemPool.itemsPool.FindAll(item => item.name.Equals("Hardened helm") || item.name.Equals("Hardened boots") ||
                                                             item.name.Equals("Hardened gloves") || item.name.Equals("Hardened armor") ||
                                                             item.name.Equals("Iron dagger"));
        }

        public int backstab(IHero hero)
        {
            Random damageDealt = new Random();
            mana -= backstabAbility.mana;
            int backstabDamageAfterArmor = backstabAbility.damage * (100 - hero.ArmorWithEquipment()) / 100;
            return damageDealt.Next(backstabDamageAfterArmor - 5, backstabDamageAfterArmor + 5);
        }
    }

    public class Sorcerer : EnemyClass
    {
        private static Ability fireBlastAbility = new Ability(50, 8);

        public Sorcerer() : base()
        {
            name = "Sorcerer";
            maxHealth = 170;
            maxMana = 40;
            health = 170;
            mana = 40;
            minDamage = 30;
            maxDamage = 35;
            armor = 0;
            fireResistance = 30;
            iceResistance = 10;
            lightningResistance = 20;
            experienceWorth = 140;
            itemsOnDrop = ItemPool.itemsPool.FindAll(item => item.name.Equals("Sorecerer helm") || item.name.Equals("Sorecerer boots") ||
                                                             item.name.Equals("Sorecerer gloves") || item.name.Equals("Sorecerer armor") ||
                                                             item.name.Equals("Sorecerer staff"));
        }

        public int fireblast(IHero hero)
        {
            Random damageDealt = new Random();
            mana -= fireBlastAbility.mana;
            int fireblastDamageAfterArmor = fireBlastAbility.damage * (100 - hero.FireResistanceWithEquipment()) / 100;
            return damageDealt.Next(fireblastDamageAfterArmor - 2, fireblastDamageAfterArmor + 2);
        }
    }

    public class DesertedGuard : EnemyClass
    {
        private static Ability assaultAbility = new Ability(40, 7);

        public DesertedGuard() : base()
        {
            name = "Deserted Guard";
            maxHealth = 230;
            maxMana = 15;
            health = 230;
            mana = 15;
            minDamage = 32;
            maxDamage = 37;
            armor = 30;
            fireResistance = 10;
            iceResistance = 10;
            lightningResistance = 10;
            experienceWorth = 120;
            itemsOnDrop = ItemPool.itemsPool.FindAll(item => item.name.Equals("Guard helm") || item.name.Equals("Guard boots") ||
                                                             item.name.Equals("Guard gloves") || item.name.Equals("Guard armor") ||
                                                             item.name.Equals("Guard staff"));
        }

        public int assault(IHero hero)
        {
            Random damageDealt = new Random();
            mana -= assaultAbility.mana;
            int assaultDamageAfterArmor = assaultAbility.damage * (100 - hero.ArmorWithEquipment()) / 100;
            return damageDealt.Next(assaultDamageAfterArmor - 7, assaultDamageAfterArmor + 7);
        }
    }

    public class BanditLeader : EnemyClass
    {
        private static Ability backstabAbility = new Ability(50, 4);
        private static Ability flurryAbility = new Ability(60, 6);

        public BanditLeader() : base()
        {
            name = "Bandit Leader";
            maxHealth = 700;
            maxMana = 18;
            health = 700;
            mana = 15;
            minDamage = 32;
            maxDamage = 37;
            armor = 10;
            fireResistance = 10;
            iceResistance = 10;
            lightningResistance = 10;
            experienceWorth = 400;
            itemsOnDrop = ItemPool.itemsPool.FindAll(item => item.name.Equals("Guard helm") || item.name.Equals("Guard boots") ||
                                                             item.name.Equals("Guard gloves") || item.name.Equals("Guard armor") ||
                                                             item.name.Equals("Guard staff"));
        }

        public int backstab(IHero hero)
        {
            Random damageDealt = new Random();
            mana -= backstabAbility.mana;
            int backstabDamageAfterArmor = backstabAbility.damage * (100 - hero.ArmorWithEquipment()) / 100;
            return damageDealt.Next(backstabDamageAfterArmor - 8, backstabDamageAfterArmor + 8);
        }

        public int flurry(IHero hero)
        {
            Random damageDealt = new Random();
            mana -= flurryAbility.mana;
            int flurryDamageAfterArmor = flurryAbility.damage * (100 - hero.ArmorWithEquipment()) / 100;
            return damageDealt.Next(flurryDamageAfterArmor - 4, flurryDamageAfterArmor + 4);
        }
    }
}
