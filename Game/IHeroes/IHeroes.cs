using System;

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
    }
}
