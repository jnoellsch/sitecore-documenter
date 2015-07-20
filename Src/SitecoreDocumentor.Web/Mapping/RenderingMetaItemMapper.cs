namespace SitecoreDocumentor.Web.Mapping
{
    using Sitecore;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using SitecoreDocumentor.Web.Models;
    using Constants = SitecoreDocumentor.Web.Constants;

    public class RenderingMetaItemMapper : IObjectMapper<Item, RenderingMetaItem>
    {
        private Database Database
        {
            get
            {
                return Sitecore.Configuration.Factory.GetDatabase("master");
            }
        }

        public RenderingMetaItem Map(Item source)
        {
            if (source == null) return null;

            return new RenderingMetaItem()
                   {
                       Id = source.ID.ToGuid(),
                       Path = source.Paths.GetPath(ItemPathType.Name),
                       Name = source.DisplayName,
                       Icon = source.Fields[FieldIDs.Icon].GetValue(true, true),
                       ThumbnailImage = source.Fields[FieldIDs.Thumbnail].GetMediaUrlSafe(),
                       Description = source.Fields[Constants.Fields.LongDescription].Value,
                       DataSourceLocation = source.Fields[Constants.Fields.DataSourceLocation].Value,
                       DataSourceTemplate = this.FillRenderingDataSourceTemplate(source)
                   };
        }

        private TemplateMetaItem FillRenderingDataSourceTemplate(Item item)
        {
            var templateItem = this.Database.GetItem(item.Fields[Constants.Fields.DataSourceTemplate].Value);
            if (templateItem == null)
            {
                return null;
            }

            return new TemplateMetaItem()
            {
                Id = templateItem.ID.ToGuid(),
                Path = templateItem.Paths.GetPath(ItemPathType.Name),
                Name = templateItem.DisplayName
            };
        }

    }
}