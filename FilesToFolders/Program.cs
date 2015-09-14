// Written by Alexander Jurk 2015 under GPL v3

using System;
using System.Collections.Generic;
using System.IO;

namespace FilesToFolders
{
    class Program
    {
        /// <summary>
        /// Main function.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>Error code.</returns>
        static int Main(string[] args)
        {
            // Initialization of standart values for arguments
            string path = AppDomain.CurrentDomain.BaseDirectory;    // Use execution directory as default path
            DirectoryInfo dir = new DirectoryInfo(args[0]);
            long folderSize = 100;    // 100 files per folder by default

            // Parsing command line
            if (args.Length > 0)
            {
                // Check if first argument is valid and existing path
                try
                {
                    dir = new DirectoryInfo(args[0]);
                    if (!dir.Exists)
                    {
                        Console.WriteLine("Error: Invalid path!");
                        return 1;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return -1;
                }

                // Check if second argument exists and is a valid number
                if (args.Length > 1 && args[1] != null && !long.TryParse(args[1], out folderSize))
                {
                    Console.WriteLine("Error: Invalid folder size!");
                    return 1;
                }
            }
            
            List<FileInfo> files = new List<FileInfo>(dir.GetFiles());
            long folderNum = (long)Math.Ceiling(files.Count / (double)folderSize);    // Needed amount of folders to fit all files in path with given folderSize

            Console.Write("Path: " + path + "\nFodler size: " + folderSize.ToString() + "\nFile count: " + files.Count.ToString() + "\n");

            // Create numbered (leading zeros) subdirectories and move amount of files into them equal to folderSize or remaining rest
            for (long i = 1; i <= folderNum; i++)
            {
                string dirName = i.ToString("D" + folderNum.ToString().Length.ToString());
                dir.CreateSubdirectory(dirName);

                for (long j = 0; j < folderSize && files.Count > 0; j++)
                {
                    File.Move(files[0].FullName, Path.Combine(new string[] { path, dirName, files[0].Name }));
                    files.RemoveAt(0);
                }
            }

            return (0);
        }
    }
}
