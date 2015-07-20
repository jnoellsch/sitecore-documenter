using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreDocumentor.Web.Mapping
{
    using Sitecore;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using SitecoreDocumentor.Web.Models;
    using Constants = SitecoreDocumentor.Web.Constants;

    public class TemplateMetaItemMapper : IObjectMapper<Item, TemplateMetaItem>
    {
        private Database Database
        {
            get
            {
                return Sitecore.Configuration.Factory.GetDatabase("master");
            }
        }

        public TemplateMetaItem Map(Item source)
        {
            if (source == null) return null;

            return new TemplateMetaItem()
                   {
                       Id = source.ID.ToGuid(),
                       Path = source.Paths.GetPath(ItemPathType.Name),
                       Name = source.DisplayName,
                       Icon = source.Fields[FieldIDs.Icon].GetValue(true, true),
                       Description = source.Fields[Constants.Fields.LongDescription].Value,
                       Fields = this.GetTemplateFields(source),
                       BaseTemplates = this.FillTemplateBases(source),
                       InsertOptions = this.FillTemplateInsertOptions(source)
                   };
        }


        private IList<TemplateMetaItem> FillTemplateBases(Item template)
        {
            var noiseTemplates = new[] { Constants.Templates.StandardTemplate };
            var baseTemplatesField = (MultilistField)template.Fields[Constants.Fields.BaseTemplate];
            return baseTemplatesField
                .GetItems()
                .Where(x => !noiseTemplates.Contains(x.ID))
                .Select(x => new TemplateMetaItem()
                {
                    Id = x.ID.ToGuid(),
                    Path = x.Paths.GetPath(ItemPathType.Name),
                    Name = x.DisplayName
                })
                .ToList();
        }

        private IList<TemplateMetaItem> FillTemplateInsertOptions(Item template)
        {
            var standardValuesItem = this.Database.GetItem(template.Fields[FieldIDs.StandardValues].Value);
            if (standardValuesItem == null)
            {
                return Enumerable.Empty<TemplateMetaItem>().ToList();
            }

            var mastersField = (MultilistField)standardValuesItem.Fields[Constants.Fields.Masters];
            return mastersField
                .GetItems()
                .Select(x => new TemplateMetaItem()
                {
                    Id = x.ID.ToGuid(),
                    Path = x.Paths.GetPath(ItemPathType.Name),
                    Name = x.DisplayName
                })
                .ToList();
        }

        private IList<FieldItem> GetTemplateFields(Item template)
        {
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
                        Id = x.Parent.ID.ToGuid(),
                        Name = x.Parent.DisplayName,
                        Path = x.Parent.Paths.GetPath(ItemPathType.Name)
                    },
                    FieldType = x.Fields[Constants.Fields.Type].Value,
                    Source = x.Fields[Constants.Fields.Source].Value,
                    LongDescription = x.Fields[Constants.Fields.LongDescription].Value,
                    ShortDescription = x.Fields[Constants.Fields.ShortDescription].Value,
                    IsRequired = x.Fields[Constants.Fields.ValidatorBar].Value.Contains(Constants.Validators.IsRequired.ToString())
                })
                .ToList();

            return fields;
        }
    }
}