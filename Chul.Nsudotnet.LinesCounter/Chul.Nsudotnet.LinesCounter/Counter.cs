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
                if (String.IsNullOrEmpty(str))
                {
                    continue;
                }
                int endCommentIndex = str.IndexOf("*/", StringComparison.Ordinal);
                if (commentStarted)
                {
                    if (endCommentIndex != -1)
                    {
                        commentStarted = false;
                        str = str.Remove(0, endCommentIndex + 2);
                    }
                    else
                    {
                        continue;
                    }
                }
                str = str.Trim();
                int lineCommentIndex = str.IndexOf("//", StringComparison.Ordinal);
                if (lineCommentIndex != -1)
                {
                    if (lineCommentIndex == 0)
                    {
                        continue;
                    }
                    else
                    {
                        str = str.Substring(0, lineCommentIndex+1);
                    }
                }
               
                int startCommentIndex = str.IndexOf("/*", StringComparison.Ordinal);
                while (startCommentIndex != -1 && endCommentIndex != -1 && startCommentIndex<endCommentIndex)
                {
                    str = str.Remove(startCommentIndex, endCommentIndex - startCommentIndex + 2);
                    startCommentIndex = str.IndexOf("/*", StringComparison.Ordinal);
                    endCommentIndex = str.IndexOf("*/", StringComparison.Ordinal);
                }
                if (startCommentIndex != -1)
                {
                    commentStarted = true;
                    str = str.Substring(0, startCommentIndex);
                }
                if (!String.IsNullOrWhiteSpace(str))
                {
                    linesCount++;
                }
            }
            return linesCount;
        }

    }
}
