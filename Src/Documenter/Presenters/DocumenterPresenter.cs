namespace Sitecore.SharedSource.Documenter.Presenters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Sitecore.SharedSource.Documenter.Services;
    using global::Sitecore.SharedSource.Documenter.Views;

    public class DocumenterPresenter
    {
        private readonly IDocumenterView _view;

        private readonly DocumenterService _service;

        public DocumenterPresenter(IDocumenterView view, DocumenterService service)
        {
            if (view == null) throw new ArgumentNullException("view");
            if (service == null) throw new ArgumentNullException("service");

            this._view = view;
            this._service = service;
        }

        public void LoadData()
        {
            // validate paths
            var errors = this.RunValidators();
            if (errors.Any())
            {
                this._view.ErrorMessages = errors;
                this._view.DataBind();
                return;
            }

            // good? get full data
            this._view.Renderings = this._service.GetRenderings(this._view.RenderingRootPath);
            this._view.Templates = this._service.GetTemplates(this._view.TemplateRootPath);
            this._view.DataBind();
        }

        private IList<string> RunValidators()
        {
            var msgs = new List<string>();

            if (!this._service.IsValidItem(this._view.RenderingRootPath))
            {
                msgs.Add("Rendering start item could not be found. Is the path or ID correct?");
            }

            if (!this._service.IsValidItem(this._view.TemplateRootPath))
            {
                msgs.Add("Template start item could not be found. Is the path or ID correct?");
            }

            int num;
            if (!int.TryParse(this._view.ImageWidths, out num))
            {
                msgs.Add("Image widths is not a valid number");
            }
            else if (num <= 0)
            {
                msgs.Add("Image widths must be greater than 0.");
            }

            return msgs;
        }
    }
}