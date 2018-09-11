namespace IntegrationContracts.Events
{
    public class DocumentPageExtracted
    {
        public string PageLocation { get; set; }
        public string PageName { get; set; }
        public byte[] Page { get; set; }
        public DocumentPageExtracted(string pageLocation, string pageName, byte[] page)
        {
            this.PageName = pageName;
            this.Page = page;

            this.PageLocation = pageLocation;
        }
    }
}
