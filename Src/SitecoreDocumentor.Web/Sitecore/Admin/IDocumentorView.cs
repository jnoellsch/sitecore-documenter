namespace SitecoreDocumentor.Web
{
    using System.Collections.Generic;
    using SitecoreDocumentor.Core.Models;

    public interface IDocumentorView
    {
        string TemplateRootPath { get; set; }
        string RenderingRootPath { get; set; }
        TemplateFolder Templates { get; set; }
        RenderingFolder Renderings { get; set; }
        void DataBind();
    }
}
