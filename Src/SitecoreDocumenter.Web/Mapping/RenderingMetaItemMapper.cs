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

            return new RenderingMetaItem()
                   {
                       Id = source.ID.ToGuid(),
                       Path = source.Paths.GetPath(ItemPathType.Name),
                       Name = source.DisplayName,
                       Icon = string.Format(
                               "{0}{1}{2}",
                               this._httpRequest.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped),
                               "/sitecore/shell/~/icon/",
                               source.Fields[FieldIDs.Icon].GetValueWithFallback("Software/16x16/element.png")),
                       ThumbnailImage = source.Fields[FieldIDs.Thumbnail].GetMediaUrlSafe(),
                       FullImage = this.MakeFullImage(source),
                       Description = source.Fields[Constants.Fields.LongDescription].Value,
                       DataSourceLocation = source.Fields[Constants.Fields.DataSourceLocation].Value,
                       DataSourceTemplate = this.FillRenderingDataSourceTemplate(source),
                       Type = source.TemplateName.Replace("Rendering", string.Empty).Replace("rendering", string.Empty)
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