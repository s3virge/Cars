using System;
using System.Text;

namespace Cars {
    abstract class Cars {
        //where the cars will draws
        protected int left;
        protected int top;

        protected static byte leftWheels = 91;
        protected static byte rightWheels = 93;
        protected static byte hood = 178;
        protected static byte roof = 219;
        protected static byte hatch = 219;
        protected static byte leftWing = 47;
        protected static byte rightWing = 92;
        protected static byte leftDoor = 0;
        protected static byte rightDoor = 0;

        private int speed;
        public int Speed {
            get {
                return speed;
            }

            set {
                int maxSpeed = 45;
                if (value > maxSpeed)
                    speed = value;
                else 
                    speed = maxSpeed;                
            }
        }

        //all cars has the same size
        protected const int width = 7;
        protected const int length = 4;

        public Cars(int left, int top, int speed = 200) {
            Console.OutputEncoding = Encoding.UTF8;
            this.left = left;
            this.top = top;
            Speed = speed;
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
