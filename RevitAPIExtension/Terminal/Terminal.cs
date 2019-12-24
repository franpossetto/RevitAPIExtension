using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPIExtension
{
    public static class Terminal
    {
        public static string Location = @"C:\PyRevit\PyRevitScaffold";
        public static bool Install() 
        {
            try
            {
                string path = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User);
                string new_path = path + ";" + Location;
                Environment.SetEnvironmentVariable("Path", new_path, EnvironmentVariableTarget.User);
                return true;
            } catch(Exception e)
            {
                return false;
            }
            
        }

        public static bool IsInstall()
        {
            string path = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User);
            List<string> paths = path.Split(';').ToList();
            if (paths.Any(x => x == Location))
                return true;
            else
                return false;
        }
    }
}
