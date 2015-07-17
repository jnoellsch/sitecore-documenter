namespace SitecoreDocumentor.Web
{
    using System;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using SitecoreDocumentor.Core.Models;

    public partial class Documentor : Page, IDocumentorView
    {
        private DocumentorPresenter _presenter;

        private SectionItem LastFieldSection
        {
            get
            {
                object o = this.ViewState["PreviousFieldSection"];
                return (SectionItem)o;
            }

            set
            {
                this.ViewState["PreviousFieldSection"] = value;
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this._presenter = new DocumentorPresenter(this, new DocumentorService());

            this.btnSubmit.Click += (sender, args) => { this._presenter.LoadData(); };
            this.rptRenderingFolders.ItemDataBound += this.RenderingsFolderRepeaterOnItemDataBound;
            this.rptTemplateFolders.ItemDataBound += this.TemplatesFolderRepeaterOnItemDataBound;
        }

        private void TemplatesFolderRepeaterOnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {
                var templateFolder = (TemplateFolder)args.Item.DataItem;
                var rptTemplates = (Repeater)args.Item.FindControl("rptTemplates");
                rptTemplates.ItemDataBound += this.TemplatesRepeaterOnItemDataBound;
                rptTemplates.Visible = templateFolder.Templates.Any();
                rptTemplates.DataSource = templateFolder.Templates;
                rptTemplates.DataBind();
            }
        }

        private void TemplatesRepeaterOnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {
                var templateMetaItem = (TemplateMetaItem)args.Item.DataItem;
                var rptFields = (Repeater)args.Item.FindControl("rptFields");
                rptFields.ItemDataBound += this.FieldsRepeaterOnItemDataBound;
                rptFields.Visible = templateMetaItem.Fields.Any();
                rptFields.DataSource = templateMetaItem.Fields;
                rptFields.DataBind();
            }
        }

        private void FieldsRepeaterOnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {
                var fieldItem = (FieldItem)args.Item.DataItem;
                if (this.LastFieldSection != null && this.LastFieldSection.Id != fieldItem.Section.Id)
                {
                    args.Item.FindControl("trGroup").Visible = true;
                }

                this.LastFieldSection = fieldItem.Section;
            }
        }

        private void RenderingsFolderRepeaterOnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {
                var renderingFolder = (RenderingFolder)args.Item.DataItem;
                var rptRenderings = (Repeater)args.Item.FindControl("rptRenderings");
                rptRenderings.Visible = renderingFolder.Renderings.Any();
                rptRenderings.DataSource = renderingFolder.Renderings;
                rptRenderings.DataBind();
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

        public TemplateFolder Templates
        {
            get
            {
                return (TemplateFolder)this.ViewState["Templates"];
            }

            set
            {
                this.ViewState["Templates"] = value;
                this.rptTemplateFolders.DataSource = value.Folders;
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
                this.rptRenderingFolders.DataSource = value.Folders;
            }
        }
    }
}