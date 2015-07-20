namespace SitecoreDocumentor.Web.Mapping
{
    /// <summary>
    /// Maps one object to another.
    /// </summary>
    public interface IObjectMapper<in TSource, out TDestination>
    {
        /// <summary>
        /// Maps a <typeparamref name="TSource"/> object to a new <typeparamref name="TDestination"/> object.
        /// </summary>
        TDestination Map(TSource source);
    }
}
