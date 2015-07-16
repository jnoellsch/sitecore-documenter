namespace SitecoreDocumentor.Core.Models
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class TemplateItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public IEnumerable<FieldItem> Fields { get; set; }
        public IEnumerable<TemplateItem> InsertOptions { get; set; }
        public IEnumerable<TemplateItem> BaseTemplates { get; set; }
    }
}
