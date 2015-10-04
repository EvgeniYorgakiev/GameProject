using System;
using System.Collections.Generic;

namespace GameConsole
{
    class GameConsole
    {
        static void Main()
        {
            List<string[,]> result = new List<string[,]>();
            result.Add(Character("Rogue", 100, 100));
            result.Add(Character("Rogue", 100, 100));
            result.Add(Character("Rogue", 100, 100));
            Console.Write("{0}", string.Join(" | ",result));
        }

        static string[,] Character(string name, int health, int mana)
        {
            string[] arrPar = { 
                name,
                health.ToString(),
                mana.ToString()
            };
            string[,] staffMatrix = new string[3, 1];
            for (int row = 0; row < staffMatrix.GetLength(0); row++)
            {
                staffMatrix[row, 0] = arrPar[row];
            }
            return staffMatrix;
           
        }
    }
}
