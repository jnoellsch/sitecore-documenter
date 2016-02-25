namespace Sitecore.SharedSource.Documenter
{
    using global::Sitecore.Data.Fields;
    using global::Sitecore.Data.Items;
    using global::Sitecore.Resources.Media;

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
            var o = new MediaUrlOptions() { AlwaysIncludeServerUrl = true };
            string url = imageField.MediaItem.GetMediaUrlFull().CleanAdminPath();
            return url;
        }

        public static string GetValueWithFallback(this Field field, string fallback)
        {
            if (field == null)
            {
                return fallback;
            }

            return !string.IsNullOrEmpty(field.GetValue(true)) ? field.GetValue(true) : fallback;
        }

        public static string CleanAdminPath(this string path)
        {
            return !string.IsNullOrEmpty(path) ? path.Replace("/sitecore/admin", string.Empty) : path;
        }

        public static string GetMediaUrlFull(this Item mediaItem)
        {
            var o = new MediaUrlOptions() { AlwaysIncludeServerUrl = true };
            return MediaManager.GetMediaUrl(mediaItem, o).CleanAdminPath();
        }
    }
}