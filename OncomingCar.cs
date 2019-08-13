using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cars {
    class OncomingCar : Cars {

        static object locker = new object();

        private static byte[,] shape = new byte[length, width] {
            { leftWheels, hatch, hatch, hatch, hatch, hatch, rightWheels },
            { leftDoor, roof, roof, roof, roof, roof, rightDoor },
            { leftWheels, roof, roof, roof, roof, roof, rightWheels },
            { rightWing, hood, hood, hood, hood, hood, leftWing }
        };

        public OncomingCar() : base(0, 0 - length) {
        }

        public override void draw() {

            //car draws from top to bootom

            int topCursorPos = top;

            //установить курсор за край окна нельзя
            if (top < 0)
                topCursorPos = 0;

            //wipe car when she touched the bootom 
            if (top >= Console.WindowHeight) {
                return;
            }

            Console.SetCursorPosition(left, topCursorPos);
            //----------------------------------------------

            //если спрайт машинки за верхним краем экрана
            //то вычислить сколько частей корпуса машинки уже показалось из-за верхнего края окна
            int shownCarPiece = length;
            if (top < 0)
                shownCarPiece = top + length;

            if (shownCarPiece == 0)
                shownCarPiece = -(top);

            byte i, j;
            /* output each array element's value */
            i = (byte)(length - shownCarPiece);

            for (; i < length; i++) {
                for (j = 0; j < width; j++) {
                    char c = Encoding.GetEncoding(437).GetChars(new byte[] { shape[i, j] })[0];
                    Console.Write(c);
                }

                try {
                    Console.SetCursorPosition(left, ++topCursorPos);
                }
                catch (ArgumentOutOfRangeException aorException) {
                    Debug.WriteLine("draw() gen an exception - ", aorException.Message);
                    break;
                }
            }
        }

        public void moveDown() {
            //не вытирать за машинкой пока она полностью не показалась.
            top++;

            lock (locker) {
                draw();

                if (top > 0) {
                    cleanBehind();
                }
            }

            Thread.Sleep(Speed);
        }

        /// <summary>
        /// clean screen behide the  car
        /// </summary>
        private void cleanBehind() {
            for (int el = 0; el < width; el++) {
                Console.SetCursorPosition(left + el, top - 1);
                Console.Write(" ");
            }
        }
    }
}
