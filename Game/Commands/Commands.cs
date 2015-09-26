using System;

namespace Commands
{
    public class Commands
    {
        public static void ExecuteCommand(string command)
        {
            switch (command)
            {
                case "start":
                    {
                        StartGame();
                        break;
                    }
            }
        }

        private static void StartGame()
        {

        }
    }

}
