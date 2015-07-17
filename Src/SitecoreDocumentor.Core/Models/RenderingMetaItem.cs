namespace SitecoreDocumentor.Core.Models
{
    using System;
    using Sitecore.Data.Items;

    [Serializable]
    public class RenderingMetaItem : ModelBase
    {
        public string Description { get; set; }
        public string Icon { get; set; }
        public string ThumbnailImage { get; set; }
        public string FullImage { get; set; }
        public string DataSourceTemplate { get; set; }
        public string DataSourceLocation { get; set; }
    }
}