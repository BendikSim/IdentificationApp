using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identification_APP.Models
{
    public class Info
    {
        public string flow { get; set; }
        public string[] allowedProviders { get; set; }

        public List<string> include { get; set; }
        public string redirectSettings { get; set; }

        public Info(string flow, List<string> include, string[] allowedProviders, string redirectSettings)
        {
            this.flow = flow;
            this.include = include;
            this.allowedProviders = allowedProviders;
            this.redirectSettings = redirectSettings;
        }
    }
}
