namespace Sitecore.SharedSource.Documenter.Views
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using global::Sitecore.SharedSource.Documenter.Models;

    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Simple properties are cleaner without separated lines.")]
    public interface IDocumenterView
    {
        string TemplateRootPath { get; set; }
        string RenderingRootPath { get; set; }
        string ImageWidths { get; set; }
        TemplateFolder Templates { get; set; }
        RenderingFolder Renderings { get; set; }
        IEnumerable<string> ErrorMessages { get; set; }
        void DataBind();
    }
}
