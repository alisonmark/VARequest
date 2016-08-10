using System;
using System.Text;
using System.IO;

namespace VisaPointAutoRequest
{
    class IOUtil
    {

        public static string GetWorkingPath(string relativePath)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        }

        public static void CreateDirectory(string path, bool isRelative)
        {
            if(isRelative)
            {
                path = GetWorkingPath(path);
            }

            var directory = new DirectoryInfo(path);
            if(!directory.Exists)
            {
                directory.Create();
            }
        }
    }
}
