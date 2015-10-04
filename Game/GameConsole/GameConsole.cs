using System;

namespace GameConsole
{
    class GameConsole
    {
        static void Main()
        {
            PrintCharacter("Rogue", 100, 100);
                            
        }

        private static void PrintCharacter(string name, int health, int mana)
        {
            string[] arrPar = { 
                name,
                health.ToString(),
                mana.ToString()
            };
            int count = 0;
            string[,] matrixPrint = new string[3, 4];
            for (int row = 0; row < matrixPrint.GetLength(0); row++)
            {
                for (int col = 0; col < matrixPrint.GetLength(1); col++)
                {
                    if (row == count)
                    {
                        matrixPrint[row, col] = arrPar[count];
                        count++;
                    }
                }
            }
            for (int row = 0; row < matrixPrint.GetLength(0); row++)
            {
                for (int col = 0; col < matrixPrint.GetLength(1); col++)
                {
                    Console.Write("|{0}\t", matrixPrint[row, col]);
                }
                Console.WriteLine();
            }
           
        }
    }
}
