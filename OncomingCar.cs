using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cars {
    class OncomingCar : Cars {

        private static byte[,] shape = new byte[length, width] {
            { leftWheels, hatch, hatch, hatch, hatch, hatch, rightWheels },
            { leftDoor, roof, roof, roof, roof, roof, rightDoor },
            { leftWheels, roof, roof, roof, roof, roof, rightWheels },
            { rightWing, hood, hood, hood, hood, hood, leftWing }
        };

        public OncomingCar() : base(0, 0) {
        }

        public override void draw() {
            Console.SetCursorPosition(left, top);

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

        public void moveDown() {
            cleanBehind();
            top++;
            draw();            
            Thread.Sleep(500);
        }

        /// <summary>
        /// clean screen behide the  car
        /// </summary>
        private void cleanBehind() {
            for (int l = left; l < width; l++) {
                Console.SetCursorPosition(l, top);
                Console.Write(" ");
            }
        }

        public int getLeft() {
            return left;
        }

        public int getTop() {
            return top;
        }
    }
}
