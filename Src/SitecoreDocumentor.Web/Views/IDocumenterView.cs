namespace SitecoreDocumenter.Web.Views
{
    using System.Collections.Generic;
    using SitecoreDocumenter.Web.Models;

    public interface IDocumenterView
    {
        string TemplateRootPath { get; set; }
        string RenderingRootPath { get; set; }
        TemplateFolder Templates { get; set; }
        RenderingFolder Renderings { get; set; }
        IEnumerable<string> ErrorMessages { get; set; }
        void DataBind();
    }
}
