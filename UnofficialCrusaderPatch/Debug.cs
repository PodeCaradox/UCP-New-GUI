﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;

namespace UCP
{
    public static class Debug
    {
        public static void Show(object message, object title = null)
        {
            MessageBox.Show(message == null ? "" : message.ToString(), title == null ? "Info" : title.ToString());
        }

        public static void Error(string message)
        {
            
            Show(message, "Error");
        }

        public static void Error(Exception e)
        {
            string message = e.ToString();
            File.AppendAllText("icp_error_dump.txt", message + "\n\n\n");
            Error(message);
        }
    }
}
