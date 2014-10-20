namespace Spinit.Wpc.Synologen.Core.Domain.Model.Yammer
{
    public class ReferenceModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string normalized_name { get; set; }
        public string permalink { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string web_url { get; set; }
        public string full_name { get; set; }
        public string job_title { get; set; }
        public string mugshot_url { get; set; }
        public string state { get; set; }
        public StatsModel stats { get; set; }
    }
}
