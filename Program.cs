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
        private int wndWidth, wndHeight;

        static object locker = new object();

        public Program() {
            wndWidth = 50;
            wndHeight = 40;

            new Road();

            Console.SetWindowSize(wndWidth, wndHeight);
            Console.SetBufferSize(wndWidth, wndHeight);
            Console.CursorVisible = false;

            car = new ControlledCar();

            car.setLeft(Road.rightSide - car.getWidth());
            car.setTop(Console.WindowHeight - car.getLength() - 1);            
        }


        /// //////////////////////////////////////////
        //todo controlled car must be on the road
        /// //////////////////////////////////////////
        
        static void Main(string[] args) {
            Program app = new Program();

            Console.CancelKeyPress += (sender, e) => {
                Console.WriteLine("Exiting...");
                Environment.Exit(0);
            };

            //Console.WriteLine("Press ESC to Exit");
            Road.draw();
            
            car.draw();

            var taskKeys = new Task(app.controlledCarRoutine);
            taskKeys.Start();

            var taskOncomingCar = new Task(app.oncommingCarRoutine);
            taskOncomingCar.Start();

            var tasks = new[] { taskKeys, taskOncomingCar };
            Task.WaitAll(tasks);
        }

        /// <summary>
        /// method starts in separated process and draws the oncommint car in new position
        /// </summary>
        private void oncommingCarRoutine() {
            //гонять машинку по кругу с разными смещениями по left
            OncomingCar oncCar = new OncomingCar();

            for (; ; ) {
                moveDownOncomingCar(ref oncCar);
                oncCar.Speed -= 15;
                Debug.WriteLine("oncCar.Speed = {0}", oncCar.Speed);
            }
        }

        private void moveDownOncomingCar(ref OncomingCar oncomingCar) {
            //на край дороги выезжать не будем, поэтому leftSide + 1
            int col = new Random().Next(Road.leftSide + 1, Road.rightSide - oncomingCar.getWidth() + 1);

            oncomingCar.setLeftTop(col, 0 - car.getLength()); //машинка за верхнем краем окна.

            for (; oncomingCar.getTop() < wndHeight;) {
                oncomingCar.moveDown();
                Debug.WriteLine("moveDownOncomingCar(). oncomingCar.getTop() = {0}", oncomingCar.getTop());
            }
            Debug.WriteLine("moveDownOncomingCar() - loop is finished.");
        }

        private void controlledCarRoutine() {
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            int carLeft, carTop;

            while (!Console.KeyAvailable && key.Key != ConsoleKey.Escape) {

                key = Console.ReadKey(true);

                lock (locker) {
                    switch (key.Key) {

                        case ConsoleKey.UpArrow:
                            //Console.WriteLine("UpArrow was pressed");
                            carTop = car.getTop();
                            if (--carTop <= 0)
                                carTop = 0;
                            car.setTop(carTop);
                            car.wipeBehind();
                            break;

                        case ConsoleKey.DownArrow:
                            //Console.WriteLine("DownArrow was pressed");
                            int bottom = wndHeight - car.getLength() - 1;
                            carTop = car.getTop();
                            if (++carTop >= bottom) {
                                carTop = bottom;
                            }
                            car.setTop(carTop);
                            car.wipeBefore();
                            break;

                        case ConsoleKey.LeftArrow:
                            //Console.WriteLine("LeftArrow was pressed");
                            carLeft = car.getLeft();
                            if (--carLeft <= 0) {
                                carLeft = 0;
                            }
                            car.setLeft(carLeft);
                            car.wipeRight();
                            break;

                        case ConsoleKey.RightArrow:
                            //Console.WriteLine("RightArrow was pressed");
                            int right = wndWidth - car.getWidth();
                            carLeft = car.getLeft();
                            if (++carLeft >= right) {
                                carLeft = right;
                            }
                            car.setLeft(carLeft);
                            car.wipeLeft();
                            break;

                        case ConsoleKey.Escape:
                            break;
                    }

                    car.draw();
                }
            }
        }
    }
}