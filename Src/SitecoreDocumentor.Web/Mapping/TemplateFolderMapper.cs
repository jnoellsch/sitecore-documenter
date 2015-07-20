namespace SitecoreDocumentor.Web.Mapping
{
    using Sitecore.Data.Items;
    using SitecoreDocumentor.Web.Models;

    public class TemplateFolderMapper : IObjectMapper<Item, TemplateFolder>
    {
        public TemplateFolder Map(Item source)
        {
            if (source == null) return null;

            return new TemplateFolder()
                   {
                       Id = source.ID.ToGuid(),
                       Path = source.Paths.GetPath(ItemPathType.Name),
                       Name = source.DisplayName
                   };
        }
    }
}