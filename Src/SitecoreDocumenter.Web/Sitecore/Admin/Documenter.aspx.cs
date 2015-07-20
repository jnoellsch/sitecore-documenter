namespace SitecoreDocumenter.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.sitecore.admin;
    using SitecoreDocumenter.Web.Models;
    using SitecoreDocumenter.Web.Presenters;
    using SitecoreDocumenter.Web.Services;
    using SitecoreDocumenter.Web.Views;

    public partial class Documenter : AdminPage, IDocumenterView
    {
        private DocumenterPresenter _presenter;

        public string TemplateRootPath
        {
            get
            {
                return this.txtTemplateRoot.Text;
            }

            set
            {
                this.txtTemplateRoot.Text = value;
            }
        }

        public string RenderingRootPath
        {
            get
            {
                return this.txtRenderingRoot.Text;
            }

            set
            {
                this.txtRenderingRoot.Text = value;
            }
        }

        public TemplateFolder Templates
        {
            get
            {
                return (TemplateFolder)this.ViewState["Templates"];
            }

            set
            {
                this.ViewState["Templates"] = value;
                this.ucTemplates.DataSource = value.Folders.Any() ? value.Folders : new List<TemplateFolder>() { value };
            }
        }

        public RenderingFolder Renderings
        {
            get
            {
                return (RenderingFolder)this.ViewState["Renderings"];
            }

            set
            {
                this.ViewState["Renderings"] = value;
                this.ucRenderings.DataSource = value.Folders.Any() ? value.Folders : new List<RenderingFolder>() { value };
            }
        }

        public IEnumerable<string> ErrorMessages
        {
            get
            {
                return (IEnumerable<string>)this.rptErrMsgs.DataSource;
            }

            set
            {
                this.rptErrMsgs.DataSource = value;
            }
        }

        public override void DataBind()
        {
            this.ucRenderings.DataBind();
            this.ucTemplates.DataBind();
            base.DataBind();
        }

        protected override void OnInit(EventArgs args)
        {
            this.CheckSecurity(true);
            base.OnInit(args);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this._presenter = new DocumenterPresenter(this, new DocumenterService());

            this.btnSubmit.Click += (sender, args) => { this._presenter.LoadData(); };
        }
    }
}