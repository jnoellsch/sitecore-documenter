namespace SitecoreDocumentor.Web.UserControls
{
    using System;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using SitecoreDocumentor.Web.Models;

    public partial class TemplatesList : UserControl
    {
        public object DataSource
        {
            get
            {
                return this.rptTemplateFolders.DataSource;
            }

            set
            {
                this.rptTemplateFolders.DataSource = value;
                this.rptJumplinks.DataSource = value;
            }
        }

        private SectionItem LastFieldSection
        {
            get
            {
                object o = this.ViewState["LastFieldSection"];
                return (SectionItem)o;
            }

            set
            {
                this.ViewState["LastFieldSection"] = value;
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
                // show templates
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

                // show fields
                var rptFields = (Repeater)args.Item.FindControl("rptFields");
                rptFields.ItemDataBound += this.FieldsRepeaterOnItemDataBound;
                rptFields.Visible = templateMetaItem.Fields.Any();
                rptFields.DataSource = templateMetaItem.Fields;
                rptFields.DataBind();

                // show insert options
                var rptInsertOptions = (Repeater)args.Item.FindControl("rptInsertOptions");
                rptInsertOptions.Visible = templateMetaItem.InsertOptions.Any();
                rptInsertOptions.DataSource = templateMetaItem.InsertOptions;
                rptInsertOptions.DataBind();

                // show base teplates
                var rptBaseTemplates = (Repeater)args.Item.FindControl("rptBaseTemplates");
                rptBaseTemplates.Visible = templateMetaItem.BaseTemplates.Any();
                rptBaseTemplates.DataSource = templateMetaItem.BaseTemplates;
                rptBaseTemplates.DataBind();
            }
        }

        private void FieldsRepeaterOnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {
                // display grouping header, if applicapble
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