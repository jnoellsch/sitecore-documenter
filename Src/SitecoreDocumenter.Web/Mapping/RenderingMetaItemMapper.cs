namespace SitecoreDocumenter.Web.Mapping
{
    using Sitecore;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Resources.Media;
    using SitecoreDocumenter.Web.Models;
    using Constants = Constants;

    public class RenderingMetaItemMapper : IObjectMapper<Item, RenderingMetaItem>
    {
        private Database Database
        {
            get
            {
                return Sitecore.Configuration.Factory.GetDatabase(Constants.Databases.Master);
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
                       Icon = source.Fields[FieldIDs.Icon].GetValueWithFallback("Software/16x16/element.png"),
                       ThumbnailImage = source.Fields[FieldIDs.Thumbnail].GetMediaUrlSafe(),
                       FullImage = this.MakeFullImage(source),
                       Description = source.Fields[Constants.Fields.LongDescription].Value,
                       DataSourceLocation =
                           source.Fields[Constants.Fields.DataSourceLocation].Value,
                       DataSourceTemplate = this.FillRenderingDataSourceTemplate(source)
                   };
        }

        /// <summary>
        /// Attempts to retrieve a larger version of the thumbnail image. Assumes full images 
        /// are in the same directory as the thumbnail and have a "_full" suffix.
        /// </summary>
        private string MakeFullImage(Item item)
        {
            // grab thumbnail
            var thumbImgField = (ImageField)item.Fields[FieldIDs.Thumbnail];
            if (thumbImgField.MediaItem == null)
            {
                return string.Empty;
            }

            // using convention, attempt to grab full image
            string fullImgPath = string.Concat(thumbImgField.MediaItem.Paths.GetPath(ItemPathType.Name), "_full");
            var fullImgItem = (MediaItem)this.Database.GetItem(fullImgPath);
            if (fullImgItem == null)
            {
                return string.Empty;
            }

            // generate path to image
            return MediaManager.GetMediaUrl(fullImgItem).CleanAdminPath();
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