using System;

namespace Heroes
{
    public class Hero : IHeroes.IHero
    {
        public int attack(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            int minDamageAfterArmor = (int) minDamage * (100 - enemy.armor)/100;
            int maxDamageAfterArmor = (int) maxDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(minDamageAfterArmor, maxDamageAfterArmor);
        }

        public void OnLevelUp()
        {
            level++;
            maxDamage += maxDamageOnLevelUp;
            maxHealth += maxHealth;
            maxMana += maxMana;
            minDamage += minDamageOnLevelUp;
        }
    }
}
