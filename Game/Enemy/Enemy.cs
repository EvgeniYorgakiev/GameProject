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

        public static string name;
        public static int health;
        public static int maxHealth;
        public static int mana;
        public static int maxMana;
        public static int minDamage;
        public static int maxDamage;

        public void SetHealth(int newHealth)
        {
            health = newHealth;
        }

        public void SetMana(int newMana)
        {
            mana = newMana;
        }

        public static int Attack()
        {
            Random damageDealt = new Random();
            return damageDealt.Next(minDamage, maxDamage);
        }
    }
}
