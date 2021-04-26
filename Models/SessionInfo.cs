using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class SessionInfo
    {
        public string url { get; set; }
        public string id { get; set; }

        public SessionInfo(string access_token, string id)
        {
            this.url = url;
            this.id = id;
        }
    }
}
