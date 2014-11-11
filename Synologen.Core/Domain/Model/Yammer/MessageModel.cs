namespace Spinit.Wpc.Synologen.Core.Domain.Model.Yammer
{
    public class MessageModel
    {
        public AttachmentModel[] attachments { get; set; }
        public BodyModel body { get; set; }
        public string client_type { get; set; }
        public string client_url { get; set; }
        public string created_at { get; set; }
        public bool direct_message { get; set; }
        public int id { get; set; }
        public string message_type { get; set; }
        public LikedByModel liked_by { get; set; }
        public int network_id { get; set; }
        public string privacy { get; set; }
        public int? replied_to_id { get; set; }
        public int sender_id { get; set; }
        public string sender_type { get; set; }
        public bool system_message { get; set; }
        public int thread_id { get; set; }
        public string url { get; set; }
        public string web_url { get; set; }
    }
}
