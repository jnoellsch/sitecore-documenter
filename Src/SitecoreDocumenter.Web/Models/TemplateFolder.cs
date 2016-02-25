namespace Sitecore.SharedSource.Documenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Simple properties are cleaner without separated lines.")]
    public class TemplateFolder : ModelBase
    {
        public TemplateFolder()
        {
            this.Folders = new List<TemplateFolder>();
            this.Templates = new List<TemplateMetaItem>();
        }

        public IList<TemplateFolder> Folders { get; set; }
        public IList<TemplateMetaItem> Templates { get; set; }
    }
}
