using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockingCollectionDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using BlockingCollection<int> numbersBlockingCollection = new(new ConcurrentQueue<int>());

            IEnumerable<int> fromOneToTwentyFiveThousand = Enumerable.Range(1, 25000);

            Task firstEnqueuingTask = Task.Run(() =>
           {
               foreach (var number in fromOneToTwentyFiveThousand)
               {
                   numbersBlockingCollection.Add(number);
                   Console.WriteLine($"{number} is Enqueued");
                   
               }
               numbersBlockingCollection.CompleteAdding();
           });

            Task firstDequeuingTask = Task.Run(() =>
            {
                foreach (var number in numbersBlockingCollection.GetConsumingEnumerable())
                {
                    Console.WriteLine($"{number} is Dequeued");
                }

            });
            await Task.WhenAll(firstEnqueuingTask, firstDequeuingTask);

        }
    }
}
