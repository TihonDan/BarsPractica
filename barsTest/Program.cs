using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace barsTest
{
    class Program
    {
        public static void Main()   
        {
            SomeClass cl = new SomeClass();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Хотите начать: Да/Нет");
            var command = Console.ReadLine();

            while (command != "Нет")
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine('\n' + "Введите текст запроса для отправки. Если хотите выйти напишите: Да");
                var mess = Console.ReadLine();
                if (mess.Contains("Да")) break;

                string[] massArg = new string[10];

                for (int i  = 0; i < massArg.Length; i++)
                {
                    Console.WriteLine("Введите аргумент. Если хотите закночить введите: Да");
                    var Args = Console.ReadLine();
                    if (Args.Contains("Да")) break;
                    else
                    {
                        massArg[i] = Args;
                        continue;
                    }
                }

                //Console.ForegroundColor = ConsoleColor.Cyan;
                //Console.Write($"Было отправлено сообщение: {mess}. Присвоено идентификатор: {pr.SomeMethod(mess, massArg)}");
                ThreadPool.QueueUserWorkItem((o) => { cl.SomeMethod(mess, massArg); } );


            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Программа завершена");
        }
    }
    public interface IRequestHandler
    {
        string HandleRequest(string message, string[] arguments);
    }

    public class DummyRequestHandler : IRequestHandler
    {
        public string HandleRequest(string message, string[] arguments)
        {
            Thread.Sleep(10_000);
            if (message.Contains("упади"))
            {
                throw new Exception("Я упал, как сам просил");
            }
            return Guid.NewGuid().ToString("D");
        }
    }
    public class SomeClass
    {
        string nb { get; set; }
        string nb1 { get; set; }

        Dictionary<string, string> cont = new Dictionary<string, string>();
        List<string> keys = new List<string>();

        public void SomeMethod(string mess, string[] args)
        {
            DummyRequestHandler dummy = new DummyRequestHandler();

            nb = Guid.NewGuid().ToString("D");
            nb1 = Guid.NewGuid().ToString("D");

            if (!mess.Contains("упади"))
            {
                cont.Add(nb1, mess);
                keys.Add(nb);
            }

            try
            {
                dummy.HandleRequest(mess, args);
            }
            catch (Exception ex)
            {
                foreach (var temp in cont)
                {
                    foreach(var _key in keys)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Сообщение {temp.Key} получило ответ {_key}");
                        break;
                    }
                    continue;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Сообщение: {nb} упало с ошибкой: {ex.Message}");
            }

        }
    }
}
