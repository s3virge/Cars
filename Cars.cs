using System;
using System.Text;

namespace Cars {
    abstract class Cars {
        //where the cars will draws
        protected int left;
        protected int top;
        protected ConsoleColor bodyColor;

        protected static byte leftWheels = 91;
        protected static byte rightWheels = 93;
        protected static byte hood = 178;
        protected static byte roof = 219;
        protected static byte hatch = 219;
        protected static byte leftWing = 47;
        protected static byte rightWing = 92;
        protected static byte leftDoor = 0;
        protected static byte rightDoor = 0;

        protected static byte leftWheels1 = 199;
        protected static byte rightWheels1 = 182;
        protected static byte hood1 = 177;
        protected static byte roof1 = 178;
        protected static byte hatch1 = 177;
        protected static byte leftWing1 = 190;
        protected static byte rightWing1 = 212;
        protected static byte leftDoor1 = 195;
        protected static byte rightDoor1 = 180;

        protected static byte leftWheels2 = 199;
        protected static byte rightWheels2 = 182;
        protected static byte hood2 = 178;
        protected static byte roof2 = 206;
        protected static byte hatch2 = 219;
        protected static byte leftWing2 = 47;
        protected static byte rightWing2 = 92;
        protected static byte leftDoor2 = 0;
        protected static byte rightDoor2 = 0;

        protected static byte leftWheels3 = 73;
        protected static byte rightWheels3 = 73;
        protected static byte hood3 = 207;
        protected static byte roof3 = 0;
        protected static byte hatch3 = 176;
        protected static byte leftWing3 = 47;
        protected static byte rightWing3 = 92;
        protected static byte leftDoor3 = 195;
        protected static byte rightDoor3 = 180;

        int maxSpeed = 350;
        int minTimeOut = 10;

        //redraw timeout
        private int timeout;
        public int redrawTimeOut {
            get {
                return timeout;
            }

            set {                
                if (value > minTimeOut)
                    timeout = value;
                else 
                    timeout = minTimeOut;                
            }
        }

        private int speed;
        public int Speed {
            get {
                return speed;
            }
            set {               
                if (value < maxSpeed)
                    speed = value;
                else
                    speed = maxSpeed;
            }
        }

        //all cars has the same size
        protected const int width = 7;
        protected const int length = 4;

        public Cars(int left, int top) {            
            this.left = left;
            this.top = top;
            redrawTimeOut = maxSpeed - 100;
            Speed = minTimeOut + 50;
        }

        public void setColor(ConsoleColor color) {
            bodyColor = color;
        }


        /// <summary>
        /// get the width of the car
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
            return length;
        }

        /// <summary>
        /// set the left position of the car
        /// </summary>
        /// <param name="l"></param>
        public void setLeft(int l) {
            left = l;
        }

        public int getLeft() {
            return left;
        }
        
        /// <summary>
        /// set the top position of the car
        /// </summary>
        /// <param name="t"></param>
        public void setTop(int t) {
            top = t;
        }

        public int getTop() {
            return top;
        }

        /// <summary>
        /// set the left and top position of the car
        /// </summary>        
        public void setLeftTop(int left, int top) {
            this.left = left;
            this.top = top;
        }
        
        // draws the car whith desaire shape and position
        public abstract void draw();
    }
}
