using System;

namespace Heroes
{
    public class Hero : IHeroes.IHero
    {
        public int attack(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            int minDamageAfterArmor = (int) MinDamageWithEquipment() * (100 - enemy.armor)/100;
            int maxDamageAfterArmor = (int) MaxDamageWithEquipment() * (100 - enemy.armor) / 100;
            return damageDealt.Next(minDamageAfterArmor, maxDamageAfterArmor);
        }

        public void OnLevelUp()
        {
            level++;
            baseMaxDamage += maxDamageOnLevelUp;
            maxHealth += maxHealthOnLevelUp;
            maxMana += maxManaOnLevelUp;
            baseMinDamage += minDamageOnLevelUp;
        }
    }
}
