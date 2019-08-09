using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cars {
    class Program {
        //static void Main(string[] args) {
        private static ControlledCar car;
        private static int left;
        private static int top;
        private int wndWidth, wndHeight;
        public Program() {
            wndWidth = 20;
            wndHeight = 50;
            Console.SetWindowSize(wndWidth, wndHeight);
            Console.SetBufferSize(wndWidth, wndHeight);
            car = new ControlledCar();
            left = wndWidth - car.getWidth();
            top = Console.WindowHeight - car.getLength() - 1;
            Console.CursorVisible = false;
        }

        /* about tasks
         * https://metanit.com/sharp/tutorial/12.1.php 
         */
        static void Main(string[] args) {
            Program app = new Program();

            Console.CancelKeyPress += (sender, e) => {
                Console.WriteLine("Exiting...");
                Environment.Exit(0);
            };

            //Console.WriteLine("Press ESC to Exit");

            car.setLeftTop(left, top);
            car.draw();

            var taskKeys = new Task(app.controlledCarRoutine);
            taskKeys.Start();

            var taskOncomingCar = new Task(app.oncommingCarRoutine);
            taskOncomingCar.Start();

            //var taskOncomingCar2 = new Task(app.oncommingCarRoutine);
            //taskOncomingCar2.Start();
            
            var tasks = new[] { taskKeys, taskOncomingCar};
            Task.WaitAll(tasks);
        }

        /// <summary>
        /// method starts in separated process and draws the oncommint car in new position
        /// </summary>
        private void oncommingCarRoutine() {
            //гонять машинку по кругу с разными смещениями по left
            OncomingCar oncCar = new OncomingCar();

            for (; ; ) {
                oncCar.Speed -= 10;
                Debug.WriteLine("oncCar.Speed = {0}", oncCar.Speed);
                moveDownOncomingCar(ref oncCar);
            }
        }

        private void moveDownOncomingCar(ref OncomingCar oncomingCar) {
            
            int col = new Random().Next(wndWidth - oncomingCar.getWidth());
            oncomingCar.setLeftTop(col, 0 - car.getLength()); //машинка за верхнем краем окна.

            for (; oncomingCar.getTop() < wndHeight;) {               
                oncomingCar.moveDown();
                Debug.WriteLine("moveDownOncomingCar(). oncomingCar.getTop() = {0}", oncomingCar.getTop());
            }
            Debug.WriteLine("loop is finished");
        }

        private void controlledCarRoutine() {
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            while (!Console.KeyAvailable && key.Key != ConsoleKey.Escape) {

                key = Console.ReadKey(true);

                switch (key.Key) {
                    case ConsoleKey.UpArrow:
                        //Console.WriteLine("UpArrow was pressed");
                        if (--top <= 0)
                            top = 0;
                        car.setTop(top);
                        car.wipeBehind();
                        break;

                    case ConsoleKey.DownArrow:
                        //Console.WriteLine("DownArrow was pressed");
                        int bottom = wndHeight - car.getLength() - 1;
                        if (++top >= bottom) {
                            top = bottom;
                        }
                        car.setTop(top);
                        car.wipeBefore();
                        break;

                    case ConsoleKey.LeftArrow:
                        //Console.WriteLine("LeftArrow was pressed");
                        if (--left <= 0) {
                            left = 0;
                        }
                        car.setLeft(left);
                        car.wipeRight();
                        break;

                    case ConsoleKey.RightArrow:
                        //Console.WriteLine("RightArrow was pressed");
                        int right = wndWidth - car.getWidth();
                        if (++left >= right) {
                            left = right;
                        }
                        car.setLeft(left);
                        car.wipeLeft();
                        break;

                    case ConsoleKey.Escape:
                        break;

                    default:
                        if (Console.CapsLock && Console.NumberLock) {
                            Console.WriteLine(key.KeyChar);
                        }
                        break;
                }
                car.draw();
            }
        }
    }
}
