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
using System.IO;
using Musics;

namespace GameConsole
{
    class GameConsole
    {
        internal static Hero[] heroes = new Hero[4];
        internal static List<EnemyClass> enemies = new List<EnemyClass>();
        internal static bool[] heroesActedThisTurn = new bool[4];
        internal static int battlesWon = 0;
        internal static Dictionary<string, int> vanquishedEnemies = new Dictionary<string, int>();
        internal static bool started = false;
        internal static List<List<EnemyClass>> battles = new List<List<EnemyClass>>();
        internal static StringBuilder currentLog = new StringBuilder();
        internal static StringBuilder log = new StringBuilder();
        internal static Random random = new Random();
        internal static bool loadWasSuccessful = false;
        internal static Music backgroundMusic = new Music();

        internal static int maxLevel = 20;
        internal static List<int> levels = new List<int>();

        public static List<IHero> allHeroes = new List<IHero>()
        {
            new Rogue.RogueClass(),
            new Warrior.WarriorClass(),
            new Mage.MageClass(),
            new Priest.PriestClass()
        };

        static void Main()
        {
            Console.SetWindowSize((int)(Console.LargestWindowWidth * 0.8), (int)(Console.LargestWindowHeight * 0.8));
            InitializeLevels();
            InitializeBattles();
            backgroundMusic.path = "song.wav";
            Load();
            if (loadWasSuccessful && battlesWon < battles.Count)
            {
                started = true;
                Console.Clear();
                Commands.PrintStatus(heroes, enemies);
            }
            else
            {
                Commands.ExecuteCommand("help", heroes, enemies); //Start with the help command by default
                Console.WriteLine("No save found");
            }
            do
            {
                currentLog.Clear();
                Console.Write("Enter a command:");
                string command = Console.ReadLine();
                if (!started && (command.ToLower() != "start"))
                {
                    if(command.ToLower() == "classes")
                    {
                        for (int i = 0; i < allHeroes.Count; i++)
                        {
                            Commands.PrintInfo(allHeroes.ToArray(), allHeroes[i].name.ToLower());
                        }
                        Console.WriteLine(currentLog);
                        currentLog.Clear();
                        continue;
                    }
                    Console.WriteLine("Please start the game first");
                    continue;
                }
                Commands.ExecuteCommand(command, heroes, enemies); //Try to execute the command
                if (NoEnemiesAlive())
                {
                    battlesWon++;
                    InitializeEnemies();
                    for (int i = 0; i < heroesActedThisTurn.Length; i++)
                    {
                        heroesActedThisTurn[i] = false; // Once all enemies are dead we start a new turn in which the heroes have not acted
                    }
                    for (int i = 0; i < heroes.Length; i++)
                    {
                        heroes[i].health += (int)(heroes[i].maxHealth * 0.25f);
                        if (heroes[i].health > heroes[i].maxHealth)
                        {
                            heroes[i].health = (int)heroes[i].maxHealth;
                        }
                        if (heroes[i].mana > heroes[i].mana)
                        {
                            heroes[i].mana = (int)heroes[i].mana;
                        }
                    }
                }
                Console.Clear();
                if (AllHeroesHaveActed())
                {
                    AIAction();
                    bool noHeroesAlive = true;
                    for (int i = 0; i < heroesActedThisTurn.Length; i++)
                    {
                        if (heroes[i].health > 0) //Check if there is atleast 1 standing hero after the AI action
                        {
                            noHeroesAlive = false;
                        }
                        heroesActedThisTurn[i] = false; // After the AI's turn a new turn, in which the heroes have not acted, has come
                    }
                    if (noHeroesAlive)
                    {
                        currentLog.Append("You lost. Type in start if you want to restart the game");
                        Commands.PrintStatus(heroes, enemies);
                        Console.WriteLine(currentLog);
                        started = false;
                        log.Clear();
                        continue;
                    }
                }
                if (started)
                {
                    Commands.PrintStatus(heroes, enemies);
                }
                Console.WriteLine(currentLog);
                log.Append(currentLog);
                Save();
            }
            while (true);
        }

        public static void InitializeBattles()
        {
            Random random = new Random();
            var allPossibleEnemies = EnemyClass.listOfAllEnemies;
            battles = new List<List<EnemyClass>>() // We initialize the battles on random
            {
                RandomEnemies(allPossibleEnemies, 1, 0, 3), // We initialize a new enemy from all the possible enemies that have an index between 0 and 3 (exlusively)
                RandomEnemies(allPossibleEnemies, 2, 0, 3),
                RandomEnemies(allPossibleEnemies, 3, 0, 3),
                RandomEnemies(allPossibleEnemies, 4, 0, 3),
                new List<EnemyClass>() { new Centaur() },
                RandomEnemies(allPossibleEnemies, 1, 4, 7),
                RandomEnemies(allPossibleEnemies, 2, 4, 7),
                RandomEnemies(allPossibleEnemies, 3, 4, 7),
                RandomEnemies(allPossibleEnemies, 4, 4, 7),
                new List<EnemyClass>() { new Cyclops() },
                RandomEnemies(allPossibleEnemies, 1, 8, 11),
                RandomEnemies(allPossibleEnemies, 2, 8, 11),
                RandomEnemies(allPossibleEnemies, 3, 8, 11),
                RandomEnemies(allPossibleEnemies, 4, 8, 11),
                new List<EnemyClass>() { new BanditLeader() },
            };
            InitializeEnemies();
        }

        private static List<EnemyClass> RandomEnemies(List<EnemyClass> allPossibleEnemies, int numberOfEnemies, int startIndex, int endIndex)
        {
            var enemyList = new List<EnemyClass>();
            for (int i = 0; i < numberOfEnemies; i++)
            {
                int randomIndex = random.Next(startIndex, endIndex);
                Type enemyType = allPossibleEnemies[randomIndex].GetType();
                dynamic enemy = Activator.CreateInstance(enemyType);
                enemyList.Add(enemy);
            }
            return enemyList;
        }

        private static void InitializeLevels()
        {
            int experienceRequired = 100;
            for (int i = 0; i < maxLevel; i++)
            {
                levels.Add(experienceRequired);
                experienceRequired += 100 + 20 * (i + 1); //The levels with this formula are: 100, 220, 260, 320 etc.
            }
        }

        private static void InitializeEnemies()
        {
            if (battlesWon == battles.Count)
            {
                currentLog.Append("YOU WIN!!!!!!");
                started = false;
            }
            else
            {
                enemies = new List<EnemyClass>();
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
                if (!heroesActedThisTurn[i] && heroes[i].health > 0)
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
                float startingHealth = heroes[randomHeroIndex].health;
                IHero test = new IHero();
                heroes[randomHeroIndex].health = heroes[randomHeroIndex].health - //Reduce the hero's health with the damage from the ability
                    (int)abilities[randomAbilityIndex].Invoke(enemies[i], new object[] { heroes[randomHeroIndex] }); //Invoke the ability which will return an int value of the damage it deals
                if(heroes[randomHeroIndex].health < 0)
                {
                    heroes[randomHeroIndex].health = 0;
                }
                currentLog.Append(enemies[i].name + " hits " + heroes[randomHeroIndex].name + " for " + (startingHealth - heroes[randomHeroIndex].health) + " damage\n");
            }
        }

        public static void Save()
        {
            SaveHighScore();
            using (var stream = new StreamWriter("../../save.txt"))
            {
                try
                {
                    for (int i = 0; i < heroes.Length; i++) // Saves the heroes
                    {
                        stream.WriteLine(heroesActedThisTurn[i]);
                        stream.WriteLine(heroes[i].GetType().ToString());
                        FieldInfo[] fields = heroes[i].GetType().GetFields();

                        foreach (var field in fields)
                        {
                            var type = field.GetValue(heroes[i]).GetType();
                            if (type == typeof(ItemPool.Item)) // We need only the fields that are with a type of item
                            {
                                FieldInfo[] itemFields = type.GetFields(); // Gets all of fields of the current item slot
                                foreach (var itemField in itemFields)
                                {
                                    if (itemField.GetValue(field.GetValue(heroes[i])) == null)
                                    {
                                        stream.WriteLine("empty");
                                        break;
                                    }
                                    stream.WriteLine(itemField.GetValue(field.GetValue(heroes[i])).ToString());
                                    break; // We need only the name of the item which is always first
                                }
                            }
                            else if (type == typeof(IHero.Ability)) // We need only the fields that are with a type of item
                            {
                                FieldInfo[] itemFields = type.GetFields(); // Gets all of fields of the current item slot
                                foreach (var abilityField in itemFields)
                                {
                                    stream.WriteLine(abilityField.GetValue(field.GetValue(heroes[i])).ToString());
                                }
                            }
                            else
                            {
                                stream.WriteLine(field.GetValue(heroes[i]).ToString());
                            }
                        }
                    }
                    stream.WriteLine(enemies.Count);
                    for (int i = 0; i < enemies.Count; i++) // Saves the heroes
                    {
                        stream.WriteLine(enemies[i].GetType().ToString());
                        FieldInfo[] fields = enemies[i].GetType().GetFields();

                        foreach (var field in fields)
                        {
                            var type = field.GetValue(enemies[i]).GetType();
                            if (type != typeof(List<ItemPool.Item>) && type != typeof(EnemyClass.Ability))
                            {
                                stream.WriteLine(field.GetValue(enemies[i]).ToString());
                            }
                        }
                    }
                    stream.WriteLine(battlesWon);
                    stream.WriteLine(ItemPool.inventory.Count);
                    for (int i = 0; i < ItemPool.inventory.Count; i++)
                    {
                        stream.WriteLine(ItemPool.inventory[i].name);
                    }
                    foreach (var enemy in vanquishedEnemies)
                    {
                        stream.WriteLine(enemy.Key.ToString());
                        stream.WriteLine(enemy.Value.ToString());
                    }
                }
                finally
                {
                    stream.Close();
                }
            }
        }

        public static void SaveHighScore()
        {
            using (var outStream = new FileStream("../../highscore.txt", FileMode.Open, FileAccess.Write, FileShare.Read))
            {
                using (var inStream = new FileStream("../../highscore.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    var streamWriter = new StreamWriter(outStream);
                    var streamReader = new StreamReader(inStream);
                    try
                    {
                        string currentLine = streamReader.ReadLine();
                        if(currentLine != null)
                        {
                            int HighestBattlesWon = int.Parse(currentLine);
                            if (HighestBattlesWon < battlesWon)
                            {
                                SaveScore(streamWriter);
                            }
                        }
                        else
                        {
                            SaveScore(streamWriter);
                        }
                    }
                    catch(Exception)
                    {

                    }
                    finally
                    {
                        streamWriter.Close();
                        streamReader.Close();
                        outStream.Close();
                        inStream.Close();
                    }
                }
            }
        }

        private static void SaveScore(StreamWriter streamWriter)
        {
            streamWriter.WriteLine(battlesWon);
            foreach (var enemy in vanquishedEnemies)
            {
                streamWriter.WriteLine(enemy.Key + ": " + enemy.Value);
            }
        }

        public static void LoadHighScore()
        {
            using (var stream = new StreamReader("../../highscore.txt"))
            {
                string line = stream.ReadLine();
                currentLog.Append("battles won: " + line + "\n");
                while ((line = stream.ReadLine()) != null)
                {
                    currentLog.Append(line + "\n");
                }
            }
        }

        public static void Load()
        {
            using (var stream = new StreamReader("../../save.txt"))
            {
                try
                {
                    for (int i = 0; i < heroes.Length; i++) // Loads the heroes
                    {
                        heroesActedThisTurn[i] = bool.Parse(stream.ReadLine());
                        Type heroType = FindType(stream.ReadLine());
                        dynamic hero = Activator.CreateInstance(heroType);
                        heroes[i] = hero;
                        FieldInfo[] fields = heroes[i].GetType().GetFields();

                        foreach (var field in fields)
                        {
                            var type = field.GetValue(heroes[i]).GetType();
                            if (type == typeof(ItemPool.Item)) // We need only the fields that are with a type of item
                            {
                                field.SetValue(heroes[i], new ItemPool.Item());
                                string currentLine = stream.ReadLine();
                                if (currentLine.ToString() == "empty")
                                {
                                    continue;
                                }
                                ItemPool.inventory.Add(Commands.FindItemWithName(currentLine));
                                Commands.EquipItem(heroes, itemName: currentLine, currentHeroIndex: i);
                            }
                            else if (type == typeof(IHero.Ability)) // We need only the fields that are with a type of item
                            {
                                FieldInfo[] abilities = type.GetFields(); // Gets all of fields of the current item slot
                                foreach (var abilityField in abilities)
                                {
                                    abilityField.SetValue(field.GetValue(heroes[i]), int.Parse(stream.ReadLine()));
                                }
                            }
                            else
                            {
                                AddValueToHero(stream, i, field);
                            }
                        }
                    }
                    int enemiesLength = int.Parse(stream.ReadLine());
                    enemies = new List<EnemyClass>();
                    for (int i = 0; i < enemiesLength; i++) // Loads the heroes
                    {
                        string enemyTypeAsString = stream.ReadLine();
                        Type enemyType = FindType(enemyTypeAsString);
                        dynamic enemy = Activator.CreateInstance(enemyType);
                        enemies.Add(enemy);
                        FieldInfo[] fields = enemies[i].GetType().GetFields();

                        foreach (var field in fields)
                        {
                            var type = field.GetValue(enemies[i]).GetType();
                            if(type != typeof(List<ItemPool.Item>) && type != typeof(EnemyClass.Ability))
                            {
                                AddValueToEnemy(stream, i, field);
                            }
                        }
                    }
                    battlesWon = int.Parse(stream.ReadLine());
                    int inventoryItemsCount = int.Parse(stream.ReadLine());
                    for (int i = 0; i < inventoryItemsCount; i++)
                    {
                        ItemPool.Item currentItem = Commands.FindItemWithName(stream.ReadLine());
                        ItemPool.inventory.Add(currentItem);
                    }
                    string line;
                    while ((line = stream.ReadLine()) != null)
                    {
                        vanquishedEnemies.Add(line, int.Parse(stream.ReadLine()));
                    }
                    loadWasSuccessful = true;
                }
                catch (Exception)
                {
                    return;
                }
                finally
                {
                    stream.Close();
                }
            }
        }

        private static void AddValueToHero(StreamReader stream, int i, FieldInfo field)
        {
            int valueAsInt;
            float valueAsFloat;
            string value = stream.ReadLine();
            if (int.TryParse(value, out valueAsInt))
            {
                field.SetValue(heroes[i], int.Parse(value));
            }
            else if (float.TryParse(value, out valueAsFloat))
            {
                field.SetValue(heroes[i], float.Parse(value));
            }
            else
            {
                field.SetValue(heroes[i], value);
            }
        }

        private static void AddValueToEnemy(StreamReader stream, int i, FieldInfo field)
        {
            int valueAsInt;
            float valueAsFloat;
            string value = stream.ReadLine();
            if (int.TryParse(value, out valueAsInt))
            {
                field.SetValue(enemies[i], int.Parse(value));
            }
            else if (float.TryParse(value, out valueAsFloat))
            {
                field.SetValue(enemies[i], float.Parse(value));
            }
            else
            {
                field.SetValue(enemies[i], value);
            }
        }

        private static void AddValueToItem(ItemPool.Item currentItem, StreamReader stream, int i, FieldInfo field)
        {
            int valueAsInt;
            float valueAsFloat;
            string value = stream.ReadLine();
            if (int.TryParse(value, out valueAsInt))
            {
                field.SetValue(currentItem, int.Parse(value));
            }
            else if (float.TryParse(value, out valueAsFloat))
            {
                field.SetValue(currentItem, float.Parse(value));
            }
            else if (field.GetValue(currentItem).GetType() == typeof(ItemPool.Slot))
            {
                ItemPool.Slot slot;
                if (Enum.TryParse(value, true, out slot))
                {
                    field.SetValue(currentItem, slot);
                }
            }
            else
            {
                field.SetValue(currentItem, value);
            }
        }

        private static Type FindType(string fullName)
        {
            return
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.IsDynamic)
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t.FullName.Equals(fullName));
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
                        PrintClasses();
                        break;
                    }
                case "status":
                    {
                        PrintStatus(GameConsole.vanquishedEnemies);
                        break;
                    }
                case "log":
                    {
                        GameConsole.currentLog.Append(GameConsole.log);
                        break;
                    }
                case "highscore":
                    {
                        GameConsole.LoadHighScore();
                        break;
                    }
                case "music":
                    {
                        if(commandParts[1] == "on")
                        {
                            GameConsole.backgroundMusic.startSong();
                        }
                        else if(commandParts[1] == "off")
                        {
                            GameConsole.backgroundMusic.stopSong();
                        }
                        break;
                    }
                default: // since the code for all heroes is the same we can just put it in default and let the try and catch pick up unregonised commands
                    {
                        try
                        {
                            if(commandParts[0] == "all" && commandParts[1] == "attack")
                            {
                                for (int i = 0; i < heroes.Length; i++)
                                {
                                    string attackCommand = heroes[i].name + " attack " + commandParts[2];
                                    ExecuteCommand(attackCommand, heroes, enemies);
                                }
                                break;
                            }
                            if (commandParts[1].Equals("info") && commandParts.Length == 2) //If the second part is info and the command has 2 parts
                            {
                                PrintInfo(heroes, commandParts[0]);
                                break;
                            }
                            else if (commandParts[1].Equals("equip"))
                            {
                                EquipItem(heroes, commandParts); // Check if the command is an ability command and uses it
                                break;
                            }
                            else if (commandParts[1].Equals("unequip"))
                            {
                                UnEquipItem(heroes, commandParts); // Check if the command is an ability command and uses it
                                break;
                            }
                            else
                            {
                                TryToUseAbility(heroes, enemies, commandParts); // Check if the command is an ability command and uses it
                                break;
                            }
                        }
                        catch(Exception)
                        {
                            UnrecognizedCommand();
                        }
                        break;
                    }
            }
        }

        private static void PrintClasses()
        {
            for (int i = 0; i < GameConsole.allHeroes.Count; i++)
            {
                PrintInfo(GameConsole.allHeroes.ToArray(), GameConsole.allHeroes[i].name.ToLower());
            }
        }

        public static void EquipItem(Hero[] heroes, string[] commandParts = null, string itemName = "", int currentHeroIndex = 0)
        {
            try
            {
                if (commandParts != null)
                {
                    currentHeroIndex = GameConsole.GetHeroIndex(commandParts[0]); //Get the current hero index
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
                                            GameConsole.currentLog.Append("Item has been equipped: ");
                                            PrintItem(item);
                                            ItemPool.inventory.Remove(item);
                                            return;
                                        }
                                    }
                                    GameConsole.currentLog.Append("Hero cannot equip item \n");
                                    return;
                                }
                            }
                        }
                    }
                }
                GameConsole.currentLog.Append("Item not present in inventory \n");
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
                                GameConsole.currentLog.Append("Item has been unequipped: ");
                                PrintItem((ItemPool.Item)field.GetValue(heroes[currentHeroIndex]));
                                ItemPool.inventory.Add((ItemPool.Item)field.GetValue(heroes[currentHeroIndex]));
                                field.SetValue(heroes[currentHeroIndex], new ItemPool.Item());
                                return;
                            }
                            break;
                        }
                    }
                }
                GameConsole.currentLog.Append("Item not equipped on hero \n");
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
                if(GameConsole.heroes[currentHeroIndex].health <= 0)
                {
                    GameConsole.currentLog.Append("Hero is dead. Revive him first \n");
                    return;
                }
                if (GameConsole.heroesActedThisTurn[currentHeroIndex]) // Check if the hero has acted this turn
                {
                    GameConsole.currentLog.Append("Hero has already acted this turn \n");
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
                    int startingHealth = enemies[enemyIndex].health;
                    enemies[enemyIndex].health = (enemies[enemyIndex].health - (int)ability.Invoke(heroes[currentHeroIndex], new object[] { enemies[enemyIndex] }));
                    GameConsole.currentLog.Append("You hit " + enemies[enemyIndex].name + " for " + (startingHealth - enemies[enemyIndex].health) + " damage\n");
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
                        GameConsole.currentLog.Append("You received: ");
                        PrintItem(ItemPool.inventory[ItemPool.inventory.Count - 1]);
                        AddExperience(heroes, enemies[enemyIndex].experienceWorth);
                        enemies.RemoveAt(enemyIndex);
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
                        float startingHealth = heroes[heroTargetedIndex].health;
                        heroes[heroTargetedIndex].health = (heroes[heroTargetedIndex].health + (int)ability.Invoke(heroes[currentHeroIndex], null));
                        if (heroes[heroTargetedIndex].health > heroes[heroTargetedIndex].maxHealth)
                        {
                            heroes[heroTargetedIndex].health = heroes[heroTargetedIndex].maxHealth;
                        }
                        GameConsole.currentLog.Append("You healed " + heroes[heroTargetedIndex].name + " for " + (startingHealth - enemies[enemyIndex].health) + " health\n");
                    }
                }
                GameConsole.heroesActedThisTurn[currentHeroIndex] = true; // If everything passed without an exception then the move was succesful and the current hero has acted
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
                GameConsole.currentLog.Append("No mana for that ability \n");
                hero.mana = currentMana;
                return false;
            }
            else if (hero.mana < -100)// If after being invoked the hero's level is not enough for the ability he loses 1000 mana temporarily
            {
                GameConsole.currentLog.Append("Need a higher level to use that ability \n");
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
                if (heroes[i].level <= GameConsole.maxLevel && heroes[i].health > 0) //If the hero's experience is less than the max level
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
            for (int i = 0; i < GameConsole.heroes.Length; i++)
            {
                GameConsole.heroes[i] = null;
            }
            Console.WriteLine("Please select your first hero by typing in the class and name separated by spaces (e.g Rogue Pesho)");
            AddHero(0);
            Console.WriteLine("Please select your second hero");
            AddHero(1);
            Console.WriteLine("Please select your third hero");
            AddHero(2);
            Console.WriteLine("Please select your fourth hero");
            AddHero(3);
            GameConsole.enemies.Clear();
            GameConsole.battlesWon = 0;
            for (int i = 0; i < GameConsole.heroesActedThisTurn.Length; i++)
            {
                GameConsole.heroesActedThisTurn[i] = false;
            }
            GameConsole.InitializeBattles();
            GameConsole.started = true;
            GameConsole.currentLog.Clear();
            ItemPool.inventory.Clear();
        }

        private static void AddHero(int index)
        {
            do
            {
                try
                {                
                    string[] command = Console.ReadLine().Split(' ');
                    if(command[0] == "classes")
                    {
                        PrintClasses();
                    }
                    if (command.Length != 2)
                    {
                        UnrecognizedCommand();
                        Console.WriteLine(GameConsole.currentLog);
                        GameConsole.currentLog.Clear();
                        continue;
                    }
                    if (NameExists(command[1]))
                    {
                        Console.WriteLine("You already have a character with that name");
                        Console.WriteLine(GameConsole.currentLog);
                        GameConsole.currentLog.Clear();
                        continue;
                    }
                    command[0] = command[0].ToLower();
                    command[0] = ((char)(command[0][0] - 32)) + command[0].Substring(1) + "Class";
                    int classIndex;
                    if(ClassExists(command[0], out classIndex))
                    {
                        Type heroType = GameConsole.allHeroes[classIndex].GetType();
                        dynamic hero = Activator.CreateInstance(heroType);
                        GameConsole.heroes[index] = hero;
                        GameConsole.heroes[index].name = command[1];
                        break;
                    }
                    if(GameConsole.heroes[index] == null)
                    {
                        UnrecognizedCommand();
                        Console.WriteLine(GameConsole.currentLog);
                        GameConsole.currentLog.Clear();
                    }
                }
                catch (Exception)
                {
                    UnrecognizedCommand();
                    Console.WriteLine(GameConsole.currentLog);
                    GameConsole.currentLog.Clear();
                }
            }
            while (true);
        }

        private static bool NameExists(string name)
        {
            for (int i = 0; i < GameConsole.heroes.Length; i++)
            {
                if (GameConsole.heroes[i] != null)
                {
                    if (GameConsole.heroes[i].name.ToLower().Equals(name.ToLower()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool ClassExists(string heroClass, out int index)
        {
            index = -1;
            for (int i = 0; i < GameConsole.allHeroes.Count; i++)
            {
                if (GameConsole.allHeroes[i].GetType().Name.ToLower().Equals(heroClass.ToLower()))
                {
                    index = i;
                    return true;
                }
            }
            return false;
        }

        private static void PrintHelp()
        {
            GameConsole.currentLog.Append(@"The game's objective is to defeat as many monsters as possible before you die. After every battle
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
- All attack Number (Would use everybody's basic attack on the enemy whose number from left to right is equal to Number)
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
- Log (Gives you a log of everything you have done from the start)
- Highscore (Gives you the most won battles)
- Music On (starts playing background music)
- Music Off (stops playing the background music)
");
            if(!GameConsole.started)
            {
                Console.WriteLine(GameConsole.currentLog);
            }
        }

        private static void PrintStatus(Dictionary<string, int> vanquishedEnemies)
        {
            foreach (var vanquishedEnemy in vanquishedEnemies)
            {
                GameConsole.currentLog.Append(vanquishedEnemy.Key + ": " + vanquishedEnemy.Value + "\n");
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
                GameConsole.currentLog.Append("Inventory is empty. \n");
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
                    GameConsole.currentLog.Append("empty \n");
                    return;
                }
                var type = field.GetValue(item).GetType();
                if (type == typeof(ItemPool.ClassAbleToWearItem[])) //If the type of the current field is an array of classes able to wear an item we need to print them separately
                {
                    //Prints the classes able to wear the item
                    GameConsole.currentLog.Append("For: ");
                    object array = field.GetValue(item);
                    IEnumerable enumerable = array as IEnumerable;
                    if (enumerable != null)
                    {
                        foreach (object element in enumerable)
                        {
                            GameConsole.currentLog.Append(element.ToString() + " ");
                        }
                    }
                    GameConsole.currentLog.Append("| ");
                }
                else
                {
                    // Print item field info
                    if (isFirstField)
                    {
                        GameConsole.currentLog.Append("| " + field.GetValue(item).ToString() + " |");
                        isFirstField = false;
                    }
                    else if (field.GetValue(item).ToString() != "0")
                    {
                        GameConsole.currentLog.Append(field.Name + ": " + field.GetValue(item).ToString() + " | ");
                    }
                }
            }
            GameConsole.currentLog.Append("\n");
        }

        public static void PrintInfo(IHero[] heroes, string character)
        {
            int currentHeroIndex = -1;
            for (int i = 0; i < heroes.Length; i++)
            {
                if(character.ToLower().Equals(heroes[i].name.ToLower()))
                {
                    currentHeroIndex = i;
                    break;
                }
            }
            Type type = heroes[currentHeroIndex].GetType(); // Get the class of the current hero
            FieldInfo[] properties = type.GetFields(BindingFlags.Public | BindingFlags.Instance); // Get all of the fields of the current hero
            GameConsole.currentLog.Append("\n");
            foreach (FieldInfo property in properties)
            {
                Type propertyType = property.GetValue(heroes[currentHeroIndex]).GetType();
                if (propertyType == typeof(ItemPool.Item)) // If the current field is an item we need to print it diffrently
                {
                    ItemPool.Item item = FindItem(property.GetValue(heroes[currentHeroIndex]), propertyType);
                    GameConsole.currentLog.Append("\t" + property.Name + ": ");
                    PrintItem(item);
                }
                else
                {
                    GameConsole.currentLog.Append("\t" + property.Name + ": " + property.GetValue(heroes[currentHeroIndex]).ToString() + "\n");
                }
            }
            var abilities = type.GetFields(BindingFlags.Public | BindingFlags.Static);
            GameConsole.currentLog.Append("\tAbilities: \n");
            foreach (var ability in abilities)
            {
                Type abilityType = ability.GetValue(abilities).GetType();
                var abilityInfos = abilityType.GetFields();
                GameConsole.currentLog.Append("\t\t|" + ability.Name + " | ");
                foreach (var abilityInfo in abilityInfos)
                {
                    GameConsole.currentLog.Append(abilityInfo.Name + ": " + abilityInfo.GetValue(ability.GetValue(abilities)).ToString() + " | ");
                }
                GameConsole.currentLog.Append("\n");
            }
            GameConsole.currentLog.Append("\n");
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
                string heroClass = Regex.Match(heroes[i].GetType().Name, @"^[A-Za-z](.*?(?=[A-Z]))").Value; //Match the part of the class name that is before a CapitalLetter and Starts with a capital letter
                heroesResult.Add(Character(new string[] {heroClass + ": " + heroes[i].name,
                                           "Health: " + heroes[i].health + "/" + heroes[i].maxHealth,
                                           "Mana: " + heroes[i].mana + "/" + heroes[i].maxMana,
                                           "Acted: " + GameConsole.heroesActedThisTurn[i],
                                           "Experience: " + heroes[i].experience + "/" + GameConsole.levels[heroes[i].level],
                                           "Level: " + heroes[i].level}));
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                enemiesResult.Add(Character(new string[] { enemies[i].name,
                                            "Health: " + enemies[i].health + "/" + enemies[i].maxHealth,
                                            "Mana: " + enemies[i].mana + "/" + enemies[i].maxMana }));
            }
            PrintResult(heroesResult);
            Console.WriteLine();
            PrintResult(enemiesResult);
            Console.WriteLine();
        }

        private static void PrintResult(List<string[,]> result)
        {
            for (int i = 0; i < result[0].Length; i++)
            {
                for (int count = 0; count < result.Count; count++)
                {
                    result[count][i, 0] = StringFormatted(result[count][i, 0], 20);
                    Console.Write("|{0}|", result[count][i, 0]);
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

        static string[,] Character(string[] attributes)
        {
            string[,] staffMatrix = new string[attributes.Length, 1];
            for (int row = 0; row < staffMatrix.GetLength(0); row++)
            {
                staffMatrix[row, 0] = attributes[row];
            }
            return staffMatrix;
        }

        private static void UnrecognizedCommand()
        {
            GameConsole.currentLog.Append("Unrecognized command\n");
        }

        internal static ItemPool.Item FindItemWithName(string name)
        {
            ItemPool.Item currentItem = new ItemPool.Item();
            for (int i = 0; i < ItemPool.itemsPool.Count; i++)
            {
                if(ItemPool.itemsPool[i].name.Equals(name))
                {
                    currentItem = ItemPool.itemsPool[i];
                }
            }
            return currentItem;
        }
    }
}
