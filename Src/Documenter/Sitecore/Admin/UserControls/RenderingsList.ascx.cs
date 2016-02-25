namespace Sitecore.SharedSource.Documenter.UserControls
{
    using System;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using global::Sitecore.SharedSource.Documenter.Models;

    public partial class RenderingsList : UserControl
    {
        public object DataSource
        {
            get
            {
                return this.rptRenderingFolders.DataSource;
            }

            set
            {
                this.rptRenderingFolders.DataSource = value;
                this.rptJumplinks.DataSource = value;
            }
        }

        public string ImageWidths { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.rptRenderingFolders.ItemDataBound += this.RenderingsFolderRepeaterOnItemDataBound;
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
    }
}