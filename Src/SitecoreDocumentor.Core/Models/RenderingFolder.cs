namespace SitecoreDocumentor.Core.Models
{
    using System;
    using System.Collections.Generic;
    using Sitecore.Data.Items;

    [Serializable]
    public class RenderingFolder : RenderingBase
    {
        public RenderingFolder()
        {
            this.Folders = new List<RenderingFolder>();
            this.Renderings = new List<RenderingMetaItem>();
        }

        public IList<RenderingFolder> Folders { get; set; }
        public IList<RenderingMetaItem> Renderings { get; set; }
    }
}