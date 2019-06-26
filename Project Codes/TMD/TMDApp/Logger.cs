using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDApp
{
    public class Logger
    {
        public string logPath { get; set; }

        public Logger(string filename)
        {
            //by default, log file is created in the same folder as the exe
            logPath = AppDomain.CurrentDomain.BaseDirectory + filename;
        }

        public Logger(string path, string filename)
        {
            logPath = path + Path.DirectorySeparatorChar.ToString() + filename;
        }

        public void log(string msg)
        {
            if (!File.Exists(logPath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(logPath)) sw.WriteLine(DateTime.Now.ToString() + "\t" + msg);
            }
            else
            {
                using (StreamWriter sw = File.AppendText(logPath)) sw.WriteLine(DateTime.Now.ToString() + "\t" + msg);
            }
        }

        public void log(Exception ex)
        {
            log(ex.Message + Environment.NewLine + ex.StackTrace);
        }
    }
}
