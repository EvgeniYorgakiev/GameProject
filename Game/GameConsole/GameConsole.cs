using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using IHeroes;
using Heroes;
using Enemy;
using System.Collections;
using System.Text.RegularExpressions;
using System.Text;

namespace GameConsole
{
    class GameConsole
    {
        internal static Hero[] heroes = new Hero[4];
        internal static List<EnemyClass> enemies = new List<EnemyClass>();
        internal static bool[] heroesActedThisTurn = new bool[4];
        internal static int battlesWon = 0;
        internal static Dictionary<string, int> vanquishedEnemies = new Dictionary<string, int>();

        internal static List<List<EnemyClass>> battles = new List<List<EnemyClass>>();

        internal static int maxLevel = 20;
        internal static List<int> levels = new List<int>();

        static void Main()
        {
            Console.SetWindowSize((int)(Console.LargestWindowWidth * 0.8), (int)(Console.LargestWindowHeight * 0.8));
            InitializeLevels();
            Commands.ExecuteCommand("help", heroes, enemies); //Start with the help command by default
            Commands.ExecuteCommand("start", heroes, enemies); //Start with the start command by default
            InitializeBattles();
            do
            {
                Commands.PrintStatus(heroes, enemies);
                Console.Write("Enter a command:");
                string command = Console.ReadLine();
                Commands.ExecuteCommand(command, heroes, enemies); //Try to execute the command
                if (NoEnemiesAlive())
                {
                    battlesWon++;
                    InitializeEnemies();
                    for (int i = 0; i < heroesActedThisTurn.Length; i++)
                    {
                        heroesActedThisTurn[i] = false; // Once all enemies are dead we start a new turn in which the heroes have not acted
                    }
                }
                if (AllHeroesHaveActed())
                {
                    AIAction();
                    bool noHeroesAlive = true;
                    for (int i = 0; i < heroesActedThisTurn.Length; i++)
                    {
                        Console.WriteLine(heroes[i].health);
                        if (heroes[i].health > 0) //Check if there is atleast 1 standing hero after the AI action
                        {
                            noHeroesAlive = false;
                        }
                        heroesActedThisTurn[i] = false; // After the AI's turn a new turn, in which the heroes have not acted, has come
                    }
                    if (noHeroesAlive)
                    {
                        Commands.ExecuteCommand("start", heroes, enemies);
                    }
                }
            }
            while (true);
        }

        private static void InitializeBattles()
        {
            Random random = new Random();
            var allPossibleEnemies = EnemyClass.listOfAllEnemies;
            battles = new List<List<EnemyClass>>() // We initialize the battles on random
            {
                new List<EnemyClass>() { allPossibleEnemies[random.Next(0, 3)] }, // We initialize a new enemy from all the possible enemies that have an index between 0 and 3 (exlusively)
                new List<EnemyClass>() { allPossibleEnemies[random.Next(0, 3)], allPossibleEnemies[random.Next(0, 3)], },
                new List<EnemyClass>() { allPossibleEnemies[random.Next(0, 3)], allPossibleEnemies[random.Next(0, 3)], allPossibleEnemies[random.Next(0, 3)], },
                new List<EnemyClass>() { allPossibleEnemies[random.Next(0, 3)], allPossibleEnemies[random.Next(0, 3)], allPossibleEnemies[random.Next(0, 3)], allPossibleEnemies[random.Next(0, 3)], },
            };
            InitializeEnemies();
        }

        private static void InitializeLevels()
        {
            int experienceRequired = 100;
            for (int i = 0; i < maxLevel; i++)
            {
                levels.Add(experienceRequired);
                experienceRequired += 100 + 2 * (i + 1); //The levels with this formula are: 100, 220, 260, 320 etc.
            }
        }

        private static void InitializeEnemies()
        {
            if (battlesWon == battles.Count)
            {
                Console.WriteLine("YOU WIN!!!!!!");
            }
            else
            {
                enemies = battles[battlesWon];
            }
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
                if (enemies[i].health > 0)
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
                if (!heroesActedThisTurn[i])
                {
                    return false;
                }
            }
            return true;
        }

        private static void AIAction()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                Random random = new Random();
                int randomHeroIndex;
                do
                {
                    randomHeroIndex = random.Next(0, heroes.Length);
                }
                while (heroes[randomHeroIndex].health <= 0); //Keep choosing a new random hero if the random hero we are attacking is dead
                var methodNames = enemies[i].GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance |
                    BindingFlags.DeclaredOnly).Select(x => x.Name).Distinct().OrderBy(x => x); // We take all of the methods the current enemy has that have not been inherited remove duplicates and order them
                List<MethodInfo> abilities = new List<MethodInfo>(); //The abilities the enemy has
                abilities.Add(enemies[i].GetType().GetMethod("attack")); //All enemies have attack by default
                foreach (var methodName in methodNames)
                {
                    abilities.Add(enemies[i].GetType().GetMethod(methodName.ToLower())); //Add the method to the ability list
                    int currentMana = enemies[i].mana;
                    abilities[abilities.Count - 1].Invoke(enemies[i], new object[] { heroes[randomHeroIndex] }); //Try to use the ability by also draining mana
                    if (enemies[i].mana < 0) //If after the enemy has used the ability the current enemy has less than 0 mana then he doesn't have mana for that ability
                    {
                        abilities.RemoveAt(abilities.Count - 1); //We remove the ability from the list
                    }
                    enemies[i].mana = currentMana;
                }
                int randomAbilityIndex = random.Next(0, abilities.Count); //Use a random ability from the list
                heroes[randomHeroIndex].health = heroes[randomHeroIndex].health - //Reduce the hero's health with the damage from the ability
                    (int)abilities[randomAbilityIndex].Invoke(enemies[i], new object[] { heroes[randomHeroIndex] }); //Invoke the ability which will return an int value of the damage it deals
            }
        }
    }

    public class Commands
    {
        public static void ExecuteCommand(string command, Hero[] heroes, List<EnemyClass> enemies)
        {
            string[] commandParts = command.Split(' ');
            switch (commandParts[0].ToLower()) //Check if the first word of the command matches any of the basics
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
                case "inventory":
                    {
                        PrintInventory();
                        break;
                    }
                case "classes":
                    {
                        for (int i = 0; i < heroes.Length; i++)
                        {
                            PrintInfo(heroes[i].name.ToLower());
                        }
                        break;
                    }
                case "status":
                    {
                        PrintStatus(GameConsole.vanquishedEnemies);
                        break;
                    }
                default: // since the code for all heroes is the same we can just put it in default and let the try and catch pick up unregonised commands
                    {
                        if (commandParts[1].Equals("info") && commandParts.Length == 2) //If the second part is info and the command has 2 parts
                        {
                            PrintInfo(commandParts[0]);
                        }
                        else if (commandParts[1].Equals("equip"))
                        {
                            EquipItem(heroes, commandParts); // Check if the command is an ability command and uses it
                        }
                        else if (commandParts[1].Equals("unequip"))
                        {
                            UnEquipItem(heroes, commandParts); // Check if the command is an ability command and uses it
                        }
                        else
                        {
                            TryToUseAbility(heroes, enemies, commandParts); // Check if the command is an ability command and uses it
                        }
                        break;
                    }
            }
        }

        private static void EquipItem(Hero[] heroes, string[] commandParts)
        {
            try
            {
                int currentHeroIndex = GameConsole.GetHeroIndex(commandParts[0]); //Get the current hero index
                string itemName = "";
                for (int i = 2; i < commandParts.Length; i++) // Since the item name may contain more than 1 word we need to join all the words and make a name of them
                {
                    if (i == commandParts.Length - 1)
                    {
                        itemName += commandParts[i];
                    }
                    else
                    {
                        itemName += commandParts[i] + " ";
                    }
                }
                foreach (var item in ItemPool.inventory) // Go through the whole inventory to look for the item
                {
                    if (item.name.ToLower().Equals(itemName.ToLower()))
                    {
                        FieldInfo[] fields = heroes[currentHeroIndex].GetType().GetFields(); // Gets all of fields of the current hero
                        foreach (var field in fields)
                        {
                            var type = field.GetValue(heroes[currentHeroIndex]).GetType();
                            if (type == typeof(ItemPool.Item)) // We need only the fields that are with a type of item
                            {
                                string slotName = Regex.Match(field.Name, @"^.*?(?=[A-Z])").Value; //Match the part of the field name that is before a CapitalLetter
                                if (slotName.ToLower().Equals(item.slot.ToString().ToLower()))
                                {
                                    foreach (var classAbleToWearItem in item.classesAbleToWearItem) // Go through all the classes able to wear the item
                                    {
                                        string className = Regex.Match(heroes[currentHeroIndex].GetType().Name, @"^[A-Za-z](.*?(?=[A-Z]))").Value; //Match the part of the class name that is before a CapitalLetter and Starts with a capital letter
                                        if (classAbleToWearItem.ToString().ToLower().Equals(className.ToLower())) //Check if the current class being checked is equal to the hero class name
                                        {
                                            field.SetValue(heroes[currentHeroIndex], item);
                                            Console.Write("Item has been equipped: ");
                                            PrintItem(item);
                                            ItemPool.inventory.Remove(item);
                                            return;
                                        }
                                    }
                                    Console.WriteLine("Hero cannot equip item");
                                    return;
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("Item not present in inventory");
            }
            catch (Exception) // Every unregonised commands is basically a different exception
            {
                UnrecognizedCommand();
            }
        }

        private static void UnEquipItem(Hero[] heroes, string[] commandParts)
        {
            try
            {
                int currentHeroIndex = GameConsole.GetHeroIndex(commandParts[0]); //Get the current hero index
                string itemName = "";
                for (int i = 2; i < commandParts.Length; i++) // Since the item name may contain more than 1 word we need to join all the words and make a name of them
                {
                    if (i == commandParts.Length - 1)
                    {
                        itemName += commandParts[i];
                    }
                    else
                    {
                        itemName += commandParts[i] + " ";
                    }
                }
                FieldInfo[] fields = heroes[currentHeroIndex].GetType().GetFields(); // Gets all of fields of the current hero
                foreach (var field in fields)
                {
                    var type = field.GetValue(heroes[currentHeroIndex]).GetType();
                    if (type == typeof(ItemPool.Item)) // We need only the fields that are with a type of item
                    {
                        FieldInfo[] itemFields = type.GetFields(); // Gets all of fields of the current item slot
                        foreach (var itemField in itemFields)
                        {
                            if (itemField.GetValue(field.GetValue(heroes[currentHeroIndex])) == null)
                            {
                                break;
                            }
                            if (itemField.GetValue(field.GetValue(heroes[currentHeroIndex])).ToString().ToLower().Equals(itemName.ToLower()))
                            {
                                Console.Write("Item has been unequipped: ");
                                PrintItem((ItemPool.Item)field.GetValue(heroes[currentHeroIndex]));
                                ItemPool.inventory.Add((ItemPool.Item)field.GetValue(heroes[currentHeroIndex]));
                                field.SetValue(heroes[currentHeroIndex], new ItemPool.Item());
                                return;
                            }
                            break;
                        }
                    }
                }
                Console.WriteLine("Item not equipped on hero");
            }
            catch (Exception) // Every unregonised commands is basically a different exception
            {
                UnrecognizedCommand();
            }
        }

        private static void TryToUseAbility(Hero[] heroes, List<EnemyClass> enemies, string[] commandParts)
        {
            try
            {
                int currentHeroIndex = GameConsole.GetHeroIndex(commandParts[0]); //Get the current hero index
                if (GameConsole.heroesActedThisTurn[currentHeroIndex]) // Check if the hero has acted this turn
                {
                    Console.WriteLine("Hero has already acted this turn");
                    return;
                }
                if (commandParts[1].ToLower().Equals("skip") && commandParts.Length == 2)
                {
                    GameConsole.heroesActedThisTurn[currentHeroIndex] = true;
                    return;
                }
                MethodInfo ability = heroes[currentHeroIndex].GetType().GetMethod(commandParts[1].ToLower(),
                                     BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy); //By passing a string of the name of the method we can get it as a variable
                int enemyIndex;
                if (int.TryParse(commandParts[2], out enemyIndex)) //Try to parse the third string as a number if it is a number we use an ability on an enemy
                {
                    enemyIndex--; // Because the left most enemy is not with index 0 instead of 1 in the array 
                    if (!HeroHasMana(heroes[currentHeroIndex], ability, enemies[enemyIndex]))
                    {
                        return;
                    }
                    enemies[enemyIndex].health = (enemies[enemyIndex].health - (int)ability.Invoke(heroes[currentHeroIndex], new object[] { enemies[enemyIndex] }));
                    if (enemies[enemyIndex].health < 0)
                    {
                        if (GameConsole.vanquishedEnemies.ContainsKey(enemies[enemyIndex].GetType().Name))
                        {
                            GameConsole.vanquishedEnemies[enemies[enemyIndex].GetType().Name]++;
                        }
                        else
                        {
                            GameConsole.vanquishedEnemies.Add(enemies[enemyIndex].GetType().Name, 1);
                        }
                        enemies[enemyIndex].OnDeath();
                        AddExperience(heroes, enemies[enemyIndex].experienceWorth);
                    }
                }
                else // Else we use an ability on an ally
                {
                    int heroTargetedIndex = GameConsole.GetHeroIndex(commandParts[2]);
                    if (!HeroHasMana(heroes[currentHeroIndex], ability))
                    {
                        return;
                    }
                    if (ability.Name.ToLower().Equals("revive")) // We need to check if the ability is revive since it is a void function and is invoked in a diffrent way
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
                GameConsole.heroesActedThisTurn[currentHeroIndex] = true; // If everything passed without an exception then the move was succesful and the current hero has acted
                Console.WriteLine(enemies[enemyIndex].health);
            }
            catch (Exception) // Every unregonised commands is basically a different exception
            {
                UnrecognizedCommand();
            }
        }

        private static bool HeroHasMana(Hero hero, MethodInfo ability, EnemyClass enemy = null)
        {
            int currentMana = (int)hero.mana; //Check if the hero has mana for the ability
            if (enemy == null) //If there is no enemy the ability is a void
            {
                ability.Invoke(hero, null);
            }
            else
            {
                ability.Invoke(hero, new object[] { enemy });
            }
            if (hero.mana < 0 && hero.mana >= -100) // If after being invoked the hero's mana is between 0 and -100 he doesn't have the mana for the ability
            {
                Console.WriteLine("No mana for that ability");
                hero.mana = currentMana;
                return false;
            }
            else if (hero.mana < -100)// If after being invoked the hero's level is not enough for the ability he loses 1000 mana temporarily
            {
                Console.WriteLine("Need a higher level to use that ability");
                hero.mana = currentMana;
                return false;
            }
            hero.mana = currentMana;
            return true;
        }

        private static void AddExperience(Hero[] heroes, int experienceGained)
        {
            for (int i = 0; i < heroes.Length; i++) // Go through each hero and add the experience to all of them not just one
            {
                if (heroes[i].level <= GameConsole.maxLevel) //If the hero's experience is less than the max level
                {
                    heroes[i].experience += experienceGained;
                    if (heroes[i].experience > GameConsole.levels[heroes[i].level]) // If the hero's experience is more than the current level required
                    {
                        heroes[i].OnLevelUp();
                    }
                    if (heroes[i].level == GameConsole.maxLevel)
                    {
                        heroes[i].experience = GameConsole.levels[heroes[i].level - 1];
                    }
                }
            }
        }

        private static void StartGame()
        {
            GameConsole.heroes[0] = new Rogue.RogueClass();
            GameConsole.heroes[1] = new Warrior.WarriorClass();
            GameConsole.heroes[2] = new Mage.MageClass();
            GameConsole.heroes[3] = new Priest.PriestClass();
            GameConsole.enemies.Clear();
            GameConsole.battlesWon = 0;
            for (int i = 0; i < GameConsole.heroesActedThisTurn.Length; i++)
            {
                GameConsole.heroesActedThisTurn[i] = false;
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
- Inventory (shows a list of all of the items in the inventory)
- ClassName equip ItemName (equips the item with that name on the hero and replaces any items on that slot so long as it is in the inventory)
- ClassName unequip ItemName (unequips the item with that name from the hero)
- Status (gives you the number of vanquished enemies ,their names and battles fought e.g:
Boars: 2 
Skeletons: 3 
BigBadBoss1: 1
Total number of random enemies vanquished 5
Total number of battles: 5)
");
        }

        private static void PrintStatus(Dictionary<string, int> vanquishedEnemies)
        {
            foreach (var vanquishedEnemy in vanquishedEnemies)
            {
                Console.WriteLine("{0}: {1}", vanquishedEnemy.Key, vanquishedEnemy.Value);
            }
        }

        private static void PrintInventory()
        {
            if (ItemPool.inventory.Count > 0)
            {
                foreach (var item in ItemPool.inventory)
                {
                    PrintItem(item);
                }
            }
            else
            {
                Console.WriteLine("Inventory is empty.");
            }
        }

        private static void PrintItem(ItemPool.Item item)
        {
            FieldInfo[] fields = item.GetType().GetFields(); // Gets all of fields of the current item
            bool isFirstField = true;
            foreach (var field in fields)
            {
                if (field.GetValue(item) == null)
                {
                    Console.WriteLine("empty");
                    return;
                }
                var type = field.GetValue(item).GetType();
                if (type == typeof(ItemPool.ClassAbleToWearItem[])) //If the type of the current field is an array of classes able to wear an item we need to print them separately
                {
                    //Prints the classes able to wear the item
                    Console.Write("For: ");
                    object array = field.GetValue(item);
                    IEnumerable enumerable = array as IEnumerable;
                    if (enumerable != null)
                    {
                        foreach (object element in enumerable)
                        {
                            Console.Write("{0} ", element.ToString());
                        }
                    }
                    Console.Write("| ");
                }
                else
                {
                    // Print item field info
                    if (isFirstField)
                    {
                        Console.Write("| {0} | ", field.GetValue(item).ToString());
                        isFirstField = false;
                    }
                    else if (field.GetValue(item).ToString() != "0")
                    {
                        Console.Write("{0}: {1} | ", field.Name, field.GetValue(item).ToString());
                    }
                }
            }
            Console.WriteLine();
        }

        private static void PrintInfo(string character)
        {
            int currentHeroIndex = GameConsole.GetHeroIndex(character);
            Type type = GameConsole.heroes[currentHeroIndex].GetType(); // Get the class of the current hero
            FieldInfo[] properties = type.GetFields(BindingFlags.Public | BindingFlags.Instance); // Get all of the fields of the current hero
            Console.WriteLine();
            foreach (FieldInfo property in properties)
            {
                Type propertyType = property.GetValue(GameConsole.heroes[currentHeroIndex]).GetType();
                if (propertyType == typeof(ItemPool.Item)) // If the current field is an item we need to print it diffrently
                {
                    ItemPool.Item item = FindItem(property.GetValue(GameConsole.heroes[currentHeroIndex]), propertyType);
                    Console.Write("\t{0}: ", property.Name);
                    PrintItem(item);
                }
                else
                {
                    Console.WriteLine("\t{0}: {1}", property.Name, property.GetValue(GameConsole.heroes[currentHeroIndex]).ToString());
                }
            }
            var abilities = type.GetFields(BindingFlags.Public | BindingFlags.Static);
            Console.WriteLine("\tAbilities:");
            foreach (var ability in abilities)
            {
                Type abilityType = ability.GetValue(abilities).GetType();
                var abilityInfos = abilityType.GetFields();
                Console.Write("\t\t|{0} | ", ability.Name);
                foreach (var abilityInfo in abilityInfos)
                {
                    Console.Write("{0}: {1} | ", abilityInfo.Name, abilityInfo.GetValue(ability.GetValue(abilities)).ToString());
                }
                Console.WriteLine();
            }
            //var abilities = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Select(x => x.Name).Distinct().OrderBy(x => x);
            //Console.WriteLine("\tAbilities: ");
            //Console.WriteLine("\t\tattack");
            //foreach (var methodName in abilities)
            //{
            //    Console.WriteLine("\t\t{0}", methodName);
            //}
            Console.WriteLine();
        }

        private static ItemPool.Item FindItem(object property, Type propertyType)
        {
            FieldInfo[] itemProperties = propertyType.GetFields();
            foreach (var item in ItemPool.itemsPool) //Check all the items to find our item
            {
                foreach (var itemProperty in itemProperties)
                {
                    if (itemProperty.GetValue(property) == null)
                    {
                        return new ItemPool.Item(); //item slot is empty
                    }
                    if (itemProperty.GetValue(property).ToString().Equals(item.name))
                    {
                        return item;
                    }
                    break;
                }
            }
            return new ItemPool.Item();
        }

        public static void PrintStatus(Hero[] heroes, List<EnemyClass> enemies)
        {
            List<string[,]> heroesResult = new List<string[,]>();
            List<string[,]> enemiesResult = new List<string[,]>();
            for (int i = 0; i < heroes.Length; i++)
            {
                heroesResult.Add(Character(heroes[i].name, heroes[i].health + "/" + heroes[i].maxHealth, heroes[i].mana + "/" + heroes[i].maxMana));
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                enemiesResult.Add(Character(enemies[i].name, enemies[i].health + "/" + enemies[i].maxHealth, enemies[i].mana + "/" + enemies[i].maxMana));
            }
            PrintResult(heroesResult);
            Console.WriteLine();
            PrintResult(enemiesResult);
        }

        private static void PrintResult(List<string[,]> result)
        {
            for (int i = 0; i < result[0].Length; i++)
            {
                for (int count = 0; count < result.Count; count++)
                {
                    result[i][i, 0] = StringFormatted(result[i][i, 0], 11);
                    Console.Write("|{0}|", result[i][i, 0]);

                }
                Console.WriteLine();
            }
        }

        static string StringFormatted(string stringToBeFormatted, int length)
        {
            StringBuilder stringFormatted = new StringBuilder();

            if (stringToBeFormatted.Length <= length)
            {
                stringFormatted.Append(stringToBeFormatted.Substring(0, stringToBeFormatted.Length));
            }
            else
            {
                stringFormatted.Append(stringToBeFormatted.Substring(0, length));
            }
            for (int i = 0; i < length - stringToBeFormatted.Length; i++)
            {

                if (i % 2 == 0)
                {
                    stringFormatted.Insert(0, " ");
                }
                else
                {
                    stringFormatted.Insert(stringFormatted.Length, " ");
                }
            }
            return stringFormatted.ToString();
        }

        static string[,] Character(string name, string health, string mana)
        {
            string[] arrPar = {
                name,
                health.ToString(),
                mana.ToString()
            };
            string[,] staffMatrix = new string[arrPar.Length, 1];
            for (int row = 0; row < staffMatrix.GetLength(0); row++)
            {
                staffMatrix[row, 0] = arrPar[row];
            }
            return staffMatrix;
        }

        private static void UnrecognizedCommand()
        {
            Console.WriteLine("Unrecognized command");
        }
    }
}
