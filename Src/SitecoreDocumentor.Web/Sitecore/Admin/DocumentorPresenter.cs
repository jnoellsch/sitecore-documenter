namespace SitecoreDocumentor.Web
{
    using System;

    public class DocumentorPresenter
    {
        private readonly IDocumentorView _view;

        private readonly DocumentorService _service;

        public DocumentorPresenter(IDocumentorView view, DocumentorService service)
        {
            if (view == null) throw new ArgumentNullException("view");
            this._view = view;
            this._service = service;
        }

        public void DataBind()
        {
            this._view.DataBind();
        }

        public void LoadData()
        {
            this._view.Renderings = this._service.GetRenderings(this._view.RenderingRootPath);
            this._view.Templates = this._service.GetTemplates(this._view.TemplateRootPath);
            this._view.DataBind();
        }
    }
}