namespace Sitecore.SharedSource.Documenter.Mapping
{
    using System;
    using System.Web;
    using global::Sitecore.Configuration;
    using global::Sitecore.Data;
    using global::Sitecore.Data.Fields;
    using global::Sitecore.Data.Items;
    using global::Sitecore.SharedSource.Documenter.Models;
    using Constants = global::Sitecore.SharedSource.Documenter.Constants;

    public class RenderingMetaItemMapper : IObjectMapper<Item, RenderingMetaItem>
    {
        private readonly HttpRequestBase _httpRequest;

        public RenderingMetaItemMapper(HttpRequestBase httpRequest)
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

        public RenderingMetaItem Map(Item source)
        {
            if (source == null) return null;

            System.Guid guid = source.ID.ToGuid();
            string path = source.Paths.GetPath(ItemPathType.Name);
            string name = source.DisplayName;
            string icon = string.Format(
                               "{0}{1}{2}",
                               this._httpRequest.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped),
                               "/~/icon/",
                               source.Fields[FieldIDs.Icon].GetValueWithFallback("Software/16x16/element.png"));
            string thumbnailImage = source.Fields[FieldIDs.Thumbnail].GetMediaUrlSafe();
            string fullImage = this.MakeFullImage(source);

            Field descriptionField = source.Fields[Constants.Fields.LongDescription];
            string description = descriptionField == null ? string.Empty : descriptionField.Value;

            Field dataSourceLocationField = source.Fields[Constants.Fields.DataSourceLocation];
            string dataSourceLocation = dataSourceLocationField == null ? string.Empty : dataSourceLocationField.Value;

            TemplateMetaItem templateMetaItem = this.FillRenderingDataSourceTemplate(source);
            string type = source.TemplateName.Replace("Rendering", string.Empty).Replace("rendering", string.Empty);

            return new RenderingMetaItem()
                   {
                       Id = guid,
                       Path = path,
                       Name = name,
                       Icon = icon,
                       ThumbnailImage = thumbnailImage,
                       FullImage = fullImage,
                       Description = description,
                       DataSourceLocation = dataSourceLocation,
                       DataSourceTemplate = templateMetaItem,
                       Type = type
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
            var fullImgItem = this.Database.GetItem(fullImgPath);
            if (fullImgItem == null)
            {
                return string.Empty;
            }

            // generate path to image
            return fullImgItem.GetMediaUrlFull().CleanAdminPath();
        }

        private TemplateMetaItem FillRenderingDataSourceTemplate(Item item)
        {
            var dataSourceTemplate = item.Fields[Constants.Fields.DataSourceTemplate];
            if (dataSourceTemplate == null) return null;
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