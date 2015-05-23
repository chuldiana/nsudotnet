using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chul.Nsudotnet.NumberGuesser
{
    class Program
    {
        private static string[] _losePhrases =
        {
            "{0} - looser",
            "HA HA HA, {0}",
            "It' happends, {0}"
        };
        private static Random _random = new Random();
        private static List<string> _history = new List<string>(1000);
        private static DateTime _start;
        private static DateTime _end;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter you name");
            string name = Console.ReadLine();

            int numberToGuess = _random.Next(100);
            bool isWin = false;
            _start = DateTime.Now;
            while (true)
            {
                Console.WriteLine("Enter number from 0 to 100");
                string numberStr = Console.ReadLine();
                int number;
                if (numberStr == "q")
                {
                    break;
                }
                try
                {
                    number = int.Parse(numberStr);
                }
                catch (Exception)
                {
                    Console.WriteLine("Parse int error");
                    continue;
                }
                if (number == numberToGuess)
                {
                    _end = DateTime.Now;
                    isWin = true;
                    break;
                }

                if (number > numberToGuess)
                {
                    Console.WriteLine("Less");
                    _history.Add(String.Format("{0} Less",number));
                }
                else
                {
                    Console.WriteLine("More");
                    _history.Add(String.Format("{0} More", number));
                }

                if (_history.Count%4 == 0)
                {
                    Console.WriteLine(_losePhrases[_random.Next(_losePhrases.Length)],name);
                }

            }
            if (!isWin)
            {
                Console.WriteLine("Good bye");
            }
            else
            {
                Console.WriteLine("History count: {0}",_history.Count);
                foreach (string str in _history)
                {
                    Console.WriteLine(str);
                }
                TimeSpan time= _end - _start;
                Console.WriteLine("Minutes: {0}",time.Minutes);
                Console.ReadLine();
            }
        }
    }
}
