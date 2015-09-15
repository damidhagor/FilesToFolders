// Written by Alexander Jurk 2015 under GPL v3

using System;
using System.Collections.Generic;
using System.IO;

namespace FilesToFolders
{
    class Program
    {
        static int Main(string[] args)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;    // Use execution directory as default path
            long folderSize = 100;    // 100 files per folder by default

            // Parsing command line
            if (args.Length > 0)
            {
                string temp_path = args[0];

                // Check if last char is '"' and remove it
                // With whitespaces in paths, Powershell for some reason leaves the '"' at the path's end
                // IO functions fail in this case because '"' is an illegal char for a path
                if (temp_path[temp_path.Length - 1] == '"')
                    temp_path = temp_path.Remove(temp_path.Length - 1);

                // Check if first argument is valid and existing path
                try
                {
                    if (!Directory.Exists(temp_path))
                    {
                        Console.WriteLine("Error: Invalid path!");
                        Console.WriteLine(temp_path);
                        return 1;
                    }
                    path = Path.GetFullPath(temp_path);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(temp_path);
                    return -1;
                }

                // Check if second argument exists and is a valid number
                if (args.Length > 1 && args[1] != null && !long.TryParse(args[1], out folderSize))
                {
                    Console.WriteLine("Error: Invalid folder size!");
                    return 1;
                }
            }

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            List<FileInfo> files = new List<FileInfo>(dirInfo.GetFiles());
            long folderNum = (long)Math.Ceiling(files.Count / (double)folderSize);    // Needed amount of folders to fit all files in path with given folderSize

            Console.Write("Path: " + path + "\nFolder size: " + folderSize.ToString() + "\nFile count: " + files.Count.ToString() + "\n");

            // Create numbered (leading zeros) subdirectories and move amount of files into them equal to folderSize or remaining rest
            for (long i = 1; i <= folderNum; i++)
            {
                string dirName = i.ToString("D" + folderNum.ToString().Length.ToString());
                Directory.CreateDirectory(Path.Combine(path, dirName));

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
