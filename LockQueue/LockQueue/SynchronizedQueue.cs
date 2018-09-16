using System;
using System.Collections.Generic;
using Optional;

namespace LockQueue
{
    public class QueueItem
    {
        public QueueItem(string itemName)
        {
            ItemName = itemName;
        }

        public string ItemName { get; }
    }

    public class SynchronizedQueue
    {
        private readonly Queue<QueueItem> _queueItems;

        private static readonly object Lock = new object();

        public SynchronizedQueue()
        {
            _queueItems = new Queue<QueueItem>();
        }
        public void Add(QueueItem item)
        {
            lock (Lock)
            {
                _queueItems.Enqueue(item);
            }
        }

        public Option<QueueItem> Take()
        {
            lock (Lock)
            {
                return _queueItems.TryDequeue(out var item) ? item.Some() : Option.None<QueueItem>();
            }
        }

        public void ShowAll()
        {
            Console.WriteLine("On queue:");
            lock (Lock)
            {
                foreach (var item in _queueItems)
                {
                    Console.WriteLine($"{item.ItemName}");
                }
            }
        }
    }
}
