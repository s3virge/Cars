using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars {
    class ControlledCar : Cars {

        private static byte[,] shape = new byte[length, width] {
            { leftWing, hood, hood, hood, hood, hood, rightWing },
            { leftWheels, roof, roof, roof, roof, roof, rightWheels },
            { leftDoor, roof, roof, roof, roof, roof, rightDoor },
            { leftWheels, hatch, hatch, hatch, hatch, hatch, rightWheels }
        };

        public ControlledCar() : base(0, 0) {
            bodyColor = ConsoleColor.DarkGreen;
        }

        public override void draw() {

            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = bodyColor;

            int elem = top;

            byte i, j;
            /* output each array element's value */
            for (i = 0; i < length; i++) {
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


        public void wipeBehind() {
            //wipe behinde the car from lelt to right
            for (int l = 0; l < width; l++) {
                Console.SetCursorPosition(left + l, top + length);
                Console.Write(" ");
            }
        }


        public void wipeBefore() {
            //wipe befor the car from lelt to right
            for (int pos = 0; pos < width; pos++) {
                Console.SetCursorPosition(left + pos, top - 1);
                Console.Write(" ");
            }
        }


        public void wipeLeft() {
            //wipe befor the car from lelt to right
            for (int pos = 0; pos < length; pos++) {
                Console.SetCursorPosition(left - 1, top + pos);
                Console.Write(" ");
            }
        }


        public void wipeRight() {
            //wipe befor the car from lelt to right
            for (int pos = 0; pos < length; pos++) {
                Console.SetCursorPosition(left + width, top + pos);
                Console.Write(" ");
            }
        }
    }
}
