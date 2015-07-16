namespace SitecoreDocumentor.Core
{
    using Sitecore.Data;

    public class Constants
    {
        public class Templates
        {
            public static readonly ID Folder = new ID("{A87A00B1-E6DB-45AB-8B54-636FEC3B5523}");
            public static readonly ID RenderingFolder = new ID("{7EE0975B-0698-493E-B3A2-0B2EF33D0522}");
            public static readonly ID TemplateFolder = new ID("{0437FEE2-44C9-46A6-ABE9-28858D9FEE8C}");
            public static readonly ID SublayoutFolder = new ID("{3BAA73E5-6BA9-4462-BF72-C106F8801B11}");
        }

        public class Fields
        {
            public static readonly string Description = "Description";
            public static readonly string DataSourceLocation = "Datasource Location";
            public static readonly string DataSourceTemplate = "Datasource Template";
        }
    }
}
