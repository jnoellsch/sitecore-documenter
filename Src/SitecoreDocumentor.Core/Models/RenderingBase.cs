namespace SitecoreDocumentor.Core.Models
{
    using System;
    using Sitecore.Data.Items;

    [Serializable]
    public class RenderingBase
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
