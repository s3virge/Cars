using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cars {
    class Road {
        /// <summary>
        /// Road width 
        /// </summary>
        private int width { get; set; }

        private static bool finish = false;

        /// <summary>
        /// Left side position of thе road
        /// </summary>
        public int leftSide { get; set; }

        /// <summary>
        /// Right side of the road. Dpends from road width.
        /// </summary>
        int right_Side;
        public int rightSide {
            get {
                right_Side = leftSide + width;
                return right_Side;
            }
            set {
                right_Side = value;
            }
        }

        public Road(int roadWidth = 21) {
            width = roadWidth;
            leftSide = 0;
            rightSide = leftSide + width;
        }

        /// <summary>
        /// draws the road
        /// </summary>
        public void draw() {

            bool sign = true;
            
            for (; ; ) {
                drawSide(leftSide, sign);
                drawSide(rightSide, sign);

                if (sign)
                    sign = false;
                else
                    sign = true;

                if (finish)
                    return;

                Thread.Sleep(200);
            }
        }

        private void drawSide(int roadSide, bool sign) {

            int consoleWndHight = Console.WindowHeight;
            char[] roadBorder = { '|', ' ' };

            for (int h = 0; h < consoleWndHight; h++) {
                lock (Program.lockObj) {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(roadSide, h);

                    if (sign) {
                        Console.Write(roadBorder[0]);
                        sign = false;
                    }
                    else {
                        Console.Write(roadBorder[1]);
                        sign = true;
                    }
                }
            }
        }

        public void setFinish() {
            finish = true;
        }
    }
}
