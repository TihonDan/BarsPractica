using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace barsTest
{
    class Program
    {
       
        //
        public static void Main()   
        {
            DummyRequestHandler dummy = new DummyRequestHandler();
            Console.WriteLine("Хотите начать: Да/Нет");
            var contains = new Dictionary<string, string>();
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
                ThreadPool.QueueUserWorkItem((o) => { dummy.HandleRequest(mess, massArg); } );


            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Программа завершена");
        }

        public interface IRequestHandler
        {
            string HandleRequest(string message, string[] arguments);
        }

        public class DummyRequestHandler : IRequestHandler
        {
            public string nb { get; set; }
            public string nb1 { get; set; }

            public Dictionary<string, string> cont = new Dictionary<string, string>();
            public string HandleRequest(string message, string[] arguments)
            {
                nb = Guid.NewGuid().ToString("D");
                nb1 = Guid.NewGuid().ToString("D");

                if(!message.Contains("упади")) cont.Add(nb, message);

                try
                {
                    Thread.Sleep(1000);
                    if (message.Contains("упади"))
                    {
                        throw new Exception("Я упал так как ты и просил");
                    }
                }
                catch (Exception ex)
                {
                    foreach (var temp in cont)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Сообщение {temp.Key} получило ответ {nb1}");
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Сообщение {nb} упало с ошибкой {ex.Message}");
                }
                return nb;
            }
        }
    }
}
