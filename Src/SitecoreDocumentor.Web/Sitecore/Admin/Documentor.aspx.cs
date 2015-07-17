namespace SitecoreDocumentor.Web
{
    using System;
    using System.Web.UI;
    using SitecoreDocumentor.Core.Models;

    public partial class Documentor : Page, IDocumentorView
    {
        private DocumentorPresenter _presenter;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this._presenter = new DocumentorPresenter(this, new DocumentorService());

            this.btnSubmit.Click += (sender, args) => { this._presenter.LoadData(); };
        }

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
                this.ucTemplates.DataSource = value.Folders;
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
                this.ucRenderings.DataSource = value.Folders;
            }
        }

        public override void DataBind()
        {
            this.ucRenderings.DataBind();
            this.ucTemplates.DataBind();
        }
    }
}