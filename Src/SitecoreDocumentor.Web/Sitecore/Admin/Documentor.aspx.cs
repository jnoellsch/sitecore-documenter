namespace SitecoreDocumentor.Web
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using SitecoreDocumentor.Core.Models;

    public partial class Documentor : Page, IDocumentorView
    {
        private DocumentorPresenter _presenter;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this._presenter = new DocumentorPresenter(this, new DocumentorService());

            this.btnSubmit.Click += (sender, args) => { this._presenter.LoadData(); };
            this.rptRenderingFolders.ItemDataBound += this.RptRenderingFoldersOnItemDataBound;
        }

        private void RptRenderingFoldersOnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {
                var datasource = (RenderingFolder)args.Item.DataItem;
                var rpt = (Repeater)args.Item.FindControl("rptRenderings");
                rpt.DataSource = datasource.Renderings;
                rpt.DataBind();
            } 
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

        public TemplateFolder Templates { get; set; }

        public RenderingFolder Renderings
        {
            get
            {
                return (RenderingFolder)this.ViewState["Renderings"];
            }

            set
            {
                this.ViewState["Renderings"] = value;
                this.rptRenderingFolders.DataSource = value.Folders;
            }
        }
    }
}