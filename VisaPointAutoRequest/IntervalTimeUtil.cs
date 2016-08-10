using System;
using System.IO;

namespace VisaPointAutoRequest
{
    class IntervalTimeUtil
    {
        #region Constants
        public const string IntervalFilePath = "tmp\\intervaltime.tmp";
        #endregion

        #region Fields/Properties
        public static bool IsResetTime = true;
        private static int _intervalTime = -1;
        #endregion

        #region Methods
        public static int IntervalTime
        {
            get
            {
                if (_intervalTime == -1)
                {
                    loadTimeFromFile();
                }
                return _intervalTime;
            }

            set
            {
                _intervalTime = value;
                saveIntervalTime();
            }
        }

        private static void loadTimeFromFile()
        {
            var intervalTime = -1;
            var isReset = false;

            // Create temp file path
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, IntervalFilePath);
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
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, IntervalFilePath);
            File.WriteAllText(filePath, _intervalTime.ToString());
        }
        #endregion
    }
}
