using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Sys = Cosmos.System;

namespace CorntoloOS
{
    public class Kernel : Sys.Kernel
    {
        Sys.FileSystem.CosmosVFS fs;
        string currentdirectory = @"0:\";
        protected override void BeforeRun()
        {
            fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            System.Console.ForegroundColor = System.ConsoleColor.DarkGreen;
            System.Console.Clear();
            System.Console.Beep();
            System.Console.WriteLine("CorntoloOS 1.1");
            System.Console.WriteLine("");
            System.Console.Beep();
            System.Console.ForegroundColor = System.ConsoleColor.Magenta;
            System.Console.WriteLine("[Main Process] Kernel.cs inited!");
            System.Console.Beep();
            System.Console.WriteLine("[Main Process] FileSystem inited!");
            System.Console.Beep();
            System.Console.WriteLine("[Main Process] System inited!");
            System.Console.Beep();
            System.Console.ForegroundColor = System.ConsoleColor.White;
            System.Console.WriteLine("");
            System.Console.WriteLine("");
            System.Console.WriteLine("==========");
            System.Console.WriteLine("CorntoloOS");
            System.Console.WriteLine("==========");
            System.Console.WriteLine("(c) matveymayner 2024-2025"); //копирайт понял?! Отведайте Этих Французких Булочек Да выпийте чаю!
        }

        protected override void Run()
        {
            System.Console.Write(currentdirectory + "@");
            string filename = "";
            string dirname = "";
            var input = System.Console.ReadLine();
            //System.Console.WriteLine(input); //Спустя 5 квантилионов лет я думал и выпилил тебя!
            switch (input)
            {
                default:
                    System.Console.ForegroundColor = System.ConsoleColor.Red;
                    System.Console.WriteLine("Command: " + input + " Not Found");
                    System.Console.ForegroundColor = System.ConsoleColor.White;
                    break;
                case "help":
                    System.Console.WriteLine(" ");
                    System.Console.WriteLine(" ");
                    System.Console.WriteLine("1. help");
                    System.Console.WriteLine("2. shutdown >> poweroff computer");
                    System.Console.WriteLine("3. reboot >> rebooting system");
                    System.Console.WriteLine("4. ld >> list directory");
                    System.Console.WriteLine("5. cls >> clear screen");
                    System.Console.WriteLine("6. neofetch >> info of computer & colored system logo");
                    System.Console.WriteLine("7. crtfile >> create file in root");
                    System.Console.WriteLine("8. mkdir >> create directory in root");
                    System.Console.WriteLine("9. delfile >> removing file");
                    System.Console.WriteLine("10. deldir >> removing directory");
                    System.Console.WriteLine("11. cd >> move to directoy");
                    System.Console.WriteLine("12. time >> your time");
                    System.Console.WriteLine("13. beep >> computer beep");
                    System.Console.WriteLine(" ");
                    System.Console.WriteLine(" ");
                    break;

                case "cls":
                    System.Console.Clear();
                    System.Console.WriteLine("==========");
                    System.Console.WriteLine("CorntoloOS");
                    System.Console.WriteLine("==========");
                    System.Console.WriteLine("(c) matveymayner 2024-2025");
                    break;

                case "shutdown":
                    Cosmos.System.Power.Shutdown();
                    break;

                case "reboot":
                    Cosmos.System.Power.Reboot();
                    break;

                case "neofetch":
                    System.Console.Clear();
                    System.Console.WriteLine("==========");
                    System.Console.WriteLine("CorntoloOS");
                    System.Console.WriteLine("==========");
                    System.Console.WriteLine("(c) matveymayner 2024-2025");
                    System.Console.WriteLine(" ");
                    System.Console.ForegroundColor = System.ConsoleColor.DarkGreen;
                    System.Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                    System.Console.WriteLine("@@@@@@@@@@");
                    System.Console.WriteLine("@@@@@@@@@@");
                    System.Console.WriteLine("@@@@@@@@@@");
                    System.Console.WriteLine("@@@@@@@@@@      CorntoloOS");
                    System.Console.WriteLine("@@@@@@@@@@");
                    System.Console.WriteLine("@@@@@@@@@@");
                    System.Console.WriteLine("@@@@@@@@@@");
                    System.Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                    System.Console.ForegroundColor = System.ConsoleColor.White;
                    System.Console.WriteLine("OS: CorntoloOS 1.1");
                    System.Console.WriteLine("Host: Unknown");
                    System.Console.WriteLine("Kernel: CosmosOS Dev Kit 20241102");
                    System.Console.WriteLine("Builded: 17.12.2024");
                    string cpuBrand = Cosmos.Core.CPU.GetCPUBrandString();
                    string cpuVendor = Cosmos.Core.CPU.GetCPUVendorName();
                    uint ramInMb = Cosmos.Core.CPU.GetAmountOfRAM();
                    ulong avabile_of_ram = Cosmos.Core.GCImplementation.GetAvailableRAM();
                    System.Console.WriteLine($"CPU: {cpuBrand}\nCPU Vendor: {cpuVendor}\nAmount of RAM: {ramInMb} MB \nAvabile of RAM: {avabile_of_ram}"); // Я ТЕБЯ СЕЙЧАС ВЫПИЛЮ ГОВНО! РАБОТАЙ!
                    System.Console.WriteLine(" ");
                    break;

                //filesystem - Файловая Система
                case "ld":
                    try
                    {
                        var directory_list = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(currentdirectory);
                        foreach (var directoryEntry in directory_list)
                        {
                            try
                            {
                                var entry_type = directoryEntry.mEntryType;
                                if (entry_type == Sys.FileSystem.Listing.DirectoryEntryTypeEnum.File)
                                {
                                    System.Console.ForegroundColor = ConsoleColor.Magenta;
                                    System.Console.WriteLine("| <File>       " + directoryEntry.mName);
                                    System.Console.ForegroundColor = ConsoleColor.White;
                                }
                                if (entry_type == Sys.FileSystem.Listing.DirectoryEntryTypeEnum.Directory)
                                {
                                    System.Console.ForegroundColor = ConsoleColor.Blue;
                                    System.Console.WriteLine("| <Directory>      " + directoryEntry.mName);
                                    System.Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                            catch (Exception e)
                            {
                                System.Console.WriteLine("Error: Directory not found");
                                System.Console.WriteLine(e.ToString());
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.ToString());
                        break;
                    }
                    break;

                case "crtfile":
                    filename = System.Console.ReadLine();
                    fs.CreateFile(currentdirectory + filename);
                    System.Console.WriteLine("File " + filename + " Created!");
                    break;

                case "mkdir":
                    dirname = System.Console.ReadLine();
                    fs.CreateDirectory(currentdirectory + dirname);
                    System.Console.WriteLine("Directory " + dirname + " Created!");
                    break;

                case "delfile":
                    try
                    {
                        filename = System.Console.ReadLine();
                        Sys.FileSystem.VFS.VFSManager.DeleteFile(currentdirectory + filename);
                        System.Console.WriteLine("File " + filename + " Deleted!");
                        break;
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine("Error: File not found");
                        System.Console.WriteLine(e.ToString());
                    }
                    break;

                case "deldir":
                    try
                    {
                        dirname = System.Console.ReadLine();
                        Sys.FileSystem.VFS.VFSManager.DeleteFile(currentdirectory + dirname);
                        System.Console.WriteLine("Directory " + dirname + " Deleted!");
                        break;
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine("Error: Directory not found");
                        System.Console.WriteLine(e.ToString());
                    }
                    break;

                case "cd":
                    currentdirectory = System.Console.ReadLine();
                break;

                case "time":
                    System.Console.WriteLine(DateTime.Now);
                break;

                case "beep":
                    System.Console.Beep();
                break;
            }
        }
    }
}
