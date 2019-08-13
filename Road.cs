using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars {
    class Road {
        /// <summary>
        /// Road width 
        /// </summary>
        private int width { get; set; }

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
            char roadBorder = '|';
            int consoleWndHight = Console.WindowHeight;
            for (int h = 0; h < consoleWndHight; h++) {
                Console.SetCursorPosition(leftSide, h);
                Console.Write(roadBorder);
            }

            for (int h = 0; h < consoleWndHight; h++) {
                Console.SetCursorPosition(rightSide, h);
                Console.Write(roadBorder);
            }
        }
    }
}
