using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chul.Nsudotnet.Calendar
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("Enter date:");
            string dateString = Console.ReadLine();
            DateTime date;

            if (!DateTime.TryParse(dateString, out date))
            {
                Console.WriteLine("Incorrect date");
                return;
            }
            DateTime start = date.AddDays(-((int) date.Day + (int) date.DayOfWeek) + 2);
            for (int i = 0; i < 7; i++)
            {
                if ((int) start.DayOfWeek == 0 || (int) start.DayOfWeek == 6)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.Write(start.ToString("ddd"));
                Console.Write(" ");
                start = start.AddDays(1);
                Console.ResetColor();
            }
            Console.WriteLine();


            start = start.AddDays(-7);
            int work = 0;
            while (start.Month <= date.Month)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (start.Month != date.Month)
                    {
                        Console.Write("   ");
                    }
                    else
                    {
                        if ((int) start.DayOfWeek == 0 || (int) start.DayOfWeek == 6)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else
                        {
                            work++;
                        }

                        if (date == start)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                        }
                        if (start == DateTime.Today)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                        }
                        Console.Write(start.Day.ToString("D2"));
                        Console.ResetColor();
                        Console.Write(" ");
                       
                    }
                    start = start.AddDays(1);
                }
                Console.WriteLine();
            }
            Console.Write("work days ");
            Console.WriteLine(work);
            Console.ReadLine();
        }
    }
}
