﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cars
{
    class Program
    {
        //static void Main(string[] args) {
        private static Car car;
        private static int left;
        private static int top;

        public Program()
        {
            int wndWidth = 40, wndHeight = 50;
            Console.SetWindowSize(wndWidth, wndHeight);
            Console.SetBufferSize(wndWidth, wndHeight);
            car = new Car();
            left = 10;
            top = Console.WindowHeight - car.getLength();
        }

        /* about tasks
         * https://metanit.com/sharp/tutorial/12.1.php 
         */
        static void Main(string[] args)
        {

            new Program();

            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine("Exiting...");
                Environment.Exit(0);
            };

            Console.WriteLine("Press ESC to Exit");

            var taskKeys = new Task(ReadKeys);
            var taskDraw = new Task(drawCar);

            Task task = new Task(() => Console.WriteLine("Hello Task!"));
            task.Start();

            taskKeys.Start();
            taskDraw.Start();

            var tasks = new[] { taskKeys };
            Task.WaitAll(tasks);
        }

        private static void drawCar()
        {
            for (; ; )
            {
                car.draw(left, top);
                
                Debug.WriteLine("drawCar() was launch");
            }
        }

        private static void ProcessFiles()
        {
            var files = Enumerable.Range(1, 100).Select(n => "File" + n + ".txt");

            var taskBusy = new Task(BusyIndicator);
            taskBusy.Start();

            foreach (var file in files)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Procesing file {0}", file);
            }
        }

        private static void BusyIndicator()
        {
            var busy = new ConsoleBusyIndicator();
            busy.UpdateProgress();
        }

        private static void ReadKeys()
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            while (!Console.KeyAvailable && key.Key != ConsoleKey.Escape)
            {

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        Console.WriteLine("UpArrow was pressed");
                        break;
                    case ConsoleKey.DownArrow:
                        Console.WriteLine("DownArrow was pressed");
                        break;

                    case ConsoleKey.RightArrow:
                        Console.WriteLine("RightArrow was pressed");
                        break;

                    case ConsoleKey.LeftArrow:
                        Console.WriteLine("LeftArrow was pressed");
                        break;

                    case ConsoleKey.Escape:
                        break;

                    default:
                        if (Console.CapsLock && Console.NumberLock)
                        {
                            Console.WriteLine(key.KeyChar);
                        }
                        break;
                }
            }
        }
    }

    internal class ConsoleBusyIndicator
    {
        int _currentBusySymbol;

        public char[] BusySymbols { get; set; }

        public ConsoleBusyIndicator()
        {
            BusySymbols = new[] { '|', '/', '-', '\\' };
            Console.CursorVisible = false;
        }

        public void UpdateProgress()
        {
            while (true)
            {
                Thread.Sleep(100);
                var originalX = Console.CursorLeft;
                var originalY = Console.CursorTop;

                Console.Write(BusySymbols[_currentBusySymbol]);

                _currentBusySymbol++;

                if (_currentBusySymbol == BusySymbols.Length)
                {
                    _currentBusySymbol = 0;
                }

                Console.SetCursorPosition(originalX, originalY);
            }
        }
    }
}
