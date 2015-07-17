namespace SitecoreDocumentor.Core.Models
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class TemplateMetaItem : ModelBase
    {
        public string Description { get; set; }
        public string Icon { get; set; }
        public IEnumerable<FieldItem> Fields { get; set; }
        public IEnumerable<TemplateMetaItem> InsertOptions { get; set; }
        public IEnumerable<TemplateMetaItem> BaseTemplates { get; set; }
    }
}
