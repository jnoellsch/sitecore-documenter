namespace SitecoreDocumentor.Web
{
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Data;
    using Sitecore;
    using Sitecore.Data.Items;
    using Sitecore.Resources.Media;
    using SitecoreDocumentor.Core.Models;
    using Constants = SitecoreDocumentor.Core.Constants;

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

        private RenderingFolder GetRenderingsDeep(string rootPath, RenderingFolder result)
        {
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
                    .Select(x => new RenderingFolder()
                                 {
                                     Id = x.ID.ToGuid(),
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
                var renderings = root
                    .Children
                    .Where(x => !this.FolderTemplates.Contains(x.TemplateID))
                    .Select(x => new RenderingMetaItem()
                                 {
                                     Id = x.ID.ToGuid(),
                                     Path = x.Paths.GetPath(ItemPathType.Name),
                                     Name = x.DisplayName,
                                     Icon = x.Fields[FieldIDs.Icon].GetValue(true, true),
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

        private TemplateFolder GetTemplatesDeep(string rootPath, TemplateFolder result)
        {
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
                    .Select(x => new TemplateFolder()
                                 {
                                     Id = x.ID.ToGuid(),
                                     Path = x.Paths.GetPath(ItemPathType.Name), 
                                     Name = x.DisplayName
                                 })
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
                    .Select(x => new TemplateMetaItem()
                                 {
                                     Id = x.ID.ToGuid(),
                                     Path = x.Paths.GetPath(ItemPathType.Name),
                                     Name = x.DisplayName,
                                     Icon = x.Fields[FieldIDs.Icon].GetValue(true, true),
                                     Description = x.Fields[Constants.Fields.LongDescription].Value,
                                     Fields = this.GetTemplateFields(x.ID)
                                 })
                    .ToList();

                foreach (var r in templates)
                {
                    result.Templates.Add(r);
                }
            }

            return result;
        }

        private IEnumerable<FieldItem> GetTemplateFields(ID templateId)
        {
            var template = this.Database.GetItem(templateId);
            var fields = template
                .Axes
                .GetDescendants()
                .Where(x => x.TemplateID == Constants.Templates.TemplateField)
                .Select(x => new FieldItem()
                             {
                                 Id = x.ID.ToGuid(),
                                 Path = x.Paths.GetPath(ItemPathType.Name),
                                 Name = x.DisplayName,
                                 Section = new SectionItem()
                                           {
                                             Id  = x.Parent.ID.ToGuid(),
                                             Name = x.Parent.DisplayName,
                                             Path = x.Parent.Paths.GetPath(ItemPathType.Name)
                                           },
                                 FieldType = x.Fields[Constants.Fields.Type].Value,
                                 Source = x.Fields[FieldIDs.Source].Value,
                                 LongDescription = x.Fields[Constants.Fields.LongDescription].Value,
                                 ShortDescription = x.Fields[Constants.Fields.ShortDescription].Value,
                                 IsRequired = x.Fields[Constants.Fields.ValidatorBar].Value.Contains(Constants.Validators.IsRequired.ToString())
                             })
                .ToList();

            return fields;
        }
    }
}