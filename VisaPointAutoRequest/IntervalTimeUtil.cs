using System;
using System.IO;

namespace VisaPointAutoRequest
{
    class IntervalTimeUtil
    {
        #region Constants
        private const string INTERVAL_FILE_NAME = "intervaltime.tmp";
        #endregion

        #region Fields/Properties
        public static bool isResetTime = true;
        private static int _intervalTime = -1;
        #endregion

        #region Methods
        public static int GetIntervalTime()
        {
            if (_intervalTime == -1)
            {
                loadTimeFromFile();
            }
            return _intervalTime;
        }

        private static void loadTimeFromFile()
        {
            var intervalTime = -1;
            var isReset = false;

            // Create temp file path
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, INTERVAL_FILE_NAME);
            // File information
            var fileInfo = new FileInfo(filePath);
            // File string data
            string[] lines;

            // Check file exists
            if (!fileInfo.Exists)
            {
                isReset = true;
            }
            else
            {
                // Read all lines from temp file
                lines = File.ReadAllLines(filePath);

                // Check lines count
                // If equal 0 or greater than 1, set reset flag is "true"
                if (lines.Length == 0 || lines.Length > 1)
                {
                    isReset = true;
                }
                // Try parsing string data to integer
                else if (!Int32.TryParse(lines[0], out intervalTime))
                {
                    // If cannot parse, set reset flag is "true"
                    isReset = true;
                }
            }

            // Check reset flag
            if (isReset)
            {
                // If "true", write "-1" to temp file 
                File.WriteAllText(filePath, "-1");
                _intervalTime = -1;
            }
            else
            {
                _intervalTime = intervalTime;
            }
        }
        private static void saveIntervalTime()
        {
            var sFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, INTERVAL_FILE_NAME);
            File.WriteAllText(sFilePath, _intervalTime.ToString());
        }

        public static void SetIntervalTime(int intervalTime)
        {
            _intervalTime = intervalTime;
            saveIntervalTime();
        }
        #endregion
    }
}
