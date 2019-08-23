using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cars {
    class Program {

        public static object lockObj = new object();

        //static void Main(string[] args) {
        private static ControlledCar controledCar;
        private static OncomingCar oncomingCar;
        private int wndWidth, wndHeight;
        private static int score;
        private static Road road;
        private int deltaSpeed;

        public Program() {
            wndWidth = 50;
            wndHeight = 40;
            score = 0;
            deltaSpeed = 12;

            Console.SetWindowSize(wndWidth, wndHeight);
            Console.SetBufferSize(wndWidth, wndHeight);
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            road = new Road();
            road.leftSide = 2;

            controledCar = new ControlledCar();
            oncomingCar = new OncomingCar();

            controledCar.setLeft(road.rightSide - controledCar.getWidth());
            controledCar.setTop(Console.WindowHeight - controledCar.getLength() - 1);
        }

        static void Main(string[] args) {

            Program app = new Program();

            Console.CancelKeyPress += (sender, e) => {
                Console.WriteLine("Exiting...");
                Environment.Exit(0);
            };

            //Console.WriteLine("Press ESC to Exit");           

            Print.Score(ref score);
            Print.Speed(oncomingCar.Speed);

            controledCar.draw();

            var taskRoad = new Task(road.draw);
            taskRoad.Start();

            var taskKeys = new Task(app.controlledCarRoutine);
            taskKeys.Start();

            var taskOncomingCar = new Task(app.oncomingCarRoutine);
            taskOncomingCar.Start();

            var tasks = new[] { taskKeys, taskOncomingCar, taskRoad };
            Task.WaitAll(tasks);
        }

        /// <summary>
        /// method starts in separated process and draws the oncomming car in new position
        /// </summary>
        private void oncomingCarRoutine() {
            //гонять машинку по кругу с разными смещениями по left
            
            for (; ; ) {
                oncomingCar.RandomizeShape();

                if (!moveDownOncomingCar(ref oncomingCar)) {
                    Thread.Sleep(700);
                    Print.GameOver();
                    return;
                }

                Print.Score(ref score);
                oncomingCar.redrawTimeOut -= deltaSpeed;
                oncomingCar.Speed += deltaSpeed;
                Print.Speed(oncomingCar.Speed);
                //Debug.WriteLine("oncCar.Speed = {0}", oncCar.Speed);
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
                        carTop = controledCar.getTop();
                        if (--carTop <= 0)
                            carTop = 0;
                        controledCar.setTop(carTop);
                        controledCar.wipeBehind();
                        break;

                    case ConsoleKey.DownArrow:
                        //Console.WriteLine("DownArrow was pressed");
                        bottomLimit = wndHeight - controledCar.getLength() - 1;
                        carTop = controledCar.getTop();
                        if (++carTop >= bottomLimit) {
                            carTop = bottomLimit;
                        }
                        controledCar.setTop(carTop);
                        controledCar.wipeBefore();
                        break;

                    case ConsoleKey.LeftArrow:
                        //Console.WriteLine("LeftArrow was pressed");
                        leftLimit = road.leftSide + 1;
                        carLeft = controledCar.getLeft();
                        if (--carLeft <= leftLimit) {
                            carLeft = leftLimit;
                        }
                        controledCar.setLeft(carLeft);
                        controledCar.wipeRight();
                        break;

                    case ConsoleKey.RightArrow:
                        //Console.WriteLine("RightArrow was pressed");
                        rightLimit = road.rightSide - controledCar.getWidth();
                        carLeft = controledCar.getLeft();
                        if (++carLeft >= rightLimit) {
                            carLeft = rightLimit;
                        }
                        controledCar.setLeft(carLeft);
                        controledCar.wipeLeft();
                        break;

                    case ConsoleKey.Escape:
                        break;
                }

                controledCar.draw();
            }
        }
        private bool moveDownOncomingCar(ref OncomingCar oncomingCar) {
            //на край дороги выезжать не будем, поэтому leftSide + 1
            int col = new Random().Next(road.leftSide + 1, road.rightSide - oncomingCar.getWidth() + 1);

            oncomingCar.setLeftTop(col, 0 - controledCar.getLength()); //машинка за верхнем краем окна.

            for (; oncomingCar.getTop() < wndHeight;) {
                oncomingCar.moveDown();

                if (isCrush(ref oncomingCar, ref controledCar))
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
                road.setFinish();
                return true;
            }

            return false;
        }
    }
}

