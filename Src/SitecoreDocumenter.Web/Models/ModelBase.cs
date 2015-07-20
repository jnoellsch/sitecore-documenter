namespace SitecoreDocumenter.Web.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Simple properties are cleaner without separated lines.")]
    public class ModelBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
