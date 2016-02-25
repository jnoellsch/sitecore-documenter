namespace Sitecore.SharedSource
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Sitecore.sitecore.admin;
    using global::Sitecore.SharedSource.Documenter.Models;
    using global::Sitecore.SharedSource.Documenter.Presenters;
    using global::Sitecore.SharedSource.Documenter.Services;
    using global::Sitecore.SharedSource.Documenter.Views;

    public partial class DocumenterPage : AdminPage, IDocumenterView
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

        public string ImageWidths
        {
            get
            {
                return this.txtImageWidths.Text;
            }

            set
            {
                this.txtImageWidths.Text = value;
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
                this.ucRenderings.ImageWidths = this.ImageWidths;
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