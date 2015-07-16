namespace SitecoreDocumentor.Web
{
    using System.Linq;
    using Sitecore.Data;
    using Sitecore;
    using Sitecore.Data.Items;
    using Sitecore.Resources.Media;
    using SitecoreDocumentor.Core.Models;
    using Constants = SitecoreDocumentor.Core.Constants;

    public class DocumentorService
    {
        public DocumentorService()
        {
        }

        private Database Database
        {
            get
            {
                return Sitecore.Configuration.Factory.GetDatabase("master");
            }
        }

        public RenderingFolder GetRenderings(string rootPath)
        {
            var rendering = new RenderingFolder();
            this.GetRenderingsDeep(rootPath, rendering);
            return rendering;
        }

        private RenderingFolder GetRenderingsDeep(string rootPath, RenderingFolder result)
        {
            // get root item - go deeper if are sub items
            // otherwise, assume empty folder or bad path
            var fldrTemplates = new[]
                                {
                                    Constants.Templates.RenderingFolder, 
                                    Constants.Templates.TemplateFolder,
                                    Constants.Templates.SublayoutFolder,
                                    Constants.Templates.Folder
                                };

            Item root = this.Database.GetItem(rootPath);
            result.Name = root.DisplayName;

            if (root.HasChildren)
            {
                // grab folders, add, traverse deeper
                var fldrs = root
                    .Axes
                    .GetDescendants()
                    .Where(x => fldrTemplates.Contains(x.TemplateID))
                    .Select(x => new RenderingFolder()
                                 {
                                     Path = x.Paths.GetPath(ItemPathType.Name), 
                                     Name = x.DisplayName
                                 })
                    .ToList();
                ;

                foreach (var f in fldrs)
                {
                    result.Folders.Add(this.GetRenderingsDeep(f.Path, f));
                }

                
                // grab renderings, add only
                var renderingsItems = root
                    .Children
                    .Where(x => !fldrTemplates.Contains(x.TemplateID))
                    .ToList();

                renderingsItems.ForEach(x => x.Fields.ReadAll());

                var renderings = renderingsItems
                    .Select(x => new RenderingMetaItem()
                                 {
                                     Path = x.Paths.GetPath(ItemPathType.Name),
                                     Name = x.DisplayName,
                                     Icon = x.Fields[FieldIDs.Icon].HasValue ? x.Fields[FieldIDs.Icon].Value : "Software/16x16/element.png",
                                     ThumbnailImage = MediaManager.GetMediaUrl(x.Fields[FieldIDs.Thumbnail].Item),
                                     Description = x.Fields[Constants.Fields.Description].Value,
                                     DataSourceLocation = x.Fields[Constants.Fields.DataSourceLocation].Value,
                                     DataSourceTemplate = x.Fields[Constants.Fields.DataSourceTemplate].Value
                                 })
                    .ToList();

                foreach (var r in renderings)
                {
                    result.Renderings.Add(r);
                }
            }
           
            return result;
        }

        public TemplateFolder GetTemplates(string rootPath)
        {
            return new TemplateFolder();
        }
    }
}