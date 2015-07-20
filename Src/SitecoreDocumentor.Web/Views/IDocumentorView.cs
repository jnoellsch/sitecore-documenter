namespace SitecoreDocumentor.Web.Views
{
    using System.Collections.Generic;
    using SitecoreDocumentor.Web.Models;

    public interface IDocumentorView
    {
        string TemplateRootPath { get; set; }
        string RenderingRootPath { get; set; }
        TemplateFolder Templates { get; set; }
        RenderingFolder Renderings { get; set; }
        IEnumerable<string> ErrorMessages { get; set; }
        void DataBind();
    }
}
