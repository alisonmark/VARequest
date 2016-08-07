using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VisaPointAutoRequest
{
    class Socks
    {
        public String host { get; set; }
        public int port { get; set; }
        public WebProxy myProxy { get; set; }
        public bool isSocks { get; set; }
        private String message { get; set; }
        public Socks()
        {
            return;
        }

        public Socks(String host, int port)
        {
            this.host = host;
            this.port = port;
        }

        public Socks(String stringSocks, bool isSocks)
        {
            this.isSocks = isSocks;
            this.parseSocks(stringSocks);
        }

        private void parseSocks(String stringSocks)
        {
            try
            {
                

                String[] part = stringSocks.Split(':');
                this.host = part[0];
                this.port = int.Parse(part[1]);

                Uri newUri = new Uri("http://"+ stringSocks);
                // Associate the newUri object to 'myProxy' object so that new myProxy settings can be set.
                myProxy.Address = newUri;
                // Create a NetworkCredential object and associate it with the 
                // Proxy property of request object.
                myProxy.Credentials = new NetworkCredential("lanlan451@gmail.com", "vArDF2ssvkpU");

            }
            catch (Exception e)
            {
                this.message = e.Message;
            }
        }
    }
}
