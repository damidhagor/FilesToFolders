# FilesToFolders

This is a little commandline-tool which puts files of a folder into numbered sub directories.

The user can specify 2 values:
1) Path: The in which the program should work. (Default: The program's folder)
2) Foldersize: The amount of files each of the sub directories will contain. (Default: 100)

The program creates the needed amount of sub directories to fit all of the folder's files.
Those directories are numbered beginning with 1 and contain trailing 0s if needed.

Commandline format: "FilesToFolders.exe path\to\folder <foldersizeasnumber>"
The parameters are optional and are set to default if not specified.

Example:
Folder "C:\Users\<user>\Pictures\<somefolder>\" contains 236 images and a foldersize of 50 is specified.
The programs creates 5 folders each containing 100 of the images with the 5th containing the remaining 36.
Commandline: "> path\to\executable\FilesToFolders.exe "C:\Users\<user>\Pictures\<somefolder>\" 50"
