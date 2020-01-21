﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LarsWM
{
    class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Debug.WriteLine("Application started");

            //Application.Run();

            new WindowManager();
            // TODO: Create a workspace and assign a workspace to each connected display
        }
    }
}