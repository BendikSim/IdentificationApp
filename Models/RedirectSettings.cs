namespace Models
{
    public class RedirectSettings
    {
        public string successUrl { get; set; }
        public string abortUrl { get; set; }
        public string errorUrl { get; set; }

        public RedirectSettings(string successUrl, string abortUrl, string errorUrl)
        {
            this.successUrl = successUrl;
            this.abortUrl = abortUrl;
            this.errorUrl = errorUrl;
        }
    }
}