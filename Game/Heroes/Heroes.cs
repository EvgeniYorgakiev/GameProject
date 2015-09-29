using System;

namespace Heroes
{
    public class Hero : IHeroes.IHero
    {
        public int attack(Enemy.EnemyClass enemy)
        {
            Random damageDealt = new Random();
            int minDamageAfterArmor = minDamage * (100 - enemy.armor)/100;
            int maxDamageAfterArmor = maxDamage * (100 - enemy.armor) / 100;
            return damageDealt.Next(minDamageAfterArmor, maxDamageAfterArmor);
        }
    }
}
