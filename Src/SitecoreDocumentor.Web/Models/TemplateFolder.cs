namespace SitecoreDocumenter.Web.Models
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class TemplateFolder : ModelBase
    {
        public TemplateFolder()
        {
            this.Folders = new List<TemplateFolder>();
            this.Templates = new List<TemplateMetaItem>();
        }

        public IList<TemplateFolder> Folders { get; set; }
        public IList<TemplateMetaItem> Templates { get; set; }
    }
}
