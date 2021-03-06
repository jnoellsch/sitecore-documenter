﻿namespace Sitecore.SharedSource.Documenter.Services
{
    using System.Linq;
    using System.Web;
    using global::Sitecore.Configuration;
    using global::Sitecore.Data;
    using global::Sitecore.Data.Items;
    using global::Sitecore.SharedSource.Documenter.Mapping;
    using global::Sitecore.SharedSource.Documenter.Models;

    public class DocumenterService
    {
        private readonly ID[] _fldrTemplates =
        {
            Constants.Templates.Folders.RenderingFolder, Constants.Templates.Folders.TemplateFolder,
            Constants.Templates.Folders.SublayoutFolder, Constants.Templates.Folders.Folder
        };

        private Database Database
        {
            get
            {
                return Factory.GetDatabase(Constants.Databases.Master);
            }
        }

        private ID[] FolderTemplates
        {
            get
            {
                return this._fldrTemplates;
            }
        }

        public RenderingFolder GetRenderings(string rootPath)
        {
            var result = this.GetRenderingsDeep(rootPath);
            return result;
        }

        public TemplateFolder GetTemplates(string rootPath)
        {
            var result = this.GetTemplatesDeep(rootPath);
            return result;
        }

        public bool IsValidItem(string path)
        {
            return this.Database.GetItem(path) != null;
        }

        private RenderingFolder GetRenderingsDeep(string rootPath)
        {
            var metaItemMapper = new RenderingMetaItemMapper(new HttpRequestWrapper(HttpContext.Current.Request));
            var fldrMapper = new RenderingFolderMapper();

            // get root item - go deeper if are sub items
            // otherwise, assume empty folder or bad path
            Item root = this.Database.GetItem(rootPath);
            RenderingFolder result = fldrMapper.Map(root);

            if (root.HasChildren)
            {
                // grab folders, add, traverse deeper
                var fldrs = root
                    .Axes
                    .GetDescendants()
                    .Where(x => this.FolderTemplates.Contains(x.TemplateID))
                    .Select(x => fldrMapper.Map(x))
                    .ToList();

                foreach (var f in fldrs)
                {
                    result.Folders.Add(this.GetRenderingsDeep(f.Path));
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

        private TemplateFolder GetTemplatesDeep(string rootPath)
        {
            var metaItemMapper = new TemplateMetaItemMapper(new HttpRequestWrapper(HttpContext.Current.Request));
            var fldrMapper = new TemplateFolderMapper();

            // get root item - go deeper if are sub items
            // otherwise, assume empty folder or bad path
            Item root = this.Database.GetItem(rootPath);
            TemplateFolder result = fldrMapper.Map(root);

            if (root.HasChildren)
            {
                // grab folders, add, traverse deeper
                var fldrs = root
                    .Axes
                    .GetDescendants()
                    .Where(x => this.FolderTemplates.Contains(x.TemplateID))
                    .Select(x => fldrMapper.Map(x))
                    .ToList();

                foreach (var f in fldrs)
                {
                    result.Folders.Add(this.GetTemplatesDeep(f.Path));
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