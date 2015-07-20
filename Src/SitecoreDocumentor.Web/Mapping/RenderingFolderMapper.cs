namespace SitecoreDocumenter.Web.Mapping
{
    using Sitecore.Data.Items;
    using SitecoreDocumenter.Web.Models;

    public class RenderingFolderMapper : IObjectMapper<Item, RenderingFolder>
    {
        public RenderingFolder Map(Item source)
        {
            if (source == null) return null;

            return new RenderingFolder()
                   {
                       Id = source.ID.ToGuid(),
                       Path = source.Paths.GetPath(ItemPathType.Name),
                       Name = source.DisplayName
                   };
        }
    }
}