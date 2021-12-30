namespace _5ERAT11.Models
{
    public class Browser
    {
        public string BrowserName { get; set; }
        public string BrowserVersion { get; set; }
        public Browser(string browser, string browserVersiom)
        {
            BrowserName = browser;
            BrowserVersion = browserVersiom;
        }

        public Browser() { }
        public override string ToString()
        {
            return $"Browser[browser = {BrowserName}, version = {BrowserVersion}]";
        }

        public override bool Equals(object obj)
        {
            return obj is Browser browser && browser.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return BrowserName.GetHashCode() + BrowserVersion.GetHashCode();
        }
    }
}