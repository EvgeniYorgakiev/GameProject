using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using Heroes;
using Enemy;

namespace Test
{
    class Test
    {
        internal static Hero[] heroes = new Hero[4];
        internal static List<EnemyClass> enemies = new List<EnemyClass>();
        internal static bool[] heroesActedThisTurn = new bool[4];
        internal static int battlesWon = 0;

        internal static List<List<EnemyClass>> battles = new List<List<EnemyClass>>()
        {
            new List<EnemyClass> {new Boar() },
            new List<EnemyClass> {new Skeleton() },
            new List<EnemyClass> {new SkeletonMage(), new Boar()},
            new List<EnemyClass> {new Skeleton(), new SkeletonMage()},
        };

        internal static int maxLevel = 20;
        internal static List<int> levels = new List<int>();

        static void Main()
        {
            InitializeLevels();
            Commands.ExecuteCommand("help", heroes, enemies);
            Commands.ExecuteCommand("start", heroes, enemies);
            InitializeEnemies();
            do
            {
                Console.Write("Enter a command:");
                string command = Console.ReadLine();
                Commands.ExecuteCommand(command, heroes, enemies);
                if (NoEnemiesAlive())
                {
                    battlesWon++;
                    InitializeEnemies();
                    for (int i = 0; i < heroesActedThisTurn.Length; i++)
                    {
                        heroesActedThisTurn[i] = false;
                    }
                }
                if (AllHeroesHaveActed())
                {
                    AIAction();
                    bool noHeroesAlive = true;
                    for (int i = 0; i < heroesActedThisTurn.Length; i++)
                    {
                        Console.WriteLine(heroes[i].health);
                        if(heroes[i].health > 0)
                        {
                            noHeroesAlive = false;
                        }
                        heroesActedThisTurn[i] = false;
                    }
                    if(noHeroesAlive)
                    {
                        Commands.ExecuteCommand("start", heroes, enemies);
                    }
                }
            }
            while (true);
        }

        private static void InitializeLevels()
        {
            int experienceRequired = 100;
            for (int i = 0; i < maxLevel; i++)
            {
                levels.Add(experienceRequired);
                experienceRequired += 100 + 2 * (i + 1);
            }
        }

        private static void InitializeEnemies()
        {
            enemies = battles[battlesWon];
        }

        public static int GetHeroIndex(string name)
        {
            for (int i = 0; i < heroes.Length; i++)
            {
                if (heroes[i].name.ToLower() == name.ToLower())
                {
                    return i;
                }
            }
            return -1;
        }

        private static bool NoEnemiesAlive()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i].health > 0)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool AllHeroesHaveActed()
        {
            for (int i = 0; i < heroesActedThisTurn.Length; i++)
            {
                if(!heroesActedThisTurn[i])
                {
                    return false;
                }
            }
            return true;
        }

        private static void AIAction()
        {
            for (int i = 0; i < enemies.Count ; i++)
            {
                Random random = new Random();
                int randomHeroIndex;
                do
                {
                    randomHeroIndex = random.Next(0, heroes.Length);
                }
                while (heroes[randomHeroIndex].health < 0);
                var methodNames = enemies[i].GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Select(x => x.Name).Distinct().OrderBy(x => x);
                List<MethodInfo> abilities = new List<MethodInfo>();
                abilities.Add(enemies[i].GetType().GetMethod("attack"));
                foreach (var methodName in methodNames)
                {
                    abilities.Add(enemies[i].GetType().GetMethod(methodName.ToLower()));
                    int currentMana = enemies[i].mana;
                    abilities[abilities.Count - 1].Invoke(enemies[i], new object[] { heroes[randomHeroIndex] });
                    if(enemies[i].mana < 0)
                    {
                        abilities.RemoveAt(abilities.Count - 1);
                        enemies[i].mana = currentMana;
                    }
                }
                int randomAbilityIndex = random.Next(0, abilities.Count);
                heroes[randomHeroIndex].health = heroes[randomHeroIndex].health - (int)abilities[randomAbilityIndex].Invoke(enemies[i], new object[] { heroes[randomHeroIndex] });
            }
        }
    }

    public class Commands
    {
        public static void ExecuteCommand(string command, Hero[] heroes, List<EnemyClass> enemies)
        {
            string[] commandParts = command.Split(' ');
            switch (commandParts[0].ToLower())
            {
                case "start":
                    {
                        StartGame();
                        break;
                    }
                case "help":
                    {
                        PrintHelp();
                        break;
                    }
                default: // since the code for all heroes is the same we can just put it in default and let the try and catch pick up unregonised commands
                    {
                        if(commandParts[1].Equals("info"))
                        {
                            PrintInfo(commandParts[0]);
                            break;
                        }
                        TryToUseAbility(heroes, enemies, commandParts);
                        break;
                    }
            }
        }

        private static void TryToUseAbility(Hero[] heroes, List<EnemyClass> enemies, string[] commandParts)
        {
            try
            {
                int currentHeroIndex = Test.GetHeroIndex(commandParts[0]); //Get the current hero index
                if (Test.heroesActedThisTurn[currentHeroIndex])
                {
                    Console.WriteLine("Hero has already acted this turn");
                    return;
                }
                if(commandParts[1].ToLower().Equals("skip"))
                {
                    Test.heroesActedThisTurn[currentHeroIndex] = true;
                    return;
                }
                MethodInfo ability = heroes[currentHeroIndex].GetType().GetMethod(commandParts[1].ToLower(), 
                                                                                  BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy ); //By passing a string of the name of the method we can get it as a variable
                int enemyIndex;
                if (int.TryParse(commandParts[2], out enemyIndex)) //Try to parse the third string as a number if it is a number we use an ability on an enemy
                {
                    enemyIndex--; // Because the left most enemy is not with index 0 instead of 1 in the array 
                    if(!HeroHasMana(heroes[currentHeroIndex], ability, enemies[enemyIndex]))
                    {
                        return;
                    }
                    enemies[enemyIndex].health = (enemies[enemyIndex].health - (int)ability.Invoke(heroes[currentHeroIndex], new object[] { enemies[enemyIndex] } ));
                    if(enemies[enemyIndex].health < 0)
                    {
                        enemies[enemyIndex].OnDeath();
                        AddExperience(heroes, enemies[enemyIndex].experienceWorth);
                    }
                }
                else // Else we use an ability on an ally
                {
                    int heroTargetedIndex = Test.GetHeroIndex(commandParts[2]);
                    if (!HeroHasMana(heroes[currentHeroIndex], ability))
                    {
                        return;
                    }
                    if (ability.Name.ToLower().Equals("revive"))
                    {
                        ability.Invoke(heroes[currentHeroIndex], new object[] { heroes[heroTargetedIndex] });
                    }
                    else
                    {
                        heroes[heroTargetedIndex].health = (heroes[heroTargetedIndex].health + (int)ability.Invoke(heroes[currentHeroIndex], null));
                        if (heroes[heroTargetedIndex].health > heroes[heroTargetedIndex].maxHealth)
                        {
                            heroes[heroTargetedIndex].health = heroes[heroTargetedIndex].maxHealth;
                        }
                    }
                }
                Test.heroesActedThisTurn[currentHeroIndex] = true;
                Console.WriteLine(enemies[enemyIndex].health);
            }
            catch (Exception) // Every unregonised commands is basically a diffrent exception
            {
                UnrecognizedCommand();
            }
        }

        private static void AddExperience(Hero[] heroes, int experienceGained)
        {
            for (int i = 0; i < heroes.Length; i++)
            {
                if(heroes[i].experience < Test.levels[heroes[i].level - 1])
                {
                    heroes[i].experience += experienceGained;
                    if (heroes[i].experience > Test.levels[heroes[i].level])
                    {
                        heroes[i].OnLevelUp();
                    }
                    if (heroes[i].level == Test.maxLevel)
                    {
                        heroes[i].experience = Test.levels[heroes[i].level - 1];
                    }
                }
            }
        }

        private static void StartGame()
        {
            Test.heroes[0] = new Rogue.RogueClass();
            Test.heroes[1] = new Warrior.WarriorClass();
            Test.heroes[2] = new Mage.MageClass();
            Test.heroes[3] = new Priest.PriestClass();
            Test.enemies.Clear();
            Test.battlesWon = 0;
            for (int i = 0; i < Test.heroesActedThisTurn.Length; i++)
            {
                Test.heroesActedThisTurn[i] = false;
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine(@"The game's objective is to defeat as many monsters as possible before you die. After every battle
your health and mana regenerate. The game is turn based and every turn you have to give each of your heroes a
command after which the all of the enemies perform an action .The game is played by typing in commands in the console.
You do not need to type in the '-'. 

The commands are as follows:
- help  (Brings you a list of all commands)
- start ((re)starts the game)
- Classes (for a list of all classes)
- ClassName info (Gives you a list of the all the abilities the class can use. For example you can type
Rogue info
to get the names of the rogue's abilities)
- ClassName AbilityName Number (For example typing in 
Warrior shieldbash 1
Would use the Warrior's ability shieldbash on the first enemy from left to right)
- ClassName AbilityName ClassName(For example typing in 
Priest heal warrior
Would use the Priest's ability heal on the warrior)
- ClassName skip (Skips the turn of that class)
- Status (gives you the number of vanquished enemies ,their names and battles fought e.g:
Boars: 2 
Skeletons: 3 
BigBadBoss1: killed
Total number of random enemies vanquished 5
Total number of battles: 5)
");
        }

        private static void PrintInfo(string character)
        {
            throw new NotImplementedException();
        }

        private static void UnrecognizedCommand()
        {
            Console.WriteLine("Unrecognized command");
        }

        private static bool HeroHasMana(Hero hero, MethodInfo ability, EnemyClass enemy = null)
        {
            int currentMana = (int) hero.mana; //Check if the hero has mana for the ability
            if(enemy == null)
            {
                ability.Invoke(hero, null);
            }
            else
            {
                ability.Invoke(hero, new object[] { enemy });
            }
            if (hero.mana < 0 && hero.mana >= -100)
            {
                Console.WriteLine("No mana for that ability");
                hero.mana = currentMana;
                return false;
            }
            else if(hero.mana < - 100)
            {
                Console.WriteLine("Need a higher level to use that ability");
                hero.mana = currentMana;
                return false;
            }
            hero.mana = currentMana;
            return true;
        }
    }
}
