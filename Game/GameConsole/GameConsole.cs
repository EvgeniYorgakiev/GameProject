using System;
using System.Collections.Generic;
using System.Text;

namespace GameConsole
{
    class GameConsole
    {
        static void Main()
        {
            List<string[,]> result = new List<string[,]>();
            result.Add(Character("Rogue", "100/100", "100/100"));
            result.Add(Character("Rogue", "100/100", "100/100"));
            result.Add(Character("Rogue", "100/100", "100/100"));
            result.Add(Character("Rogue", "100/100", "100/100"));
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
                    stringFormatted.Insert(stringFormatted.Length , " ");
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
    }
}
