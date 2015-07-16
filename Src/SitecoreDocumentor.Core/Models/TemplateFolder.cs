namespace SitecoreDocumentor.Core.Models
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class TemplateFolder
    {
        public string Name { get; set; }
        public IList<TemplateFolder> Folders { get; set; }
        public IList<TemplateItem> Templates { get; set; }
    }
}
