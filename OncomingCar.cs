using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cars {
    class OncomingCar : Cars {

        private static byte[,] shape1 = new byte[length, width] {
            { leftWheels, hatch, hatch, hatch, hatch, hatch, rightWheels },
            { leftDoor, roof, roof, roof, roof, roof, rightDoor },
            { leftWheels, roof, roof, roof, roof, roof, rightWheels },
            { rightWing, hood, hood, hood, hood, hood, leftWing }
        };

        private static byte[,] shape2 = new byte[length, width] {
            { leftWheels1, hatch1, hatch1, hatch1, hatch1, hatch1, rightWheels1 },
            { leftDoor1, roof1, roof1, roof1, roof1, roof1, rightDoor1 },
            { leftWheels1, roof1, roof1, roof1, roof1, roof1, rightWheels1 },
            { rightWing1, hood1, hood1, roof1, hood1, hood1, leftWing1 }
        };

        private static byte[,] shape3 = new byte[length, width] {
            { leftWheels2, hatch2, hatch2, hatch2, hatch2, hatch2, rightWheels2 },
            { leftDoor2, roof2, roof2, roof2, roof2, roof2, rightDoor2 },
            { leftWheels2, roof2, roof2, roof2, roof2, roof2, rightWheels2 },
            { rightWing2, hood2, hood2, hood2, hood2, hood2, leftWing2 }
        };

        private static byte[,] shape4 = new byte[length, width] {
            { leftWheels3, hatch3, hatch3, hatch3, hatch3, hatch3, rightWheels3 },
            { leftDoor3, roof3, roof3, roof3, roof3, roof3, rightDoor3 },
            { leftWheels3, roof3, roof3, roof3, roof3, roof3, rightWheels3 },
            { rightWing2, hood3, hood3, hood3, hood3, hood3, leftWing2 }
        };

        private static byte[,] shape;

        public OncomingCar() : base(0, 0 - length) {
            bodyColor = ConsoleColor.Yellow;
        }

        public void RandomizeShape() {
            Random rand = new Random();
            int carShape = rand.Next(4);

            switch (carShape) {
                case 0:
                    shape = shape1;
                    break;

                case 1:
                    shape = shape2;
                    break;

                case 2:
                    shape = shape3;
                    break;

                case 3:
                    shape = shape4;
                    break;
            }

            //shape = shape4;
        }

        public override void draw() {
            lock (Program.lockObj) {
                Console.ForegroundColor = bodyColor;
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
                        Debug.WriteLine("{0}() method gen an exception {1}", System.Reflection.MethodBase.GetCurrentMethod().Name,  aorException.Message);
                        break;
                    }
                }
            }
        }

        public void moveDown() {
            //не вытирать за машинкой пока она полностью не показалась.
            top++;
            
            draw();

            if (top > 0) {
                cleanBehind();
            }

            Thread.Sleep(redrawTimeOut);
        }

        /// <summary>
        /// clean screen behide the  car
        /// </summary>
        private void cleanBehind() {
            lock (Program.lockObj) {
                for (int el = 0; el < width; el++) {
                    Console.SetCursorPosition(left + el, top - 1);
                    Console.Write(" ");
                }
            }
        }
    }
}
