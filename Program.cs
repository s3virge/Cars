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
        private static Car car;
        private static int left;
        private static int top;
        private int wndWidth, wndHeight;
        public Program() {
            wndWidth = 40;
            wndHeight = 50;
            Console.SetWindowSize(wndWidth, wndHeight);
            Console.SetBufferSize(wndWidth, wndHeight);
            car = new Car();
            left = 10;
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

            Console.WriteLine("Press ESC to Exit");

            car.setLeftTop(left, top);
            car.draw();

            var taskKeys = new Task(app.ReadKeys);
            taskKeys.Start();
     
            var tasks = new[] { taskKeys };
            Task.WaitAll(tasks);
        }
                
        private void ReadKeys() {
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            while (!Console.KeyAvailable && key.Key != ConsoleKey.Escape) {

                key = Console.ReadKey(true);

                switch (key.Key) {
                    case ConsoleKey.UpArrow:
                        //Console.WriteLine("UpArrow was pressed");
                        if (--top <= 0)
                            top = 0;
                        car.setTop(top);                        
                        break;
                    case ConsoleKey.DownArrow:
                        //Console.WriteLine("DownArrow was pressed");
                        int bottom = wndHeight - car.getLength();
                        if (++top >= bottom){
                            top = bottom;
                        }
                        car.setTop(top);
                        break;

                    case ConsoleKey.RightArrow:
                        //Console.WriteLine("RightArrow was pressed");
                        int right = wndWidth - car.getWidth() - 1;
                        if (++left >= right){
                            left = right;
                        }
                        car.setLeft(left);
                        break;

                    case ConsoleKey.LeftArrow:
                        //Console.WriteLine("LeftArrow was pressed");
                        if (--left <= 0) {
                            left = 0;
                        }
                        car.setLeft(left);
                        break;

                    case ConsoleKey.Escape:
                        break;

                    default:
                        if (Console.CapsLock && Console.NumberLock) {
                            Console.WriteLine(key.KeyChar);
                        }
                        break;
                }
                Console.Clear();
                car.draw();                
            }
        }
    }
}
