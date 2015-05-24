using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chul.Nsudotnet.LinesCounter
{
    class Counter
    {
        public int LinesCounter = 0;
        public int FilesCounter = 0;

        public void CountLinesInDirectory(string directory, string fileType)
        {
            string[] directories = Directory.GetDirectories(directory);
            foreach (string dir in directories)
            {
                CountLinesInDirectory(dir,fileType);
            }

            string[] files = Directory.GetFiles(directory, fileType);
            foreach (string file in files)
            {
                StreamReader reader = new StreamReader(file);
                try
                {
                    LinesCounter += CountLinesInFile(reader);
                    FilesCounter++;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in file");
                }
                finally
                {
                    reader.Dispose();
                }
            }
        }

        public int CountLinesInFile(StreamReader fileReader)
        {
            string str;
            int linesCount = 0;
            bool commentStarted = false;
            while ((str = fileReader.ReadLine()) != null)
            {
                if (String.IsNullOrWhiteSpace(str))
                {
                    continue;
                }
                bool lineToCount = false;
                bool previousSlash = false;
                bool previousStar = false;
                for (int i = 0; i < str.Length; i++)
                {
                    if (commentStarted)
                    {
                        if (previousStar)
                        {
                            if (str[i] == '/')
                            {
                                previousStar = false;
                                commentStarted = false;
                                continue;
                            }
                            else
                            {
                                previousStar = false;
                            }
                        }

                        if (str[i] == '*')
                        {
                            previousStar = true;
                        }
                        continue;
                    }

                    if (previousSlash)
                    {
                        if (str[i] == '/')
                        {
                            break;
                        }
                        if (str[i] == '*')
                        {
                            commentStarted = true;
                            previousSlash = false;
                            continue;
                        }
                        lineToCount = true;
                    }

                    previousSlash = false;

                    if (str[i] == '/')
                    {
                        previousSlash = true;
                        continue;
                    }
                    if (!lineToCount)
                    {
                        if (str[i] != ' ' && str[i] != '\t')
                        {
                            lineToCount = true;
                        }
                    }
                }
                if (lineToCount)
                {
                    linesCount++;
                }
            }
            return linesCount;
        }

    }
}
