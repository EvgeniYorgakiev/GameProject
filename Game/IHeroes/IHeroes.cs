using System.Collections.Generic;

namespace IHeroes
{
    public class IHero
    {
        public struct Ability
        {
            public int damage;
            public int mana;
            public int levelRequired;

            public Ability(int damage, int mana, int levelRequired)
            {
                this.damage = damage;
                this.mana = mana;
                this.levelRequired = levelRequired;
            }
        }

        public string name;
        public int level = 1;
        public int experience = 0;
        public float health;
        public float maxHealth;
        public float mana;
        public float maxMana;
        public float minDamage;
        public float maxDamage;
        public int armor;
        public int fireResistance;
        public int iceResistance;
        public int lightningResistance;
        public float maxHealthOnLevelUp;
        public float maxManaOnLevelUp;
        public float minDamageOnLevelUp;
        public float maxDamageOnLevelUp;
        public string headSlot = "empty";
        public string bootsSlot = "empty";
        public string armorSlot = "empty";
        public string glovesSlot = "empty";
    }

    public class ItemPool
    {
        public enum Slot
        {
            Head,
            Boots,
            Armor,
            Gloves,
            LeftHand,
            RightHand
        }

        public enum ClassAbleToWearItem
        {
            Rogue,
            Mage,
            Warrior,
            Priest
        }

        public struct Item
        {
            public string name;
            public Slot slot;
            public List<ClassAbleToWearItem> classesAbleToWearItem;
            public int armor;
            public int fireResistance;
            public int iceResistance;
            public int lightingResistance;
            public int minDamage;
            public int maxDamage;

            public Item(string name, Slot slot, List<ClassAbleToWearItem> classesAbleToWearItem, int armor, int fireResistance = 0, 
                        int iceResistance = 0, int lightingResistance = 0, int minDamage = 0, int maxDamage = 0)
            {
                this.name = name;
                this.slot = slot;
                this.classesAbleToWearItem = classesAbleToWearItem;
                this.armor = armor;
                this.fireResistance = fireResistance;
                this.iceResistance = iceResistance;
                this.lightingResistance = lightingResistance;
                this.minDamage = minDamage;
                this.maxDamage = maxDamage;
            }
        }

        public static List<Item> itemsPool = new List<Item>()
        {
            new Item("Leather helm", Slot.Head, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Rogue }, 2),
            new Item("Leather gloves", Slot.Gloves, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Rogue }, 1),
            new Item("Leather boots", Slot.Boots, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Rogue }, 1),
            new Item("Leather armor", Slot.Armor, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Rogue }, 5),
            new Item("Dagger (right hand)", Slot.RightHand, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Rogue }, 0, minDamage: 3, maxDamage: 3),
            new Item("Studded helm", Slot.Head, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Rogue }, 3),
            new Item("Studded gloves", Slot.Gloves, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Rogue }, 2),
            new Item("Studded boots", Slot.Boots, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Rogue }, 2),
            new Item("Studded armor", Slot.Armor, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Rogue }, 8),
            new Item("Dagger (left hand)", Slot.LeftHand, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Rogue }, 0, minDamage: 2, maxDamage: 2),
            new Item("Cloth helm", Slot.Head, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Priest, ClassAbleToWearItem.Mage}, 1),
            new Item("Cloth gloves", Slot.Gloves, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Priest, ClassAbleToWearItem.Mage}, 1),
            new Item("Cloth boots", Slot.Boots, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Priest, ClassAbleToWearItem.Mage}, 1),
            new Item("Cloth armor", Slot.Armor, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Priest, ClassAbleToWearItem.Mage}, 3),
            new Item("Staff", Slot.RightHand, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Priest, ClassAbleToWearItem.Mage }, 0, minDamage: 2, maxDamage: 2),
            new Item("Magister helm", Slot.Head, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Priest, ClassAbleToWearItem.Mage}, 2),
            new Item("Magister gloves", Slot.Gloves, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Priest, ClassAbleToWearItem.Mage}, 2),
            new Item("Magister boots", Slot.Boots, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Priest, ClassAbleToWearItem.Mage}, 2),
            new Item("Magister armor", Slot.Armor, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Priest, ClassAbleToWearItem.Mage}, 5),
            new Item("Inscribed staff", Slot.RightHand, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Priest, ClassAbleToWearItem.Mage }, 0, 3, 3, 3, 3, 3),
            new Item("Chainmail helm", Slot.Head, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Warrior }, 3),
            new Item("Chainmail gloves", Slot.Gloves, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Warrior }, 1),
            new Item("Chainmail boots", Slot.Boots, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Warrior }, 2),
            new Item("Chainmail armor", Slot.Armor, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Warrior }, 7),
            new Item("Longsword", Slot.RightHand, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Warrior }, 0, minDamage: 3, maxDamage: 3),
            new Item("Splintmail helm", Slot.Head, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Warrior }, 5),
            new Item("Splintmail gloves", Slot.Gloves, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Warrior }, 2),
            new Item("Splintmail boots", Slot.Boots, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Warrior }, 3),
            new Item("Splintmail armor", Slot.Armor, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Warrior }, 10),
            new Item("Shield", Slot.RightHand, new List<ClassAbleToWearItem> { ClassAbleToWearItem.Warrior }, 5),
        };

        public static List<Item> inventory = new List<Item>();
    }
}
