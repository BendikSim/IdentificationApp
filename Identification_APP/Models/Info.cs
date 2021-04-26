using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identification_APP.Models
{
    public class Info
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }

        public string grant_type { get; set; }
        public string scope { get; set; }

        public Info(string client_id, string client_secret, string grant_type, string scope)
        {
            this.client_id = client_id;
            this.client_secret = client_secret;
            this.grant_type = grant_type;
            this.scope = scope;
        }
    }
}
