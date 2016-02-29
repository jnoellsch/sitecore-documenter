namespace Sitecore.SharedSource.Documenter.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using global::Sitecore.Configuration;
    using global::Sitecore.Data;
    using global::Sitecore.Data.Fields;
    using global::Sitecore.Data.Items;
    using global::Sitecore.SharedSource.Documenter.Models;
    using Constants = global::Sitecore.SharedSource.Documenter.Constants;

    public class TemplateMetaItemMapper : IObjectMapper<Item, TemplateMetaItem>
    {
        private readonly HttpRequestBase _httpRequest;

        public TemplateMetaItemMapper(HttpRequestBase httpRequest)
        {
            if (httpRequest == null) throw new ArgumentNullException("httpRequest");
            this._httpRequest = httpRequest;
        }

        private Database Database
        {
            get
            {
                return Factory.GetDatabase(Constants.Databases.Master);
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
                       Icon = string.Format(
                               "{0}{1}{2}",
                               this._httpRequest.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped),
                               "/~/icon/",
                               source.Fields[FieldIDs.Icon].GetValueWithFallback("Software/16x16/element.png")),
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
            if (baseTemplatesField == null)
            {
                return Enumerable.Empty<TemplateMetaItem>().ToList();
            }

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
            if (mastersField == null)
            {
                return Enumerable.Empty<TemplateMetaItem>().ToList();
            }

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