namespace SitecoreDocumentor.Web
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
            public static readonly ID TemplateField = new ID("{455A3E98-A627-4B40-8035-E683A0331AC7}");
            public static readonly ID StandardTemplate = new ID("{1930bbeb-7805-471a-a3be-4858ac7cf696}");
        }

        public class Fields
        {
            public static readonly string DataSourceLocation = "Datasource Location";
            public static readonly string DataSourceTemplate = "Datasource Template";
            public static readonly string Type = "Type";
            public static readonly string LongDescription = "__Long Description";
            public static readonly string ShortDescription = "__Short Description";
            public static readonly string ValidatorBar = "Validator Bar";
            public static readonly string Masters = "__Masters";
            public static readonly string BaseTemplate = "__Base Template";
            public static readonly string Source = "Source";
        }

        public class Validators
        {
            public static readonly ID IsRequired = new ID("{59D4EE10-627C-4FD3-A964-61A88B092CBC}");
        }

        public class Databases
        {
            public static readonly string Master = "master";
        }
    }
}
