using System;
using System.Threading;

namespace LockQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            var synchronizedQueue = new SynchronizedQueue();

            var firstThread = new Thread(() =>
            {
                while (true)
                {
                    synchronizedQueue.Add(new QueueItem("First thread item"));
                    Thread.Sleep(1000);
                }
            });

            var secondThread = new Thread(() =>
            {
                while (true)
                {
                    synchronizedQueue.Add(new QueueItem("Second thread item"));
                    Thread.Sleep(1000);
                }
            });

            var thirdThread = new Thread(() =>
            {
                while (true)
                {
                    synchronizedQueue.ShowAll();
                    Thread.Sleep(1000);
                }
            });

            var fourthThread = new Thread(() =>
            {
                while (true)
                {
                    synchronizedQueue.Take().Match(
                        some: item => Console.WriteLine($"Item from queue {item.ItemName}"),
                        none: () => Console.WriteLine("There is no item on queue"));

                    Thread.Sleep(400);
                }
            });


            firstThread.Start();
            secondThread.Start();
            thirdThread.Start();
            fourthThread.Start();

            Console.ReadKey();
        }
    }
}
