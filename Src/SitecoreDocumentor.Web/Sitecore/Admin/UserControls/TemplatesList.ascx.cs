namespace SitecoreDocumentor.Web.UserControls
{
    using System;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using SitecoreDocumentor.Core.Models;

    public partial class TemplatesList : UserControl
    {
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

        public object DataSource
        {
            get
            {
                return this.rptTemplateFolders.DataSource;
            }

            set
            {
                this.rptTemplateFolders.DataSource = value;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

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
    }
}