using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars {
    class Print {
        public static void Msg(string msg) {
            Console.Beep();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;

            int leftOffset = (Console.WindowWidth / 2 - msg.Length / 2) - 1;
            int topOffset = 19;

            //paint background for message
            for (int r = 0; r < 3; r++) {
                for (int c = 0; c < msg.Length + 2; c++) {
                    Console.SetCursorPosition(c + leftOffset, r + topOffset);
                    Console.Write(' ');
                }
            }
            Console.SetCursorPosition(leftOffset + 1, topOffset + 1);
            Console.WriteLine(msg);

            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void Score(ref int score) {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            int offsetLeft = 29;
            int offsetTop = 5;

            string msg = string.Format("You score: {0}", score++);

            for (int row = 0; row < 3; row++) {
                for (int col = 0; col < msg.Length + 2; col++) {
                    Console.SetCursorPosition(col + offsetLeft, row + offsetTop);
                    Console.WriteLine(' ');
                }
            }

            Console.SetCursorPosition(offsetLeft + 1, offsetTop + 1);
            Console.WriteLine(msg);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
