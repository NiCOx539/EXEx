using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Net.Mime;
using System.Windows;

namespace screenshot
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 2)
                {
                    if ((args[0] == "-o") || (args[0] == "-O"))
                    {
                        // Start the process...
                        Bitmap memoryImage;
                        memoryImage = new Bitmap(1920, 1080);
                        Size s = new Size(memoryImage.Width, memoryImage.Height);

                        // Create graphics
                        Graphics memoryGraphics = Graphics.FromImage(memoryImage);

                        // Copy data from screen
                        memoryGraphics.CopyFromScreen(0, 0, 0, 0, s);

                        //That's it! Save the image in the directory and this will work like charm.
                        string str = "";
                        try
                        {
                            str = string.Format(args[1]);
                        }
                        catch (Exception er)
                        {
                        }

                        memoryImage.Save(str);
                        System.Windows.Forms.Application.Exit();
                    }

                }
            }
            catch (Exception er)
            {
                System.Windows.Forms.Application.Exit();
            }

        }
    }
}
