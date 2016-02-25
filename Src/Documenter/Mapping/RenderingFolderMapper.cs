namespace Sitecore.SharedSource.Documenter.Mapping
{
    using global::Sitecore.Data.Items;
    using global::Sitecore.SharedSource.Documenter.Models;

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