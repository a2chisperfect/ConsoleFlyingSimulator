using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
    static class Interface
    {
        static public void DrawGameInterface(int speed, int height, int range ,string mission, List<Dispatcher> Dispatchers)
        {
            Console.Clear();
            Console.ResetColor();
            Console.SetCursorPosition(24, 22);
            Console.Write("distance = {0}", range);
            Console.SetCursorPosition(24, 23);
            Console.Write("speed = {0} km/h", speed);
            Console.SetCursorPosition(24 , 24);
            Console.Write("height = {0} m", height);
            Console.SetCursorPosition(Values.windowWidth / 2 - mission.Length / 2, 0);
            Console.Write("Goal : {0}", mission);
            Console.SetCursorPosition(0, 3);
            if (Dispatchers.Count == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Disconnected ...");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
            else
            {
                foreach (var item in Dispatchers)
                {
                    Console.WriteLine();
                    Console.WriteLine(item.name);
                    Console.WriteLine("Recommend height: {0}", item.recomendedHight);
                    Console.WriteLine("Penalty points: {0}", item.score);
                    Console.WriteLine("Message {0}", item.message);
                    Console.WriteLine();
                }
            }
           
        }
        
        static public void DrawMenu(Button[] mainMenu, int buttonNumber)
        {
            foreach (var item in mainMenu)
            {
                item.Show();
            }
            mainMenu[buttonNumber].Selected();
        }
        static public void  DrawControlKeys()
        {
            Console.ResetColor();
            Console.Clear();
            Console.SetCursorPosition(8, 3);
            Console.WriteLine("Increase Speed");
            Console.SetCursorPosition(8, 5);
            Console.WriteLine("Reduce Speed");
            Console.SetCursorPosition(8, 7);
            Console.WriteLine("Increase Height");
            Console.SetCursorPosition(8, 9);
            Console.WriteLine("Reduce Height");
            Console.SetCursorPosition(33, 3);
            Console.WriteLine("Right Arrow / (+Shift)");
            Console.SetCursorPosition(33, 5);
            Console.WriteLine("Left Arrow / (+Shift)");
            Console.SetCursorPosition(33, 7);
            Console.WriteLine("UP Arrow / (+Shift)");
            Console.SetCursorPosition(33, 9);
            Console.WriteLine("Down Arrow / (+Shift)");

            Button back = new Button(Values.windowWidth / 2 - 6, 15, "Back");
            back.Selected();
        }
        static public void  DrawExcResult(string message ,string innerMessage)
        {
            Console.ResetColor();
            Console.Clear();
            Console.SetCursorPosition(Values.windowWidth / 2 - message.Length / 2, 9);
            Console.Write(message);
            Console.SetCursorPosition(Values.windowWidth / 2 - innerMessage.Length / 2, 12);
            Console.Write(innerMessage);
            Button back = new Button(Values.windowWidth / 2 - 6, 15, "Back to main");
            back.Selected();
        }

        static public void DrawScores(List<int>Scores)
        {
            Console.ResetColor();
            Console.Clear();
            int pos = 1;
            Console.SetCursorPosition(Values.windowWidth / 2 - 6, 1);
            Console.WriteLine("WORST RESULTS");
            foreach (var item in Scores.OrderByDescending(item => item))
            {
               
                Console.SetCursorPosition(Values.windowWidth / 2 - 3, pos+2);
                Console.WriteLine("{0}. {1}",pos,item);
                pos++;
            }
            Button back = new Button(Values.windowWidth / 2 - 6, 18, "Back to main");
            back.Selected();
        }

        static public void DrawWin(int points)
        {
            Console.ResetColor();
            Console.Clear();
            Console.SetCursorPosition(Values.windowWidth / 2 - 5, 9);
            Console.Write("You did it");
            Console.SetCursorPosition(Values.windowWidth / 2 - 12, 10);
            Console.Write("Total penalty points = {0}",points);
            Button back = new Button(Values.windowWidth / 2 - 6, 15, "Back to main");
            back.Selected();
        }
    }
}
