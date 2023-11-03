using System;
using System.Collections.Generic;
using System.IO;
using TransportManagementCore.Models;

namespace TransportManagementCore
{

    public static class Global
    {

        public static string EnvironmentName;

        public static string EnvironmentShortName;

        public static Dictionary<string, string> DataTableQuery = new Dictionary<string, string>();

        public static UserInfo CurrentUser; 
        public static string MainApplication { get; set; }

        public static class FileServer
        {
            public static string DownloadPath { get; set; }
            public static string UploadPath { get; set; }
            public static string ReportingAppURL { get; set; }

            public static string GetPath(string Path)
            {
                string returnPath = string.Empty;
                if (Path == "")
                    return returnPath;

                var date = DateTime.Now.ToString("dd-MM-yyyy");
                var dd = date.Substring(0, 2);
                var mm = date.Substring(3, 2);
                var yyyy = date.Substring(6);
                var dateFormat = $"{dd}-{mm}-{yyyy}";
                string filepath = $"{Global.FileServer.UploadPath}{yyyy}" +
                                  $"{System.IO.Path.DirectorySeparatorChar}{mm}{System.IO.Path.DirectorySeparatorChar}" +
                                  $"{dateFormat}{System.IO.Path.DirectorySeparatorChar}{Path}";
                string directory = $"{System.IO.Path.GetDirectoryName(filepath)}";
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                returnPath = filepath;

                return returnPath;
            }
        }

        public static class Colors
        {
            public static readonly string ThemeColor = "#4ECDC4";
        }

    }




}
