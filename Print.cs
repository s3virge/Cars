using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cars {
    class Print {
        public static void GameOver() {

            Thread.Sleep(700);

            lock (Program.lockObj) {
                string msg = "Game over";
                
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;

                int leftOffset = (Console.WindowWidth / 2 - msg.Length / 2) - 2;
                int topOffset = 19;

                //paint background for message
                for (int r = 0; r < 3; r++) {
                    for (int c = 0; c < msg.Length + 4; c++) {
                        Console.SetCursorPosition(c + leftOffset, r + topOffset);
                        Console.Write(' ');
                        Thread.Sleep(40);
                    }
                }
                Console.SetCursorPosition(leftOffset + 2, topOffset + 1);
                Console.WriteLine(msg);

                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        public static void Msg(string msg, int leftOffset, int topOffset, ConsoleColor bgColor = ConsoleColor.Black, ConsoleColor fgColor = ConsoleColor.White) {
            lock (Program.lockObj) {
                Console.Beep();
                Console.BackgroundColor = bgColor;
                Console.ForegroundColor = fgColor;
                
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
        }

        public static void Score(ref int score) {
            lock (Program.lockObj) {
                //Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;

                int offsetLeft = 29;
                int offsetTop = 5;

                string msg = string.Format("Score: {0}", score++);

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

        public static void Speed(int speed) {
            lock (Program.lockObj) {
                //Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;

                int offsetLeft = 29;
                int offsetTop = 7;

                string msg = string.Format("Speed: {0}", speed);

                //for (int row = 0; row < 3; row++) {
                //    for (int col = 0; col < msg.Length + 2; col++) {
                //        Console.SetCursorPosition(col + offsetLeft, row + offsetTop);
                //        Console.WriteLine(' ');
                //    }
                //}

                Console.SetCursorPosition(offsetLeft + 1, offsetTop + 1);
                Console.WriteLine(msg);

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static void Codepage() {
            // Задаем заголовок окна
            Console.Title = "Кодировка 437: MS-DOS ASCII";

            for (byte b = 0; b < byte.MaxValue; b++) {
                char c = Encoding.GetEncoding(437).GetChars(new byte[] { b })[0];
                switch (b) {
                    case 8: // Backspace
                    case 9: // Tab
                    case 10: // Перевод строки
                    case 13: // Возврат каретки
                        c = '.';
                        break;
                }

                Console.Write("{0:000} {1}   ", b, c);

                // 7 это сигнал -- Console.Beep() работает также
                if (b == 7) Console.Write(" ");

                if ((b + 1) % 8 == 0)
                    Console.WriteLine();
            }
            Console.WriteLine();

            Console.ReadKey();
        }

    }
}
