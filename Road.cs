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
        public static int leftSide { get; set; }
        
        /// <summary>
        /// Right side of the road. Dpends from road width.
        /// </summary>
        public static int rightSide { get; set; }

        public Road(int width = 20) {
            this.width = width;
            leftSide = 0;
            rightSide = leftSide + this.width;            
        }

        /// <summary>
        /// draws the road
        /// </summary>
        public static void draw() {
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
