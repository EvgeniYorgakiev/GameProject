using System;

namespace GameConsole
{
    class GameConsole
    {
        static void Main()
        {
           
            string[,] matrix = new string[1, 4];
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    PrintCharacter("Rogue", 100, 100);
                }
            }
        }

        private static void PrintCharacter(string name, int health, int mana)
        {
            string[] arrPar = { 
                name,
                health.ToString(),
                mana.ToString()
            };
            string[,] matrixPrint = new string[3, 1];
            for (int col = 0; col < matrixPrint.GetLength(1); col++)
            {
                for (int row = 0; row < matrixPrint.GetLength(0); row++)
                {
                    matrixPrint[row, col] = arrPar[row];
                }
            }
            for (int row = 0; row < matrixPrint.GetLength(0); row++)
            {
                for (int col = 0; col < matrixPrint.GetLength(1); col++)
                {
                    Console.Write("|{0}|", matrixPrint[row, col]);
                }
            }
            Console.WriteLine();
        }
    }
}
