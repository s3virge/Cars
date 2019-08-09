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

        public OncomingCar() : base(0, 0 - length) {
        }

        public override void draw() {

            //car draws from top to bootom

            int topCursorPos = top;

            //установить курсор за край окна нельзя
            if (top < 0)
                topCursorPos = 0;

            Console.SetCursorPosition(left, topCursorPos);
            //----------------------------------------------

            int currentCarPiece = topCursorPos;

            //если спрайт машинки за верхним краем экрана
            //то сначала нужно нарисовать багажник машинки
            //вычислить сколько частей корпуса машинки уже показалось из-за верхнего края окна
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

                //try {
                //устанавливем курсор в нажную позицию если спрайт машинки попадает в окно
                if (shownCarPiece >= 0) {
                    Console.SetCursorPosition(left, ++currentCarPiece);
                }
                //}
                //catch (ArgumentOutOfRangeException aor) {
                //    Console.SetCursorPosition(0, 0);
                //    Console.WriteLine(aor.Message);
                //    break;
                //}
            }
        }

        public void moveDown() {
            //не вытирать за машинкой пока она полностью не показалась.
            top++;
            draw();

            if (top > 0) {
                cleanBehind();
            }

            Thread.Sleep(500);
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

        public int getLeft() {
            return left;
        }

        public int getTop() {
            return top;
        }
    }
}
