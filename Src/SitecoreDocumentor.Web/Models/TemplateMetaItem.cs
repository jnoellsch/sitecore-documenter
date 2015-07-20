namespace SitecoreDocumentor.Web.Models
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class TemplateMetaItem : ModelBase
    {
        public string Description { get; set; }
        public string Icon { get; set; }
        public IList<FieldItem> Fields { get; set; }
        public IList<TemplateMetaItem> InsertOptions { get; set; }
        public IList<TemplateMetaItem> BaseTemplates { get; set; }
    }
}
