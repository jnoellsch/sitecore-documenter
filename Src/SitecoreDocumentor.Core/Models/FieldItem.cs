namespace SitecoreDocumentor.Core.Models
{
    using System;

    [Serializable]
    public class FieldItem
    {
        public string Name { get; set; }
        public string SectionName { get; set; }
        public string FieldType { get; set; }
        public string DataSource { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public bool IsRequired { get; set; }
    }
}
