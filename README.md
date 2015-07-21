# Sitecore Documenter
Produces detailed information of custom Sitecore renderings and templates for inclusion in technical documentation. 

To use, install the ZIP module from the Packages directory in the repository. 
Once installed, browse to /sitecore/admin/documenter.aspx to process the documentation.

## Folders
* Name
* Path

---

## Renderings

* Icon
* Name
* Path
* Description
* Type
* Data source (location + template)
* Image

---

> NOTE: Description field populated using "Long Description" field. Image is populated by "Thumbnail Image" field. Since larger images may be desired, a convention 
> of an appended "_full" suffix is used. For example, if the thumbnail points to /media library/image1, a secondary 
> image of /media library/image1_full could be uploaded and would take precedence.

## Templates

* Icon
* Name
* Decription
* Base Templates
* Insert Options
* Fields 
    * Section Name
    * Name
    * Description
    * Field Type
    * Source
    * Is Required 

---

> NOTE: Description fields populated using "Long Description" field.