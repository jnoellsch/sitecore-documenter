namespace SitecoreDocumenter.Web.Models
{
    using System;

    [Serializable]
    public class FieldItem : ModelBase
    {
        public SectionItem Section { get; set; }
        public string FieldType { get; set; }
        public string Source { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public bool IsRequired { get; set; }
    }
}
