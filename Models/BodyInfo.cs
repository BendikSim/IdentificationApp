using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class BodyInfo
    {
        public string flow { get; set; }
        public List<string> allowedProviders { get; set; }
        public List<string> include { get; set; }
        public RedirectSettings redirectSettings { get; set; }

        public BodyInfo(string flow, List<string> allowedProviders, List<string> include, RedirectSettings redirectSettings)
        {
            this.flow = flow;
            this.allowedProviders = allowedProviders;
            this.include = include;
            this.redirectSettings = redirectSettings;
        }
    }
}
