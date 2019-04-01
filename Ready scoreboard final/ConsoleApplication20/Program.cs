using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleApplication20
{
    class Program
    {
        static List<int> Score = new List<int>();

        static void Save()
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream("Scores.dat", FileMode.Create, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, Score);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (fs != null)
                 fs.Close();
            }

        }
        static void Load()
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream("Scores.dat", FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                Score = (List<int>)bf.Deserialize(fs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        static void Scoreboard()
        {
            Interface.DrawScores(Score);
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Escape)
            {
                return;
            }
        }
        static void ControlKeys()
        {
            Interface.DrawControlKeys();
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Escape)
            {
                return;
            }
        }
        static void Main(string[] args)
        {
            Console.Title = "AirPlane_Simulator";
            Console.CursorVisible = false;
            Console.SetWindowSize(Values.windowWidth, Values.windowHeight);
            Console.SetBufferSize(Values.windowWidth, Values.windowHeight);
            Console.ResetColor();
            Console.Clear();
            Load();

            Button[] mainMenu = new Button[4];
            mainMenu[0] = new Button(Values.windowWidth / 2 - 4, 5, "New Game");
            mainMenu[1] = new Button(Values.windowWidth / 2 - 6, 10, "Control Keys");
            mainMenu[2] = new Button(Values.windowWidth / 2 - 5, 15, "Scoreboard");
            mainMenu[3] = new Button(Values.windowWidth / 2 - 2, 20, "Exit");
            int buttonNumber = 0;
            while (true)
            {
                Console.Clear();
                Interface.DrawMenu(mainMenu, buttonNumber);
                ConsoleKeyInfo key = Console.ReadKey();
                mainMenu[buttonNumber].Show();
                if (key.Key == ConsoleKey.UpArrow)
                    buttonNumber--;
                if (key.Key == ConsoleKey.DownArrow)
                    buttonNumber++;
                if (buttonNumber < 0)
                    buttonNumber = 3;
                if (buttonNumber > 3)
                    buttonNumber = 0;
                mainMenu[buttonNumber].Selected();
                if (key.Key == ConsoleKey.Enter)
                {
                    switch (buttonNumber)
                    {
                        case 0:
                            GameLoop();
                            break;
                        case 1:
                            ControlKeys();
                            break;
                        case 2:
                            Scoreboard();
                            break;
                        case 3:
                            Console.WriteLine();
                            Console.ResetColor();
                            Save();
                            return;
                    }
                }
            }

        }

        public static void GameLoop()
        {
            try
            {
                Airplane A = new Airplane();
                DispatchersController D = new DispatchersController();
                A.RangeHandler += D.Swap;
                A.TakeOffHandler += D.TakeOff;
                A.LandingHandler += D.Landing;
                GameController C = new GameController();
                A.StatusHandler += C.SetStatus;

                while (true)
                {
                    Interface.DrawGameInterface(A.CurrentSpeed, A.currentHeight, A.Range, C.GameStatus(),D.GetActive());

                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        if ((key.Modifiers & ConsoleModifiers.Shift) != 0) A.changeHight(Values.heightShiftDelta);
                        else A.changeHight(Values.heightDelta);
                    }

                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        if ((key.Modifiers & ConsoleModifiers.Shift) != 0) A.changeHight(-Values.heightShiftDelta);
                        else A.changeHight(-Values.heightDelta);

                    }

                    if (key.Key == ConsoleKey.LeftArrow)
                    {
                        if ((key.Modifiers & ConsoleModifiers.Shift) != 0) A.changeSpeed(-Values.speedShiftDelta);
                        else A.changeSpeed(-Values.speedDelta);

                    }

                    if (key.Key == ConsoleKey.RightArrow)
                    {
                        if ((key.Modifiers & ConsoleModifiers.Shift) != 0) A.changeSpeed(Values.speedShiftDelta);
                        else A.changeSpeed(Values.speedDelta);

                    }
                    if (key.Key == ConsoleKey.Escape)
                    {
                        return;
                    }
                    if (A.Landed())
                    {
                        Interface.DrawWin(D.totalScore);
              
                        if(Score.Count == 10 && D.totalScore > Score[0])
                        {
                            Score.RemoveAt(0);
                            Score.Add(D.totalScore);
                        }
                        else if (Score.Count < 10)
                        {
                             Score.Add(D.totalScore);
                        }
                        Score.Sort();
                        
                
                        while (true)
                        {
                            ConsoleKeyInfo backKey = Console.ReadKey();
                            if (backKey.Key == ConsoleKey.Enter || backKey.Key == ConsoleKey.Escape)
                            {
                                return;
                            }
                        }
                    }
                }
            }
            catch (PlaneCrashedException ex)
            { 
                while (true)
                {
                    Interface.DrawExcResult(ex.Message, ex.InnerException.Message);
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Escape)
                    {
                        return;
                    }
                }
            }
            catch (NotSuitableException ex)
            {
                while (true)
                {
                    Interface.DrawExcResult(ex.Message, ex.InnerException.Message);
                    ConsoleKeyInfo key = Console.ReadKey();

                    if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Escape)
                    {
                        return;
                    }
                }
            }
        }
    }
}


            

        


    

