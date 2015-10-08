using System;

namespace Heroes
{
    public class Hero : HeroesAttributes.HeroAttributes
    {
        Random damageDealt = new Random();
        public int attack(Enemy.EnemyClass enemy)
        {
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
