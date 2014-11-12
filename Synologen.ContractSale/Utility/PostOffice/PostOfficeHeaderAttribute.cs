namespace Spinit.Wpc.Synologen.Invoicing.PostOffice
{
    public class PostOfficeHeaderAttribute
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Render()
        {
            return string.Format("{0}=\"{1}\"", Key, Value);
        }
    }
}