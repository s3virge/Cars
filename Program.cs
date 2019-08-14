using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cars {
    class Program {

        static object lockObj = new object();

        //static void Main(string[] args) {
        private static ControlledCar car;
        private int wndWidth, wndHeight;
        private static int score;
        private static Road road;

        public Program() {
            wndWidth = 50;
            wndHeight = 40;
            score = 0;

            Console.SetWindowSize(wndWidth, wndHeight);
            Console.SetBufferSize(wndWidth, wndHeight);
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            road = new Road();

            car = new ControlledCar();

            car.setLeft(road.rightSide - car.getWidth());
            car.setTop(Console.WindowHeight - car.getLength() - 1);
        }

        static void Main(string[] args) {
            Program app = new Program();

            Console.CancelKeyPress += (sender, e) => {
                Console.WriteLine("Exiting...");
                Environment.Exit(0);
            };

            //Console.WriteLine("Press ESC to Exit");
            road.leftSide = 2;
            road.draw();

            Print.Score(ref score);

            car.draw();

            var taskKeys = new Task(app.controlledCarRoutine);
            taskKeys.Start();

            var taskOncomingCar = new Task(app.oncommingCarRoutine);
            taskOncomingCar.Start();

            var tasks = new[] { taskKeys, taskOncomingCar };
            Task.WaitAll(tasks);
        }

        /// <summary>
        /// method starts in separated process and draws the oncomming car in new position
        /// </summary>
        private void oncommingCarRoutine() {
            //гонять машинку по кругу с разными смещениями по left
            OncomingCar oncCar = new OncomingCar();

            for (; ; ) {
                if (!moveDownOncomingCar(ref oncCar)) {
                    Print.Msg("Game over");
                    return;
                }

                Print.Score(ref score);
                oncCar.Speed -= 15;
                Debug.WriteLine("oncCar.Speed = {0}", oncCar.Speed);
            }
        }
        private void controlledCarRoutine() {
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            int carLeft, carTop, bottomLimit, leftLimit, rightLimit;

            while (!Console.KeyAvailable && key.Key != ConsoleKey.Escape) {

                key = Console.ReadKey(true);

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
                        bottomLimit = wndHeight - car.getLength() - 1;
                        carTop = car.getTop();
                        if (++carTop >= bottomLimit) {
                            carTop = bottomLimit;
                        }
                        car.setTop(carTop);
                        car.wipeBefore();
                        break;

                    case ConsoleKey.LeftArrow:
                        //Console.WriteLine("LeftArrow was pressed");
                        leftLimit = road.leftSide + 1;
                        carLeft = car.getLeft();
                        if (--carLeft <= leftLimit) {
                            carLeft = leftLimit;
                        }
                        car.setLeft(carLeft);
                        car.wipeRight();
                        break;

                    case ConsoleKey.RightArrow:
                        //Console.WriteLine("RightArrow was pressed");
                        rightLimit = road.rightSide - car.getWidth();
                        carLeft = car.getLeft();
                        if (++carLeft >= rightLimit) {
                            carLeft = rightLimit;
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
        private bool moveDownOncomingCar(ref OncomingCar oncomingCar) {
            //на край дороги выезжать не будем, поэтому leftSide + 1
            int col = new Random().Next(road.leftSide + 1, road.rightSide - oncomingCar.getWidth() + 1);

            oncomingCar.setLeftTop(col, 0 - car.getLength()); //машинка за верхнем краем окна.

            for (; oncomingCar.getTop() < wndHeight;) {
                oncomingCar.moveDown();

                if (isCrush(ref oncomingCar, ref car))
                    return false;
            }

            Debug.WriteLine("moveDownOncomingCar() - loop is finished.");
            return true;
        }
        private bool isCrush(ref OncomingCar oncomCar, ref ControlledCar controlCar) {

            int onCarLeft = oncomCar.getLeft();
            int onCarTop = oncomCar.getTop();

            int contCarLeft = controlCar.getLeft();
            int contCarTop = controlCar.getTop();

            int leftDifference = onCarLeft - contCarLeft;

            if (leftDifference < 0)
                leftDifference = -(leftDifference);

            int topDifference = onCarTop - contCarTop;

            if (topDifference < 0)
                topDifference = -(topDifference);

            int carWidth = controlCar.getWidth();
            int carLength = controlCar.getLength();

            if (leftDifference < carWidth && topDifference <= carLength) {
                Debug.WriteLine("Cars were crushed");
                return true;
            }

            return false;
        }
    }
}

