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
            string[,] matrixPrint = new string[1, 3];
            for (int col = 0; col < matrixPrint.GetLength(1); col++)
            {
                matrixPrint[0, col] = arrPar[col];
            }
            string[,] matrix = new string[3, 4];
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                for (int row = 0; row < matrix.GetLength(1); row++)
                {
                    matrix[row, col] = matrixPrint[0, row];
                }
            }
            for (int row = 0; row < matrix.GetLength(0); row++)
			{
			        for (int col= 0; col < matrix.GetLength(1); col++)
			        {
			            Console.WriteLine("|{0}|", matrix[row,col]);
			        }
			}
            
                    
        }
    }
}
