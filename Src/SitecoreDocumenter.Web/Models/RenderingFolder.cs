namespace Sitecore.SharedSource.Documenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Simple properties are cleaner without separated lines.")]
    public class RenderingFolder : ModelBase
    {
        public RenderingFolder()
        {
            this.Folders = new List<RenderingFolder>();
            this.Renderings = new List<RenderingMetaItem>();
        }

        public IList<RenderingFolder> Folders { get; set; }
        public IList<RenderingMetaItem> Renderings { get; set; }
    }
}