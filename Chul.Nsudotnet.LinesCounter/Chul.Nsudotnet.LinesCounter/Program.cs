using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chul.Nsudotnet.LinesCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
              Console.WriteLine("Help:");
              Console.WriteLine("program.exe type");
              Console.WriteLine("Example: myprogram.exe *.cs");
              return;
            }
            string filesType = args[0];
            Counter counter = new Counter();
            counter.CountLinesInDirectory(Directory.GetCurrentDirectory(),filesType);
            Console.WriteLine("Files {0}",counter.FilesCounter);
            Console.WriteLine("Lines {0}",counter.LinesCounter);
        }
    }
}
