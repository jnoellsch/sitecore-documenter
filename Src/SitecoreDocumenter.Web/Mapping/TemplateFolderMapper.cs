namespace Sitecore.SharedSource.Documenter.Mapping
{
    using global::Sitecore.Data.Items;
    using global::Sitecore.SharedSource.Documenter.Models;

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