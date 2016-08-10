using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VisaPointAutoRequest
{
    class SocksLoaderUtil
    {
        public const string SocksFilePath = "conf\\socks.conf";
        public List<string> _sockList = new List<string>();
        private int _socksCount;
        private int _currentSock = 0;

        private static SocksLoaderUtil _instance;

        public static SocksLoaderUtil Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SocksLoaderUtil();
                }
                return _instance;
            }
        }

        public string NextSock
        {
            get
            {
                var sock = _sockList[_currentSock];
                _currentSock++;
                if (_currentSock == _socksCount)
                {
                    _currentSock = 0;
                }
                return sock;
            }
        }

        private SocksLoaderUtil()
        {
            loadSockList();
        }

        private void loadSockList()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SocksFilePath);
            if (!new FileInfo(filePath).Exists)
            {
                // TODO: Define new exception here
                throw new Exception();
            }

            var socks = File.ReadAllLines(filePath);
            _sockList = new List<string>(socks);
            _socksCount = _sockList.Count;
        }
    }
}
