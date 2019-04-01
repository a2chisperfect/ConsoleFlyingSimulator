using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
    class Button
    {
        string text;
        int xPos;
        int yPos;

        public Button(int x, int y, string text)
        {
            xPos = x;
            yPos = y;
            this.text = text;
        }
        public void Show()
        {
            Console.ResetColor();
            Console.SetCursorPosition(xPos, yPos);
            Console.Write(text);
        }
        public void Selected()
        {
            Console.ResetColor();
            Console.SetCursorPosition(xPos, yPos);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
        }
    }
}
