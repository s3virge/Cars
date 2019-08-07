using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars {
    class Car {
        private static byte leftWheels = 91;
        private static byte rightWheels = 93;
        private static byte hood = 178;
        private static byte roof = 219;
        private static byte hatch = 219;
        private static byte leftWing = 47;
        private static byte rightWing = 92;
        private static byte leftDoor = 0;
        private static byte rightDoor = 0;

        private int left;
        private int top;

        private const int width = 7;
        private const int shapeLength = 4;

        private static byte[,] shape = new byte[shapeLength, width] {
            { leftWing, hood, hood, hood, hood, hood, rightWing },
            { leftWheels, roof, roof, roof, roof, roof, rightWheels },
            { leftDoor, roof, roof, roof, roof, roof, rightDoor },
            { leftWheels, hatch, hatch, hatch, hatch, hatch, rightWheels }
        };

        public Car() : this(0, 0) {
        }

        public Car(int left, int top) {
            Console.OutputEncoding = Encoding.UTF8;
            this.left = left;
            this.top = top;
        }

        /// <summary>
        /// get the weight of the car
        /// </summary>
        /// <returns></returns>
        public int getWidth() {
            return width;
        }

        /// <summary>
        /// get the length of the car
        /// </summary>
        /// <returns></returns>
        public int getLength() {
            return shapeLength;
        }
        
        /// <summary>
        /// set the left position of the car
        /// </summary>
        /// <param name="l"></param>
        public void setLeft(int l) {
            left = l;
        }

        /// <summary>
        /// set the top position of the car
        /// </summary>
        /// <param name="t"></param>
        public void setTop(int t) {
            top = t;
        }

        /// <summary>
        /// set the left and top position of the car
        /// </summary>        
        public void setLeftTop(int left, int top) {
            this.left = left;
            this.top = top;
        }

        /// <summary>
        /// drawing the car in desire position. 
        /// 
        /// </summary>
        public void draw() {            
            Console.SetCursorPosition(left, top);

            int elem = top;

            byte i, j;
            /* output each array element's value */
            for (i = 0; i < shapeLength; i++) {
                for (j = 0; j < width; j++) {
                    char c = Encoding.GetEncoding(437).GetChars(new byte[] { shape[i, j] })[0];
                    Console.Write(c);
                }
                try {
                    Console.SetCursorPosition(left, ++elem);
                }
                catch (ArgumentOutOfRangeException aor) {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine(aor.Message);
                    break;
                }
            }
        }

        public void moveLeft() {
        }
        public void moveRight() {

        }

        //public void printSymbols() {
        //    //http://krez0n.org.ua/c-ispolzovanie-system-console-dlya-sozdaniya-igr-v-tekstovom-rezhime-chast-2

        //    // Задаем заголовок окна
        //    Console.Title = "Кодировка 437: MS-DOS ASCII";

        //    for (byte b = 0; b < byte.MaxValue; b++) {
        //        //Returns an encoding for the specified code page
        //        char c = Encoding.GetEncoding(437).GetChars(new byte[] { b })[0];

        //        switch (b) {
        //            case 8: // Backspace
        //            case 9: // Tab
        //            case 10: // Перевод строки
        //            case 13: // Возврат каретки
        //                c = '.';
        //                break;
        //        }

        //        Console.Write("{0:000} {1}   ", b, c);

        //        // 7 это сигнал -- Console.Beep() работает также
        //        if (b == 7) Console.Write(" ");

        //        if ((b + 1) % 8 == 0)
        //            Console.WriteLine();
        //    }
        //    Console.WriteLine();
        //}
    }
}
