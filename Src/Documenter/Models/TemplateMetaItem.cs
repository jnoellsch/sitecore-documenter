namespace Sitecore.SharedSource.Documenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Simple properties are cleaner without separated lines.")]
    public class TemplateMetaItem : ModelBase
    {
        public TemplateMetaItem()
        {
            this.Fields = new List<FieldItem>();
            this.InsertOptions = new List<TemplateMetaItem>();
            this.BaseTemplates = new List<TemplateMetaItem>();
        }

        public string Description { get; set; }
        public string Icon { get; set; }
        public IList<FieldItem> Fields { get; set; }
        public IList<TemplateMetaItem> InsertOptions { get; set; }
        public IList<TemplateMetaItem> BaseTemplates { get; set; }
    }
}
