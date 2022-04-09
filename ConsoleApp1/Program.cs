using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var contains = new Dictionary<string, string>();

            while (true)
            {
                Console.WriteLine("Введите элемент");
                string key = Console.ReadLine();
                Console.WriteLine("Enter Value");
                string value = Console.ReadLine();

                contains.Add(key, value);

                Console.WriteLine("Ещё ?");
                string end = Console.ReadLine();
                if (end.Contains("Да")) break;
            }

            foreach (var item in contains) Console.Write($"Key: {item.Key}, Value {item.Value}");
        }
    }
}
