namespace SitecoreDocumentor.Web
{
    using Sitecore.Data.Fields;
    using Sitecore.Resources.Media;

    public static class SitecoreExtensions
    {
        public static string GetMediaUrlSafe(this Field field)
        {
            // check field
            if (field == null)
            {
                return string.Empty;
            }

            // check field, as a media/image type
            var imageField = (ImageField)field;
            if (imageField == null || imageField.MediaItem == null)
            {
                return string.Empty;
            }

            // generate image url
            string url = MediaManager.GetMediaUrl(imageField.MediaItem).Replace("/sitecore/admin", string.Empty);
            return url;
        }
    }
}