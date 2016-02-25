namespace Sitecore.SharedSource.Documenter.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Simple properties are cleaner without separated lines.")]
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
