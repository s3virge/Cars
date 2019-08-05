using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    class Car
    {

        private string[,] shape = new string[4, 3] { { char.ConvertFromUtf32(8718), "1", "1" }, { "1", "1", "1" }, { "1", "1", "1" }, { "1", "1", "1" } };
        private int[,] ushape = new int[4, 3] { { 91, 1, 93 }, { 741, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };

        public Car()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }

        public void draw()
        {
            int i, j;
            /* output each array element's value */
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    Console.Write(shape[i, j]);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        public void drawU()
        {
            int i, j;
            /* output each array element's value */
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    Console.Write(shape[i, j]);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        public void printSymbols()
        {
            //http://krez0n.org.ua/c-ispolzovanie-system-console-dlya-sozdaniya-igr-v-tekstovom-rezhime-chast-2

            // Задаем заголовок окна
            Console.Title = "Кодировка 437: MS-DOS ASCII";

            for (byte b = 0; b < byte.MaxValue; b++)
            {
                char c = Encoding.GetEncoding(437).GetChars(new byte[] { b })[0];
                switch (b)
                {
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
        }
    }
}
