namespace SitecoreDocumentor.Web.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Resources.Media;
    using Sitecore.Visualization;
    using SitecoreDocumentor.Web.Mapping;
    using SitecoreDocumentor.Web.Models;
    using Constants = SitecoreDocumentor.Web.Constants;

    public class DocumentorService
    {
        private readonly ID[] _fldrTemplates = {
                                                    Constants.Templates.RenderingFolder, 
                                                    Constants.Templates.TemplateFolder,
                                                    Constants.Templates.SublayoutFolder,
                                                    Constants.Templates.Folder
                                                };

        private Database Database
        {
            get
            {
                return Sitecore.Configuration.Factory.GetDatabase("master");
            }
        }

        public ID[] FolderTemplates
        {
            get
            {
                return this._fldrTemplates;
            }
        }

        public RenderingFolder GetRenderings(string rootPath)
        {
            var rendering = new RenderingFolder();
            this.GetRenderingsDeep(rootPath, rendering);
            return rendering;
        }

        public TemplateFolder GetTemplates(string rootPath)
        {
            var result = new TemplateFolder();
            this.GetTemplatesDeep(rootPath, result);
            return result;
        }

        public bool IsValidItem(string path)
        {
            return this.Database.GetItem(path) != null;
        }

        private RenderingFolder GetRenderingsDeep(string rootPath, RenderingFolder result)
        {
            var metaItemMapper = new RenderingMetaItemMapper();
            var fldrMapper = new RenderingFolderMapper();

            // get root item - go deeper if are sub items
            // otherwise, assume empty folder or bad path
            Item root = this.Database.GetItem(rootPath);
            result.Name = root.DisplayName;

            if (root.HasChildren)
            {
                // grab folders, add, traverse deeper
                var fldrs = root
                    .Axes
                    .GetDescendants()
                    .Where(x => this.FolderTemplates.Contains(x.TemplateID))
                    .Select(x => fldrMapper.Map(x))
                    .ToList();
                ;

                foreach (var f in fldrs)
                {
                    result.Folders.Add(this.GetRenderingsDeep(f.Path, f));
                }
                
                // grab renderings, add only
                var renderings = root
                    .Children
                    .Where(x => !this.FolderTemplates.Contains(x.TemplateID))
                    .Select(x => metaItemMapper.Map(x))
                    .ToList();

                foreach (var r in renderings)
                {
                    result.Renderings.Add(r);
                }
            }
           
            return result;
        }

        private TemplateFolder GetTemplatesDeep(string rootPath, TemplateFolder result)
        {
            var metaItemMapper = new TemplateMetaItemMapper();
            var fldrMapper = new TemplateFolderMapper();

            // get root item - go deeper if are sub items
            // otherwise, assume empty folder or bad path
            Item root = this.Database.GetItem(rootPath);
            result.Name = root.DisplayName;

            if (root.HasChildren)
            {
                // grab folders, add, traverse deeper
                var fldrs = root
                    .Axes
                    .GetDescendants()
                    .Where(x => this.FolderTemplates.Contains(x.TemplateID))
                    .Select(x => fldrMapper.Map(x))
                    .ToList();
                ;

                foreach (var f in fldrs)
                {
                    result.Folders.Add(this.GetTemplatesDeep(f.Path, f));
                }

                // grab templates, add only
                var templates = root
                    .Children
                    .Where(x => !this.FolderTemplates.Contains(x.TemplateID))
                    .Select(x => metaItemMapper.Map(x))
                    .ToList();

                foreach (var r in templates)
                {
                    result.Templates.Add(r);
                }
            }

            return result;
        }

    }
}