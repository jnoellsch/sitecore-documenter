namespace SitecoreDocumentor.Web.Models
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class RenderingFolder : ModelBase
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