using System;

namespace Enemy
{
    public class Enemy
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
        public float difficulty;

        public int attack()
        {
            Random damageDealt = new Random();
            return damageDealt.Next(minDamage, maxDamage);
        }
    }
}
