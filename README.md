# RTEStyleFormatter

## Update 1.0.3 

Removed UTF8 file encoding

Changed Json serializer (removes unnecesary encoding)



[![Platform](https://img.shields.io/badge/Umbraco-10.0+-%233544B1?style=flat&logo=umbraco)](https://umbraco.com/products/umbraco-cms/)

**Umbraco.Community.RTEStyleFormatter** easy to edit json file for RTE style_formats in appsettings.json

## Usage
In the App_Plugins/RTEStyleFormatter folder you will find a json file called RTE_style_formats.json this is the nicely formatted json that will go in the appsettings.json "style_formats" string 
```
      "RichTextEditor": {
        "CustomConfig": {
          "style_formats": ""
        }
```
You can edit the RTE_style_formats.json file and when the application starts it will parse the file and write the contents to the settings file.

Sample file
```
[
  {
    "title": "Headings",
    "items": [
      {
        "title": "Heading h3",
        "block": "h3"
      }
    ]
  },
  {
    "title": "Inline",
    "items": [
      {
        "title": "Bold",
        "block": "strong"
      },
      {
        "title": "Underline",
        "inline": "span",
        "attributes": {
          "style": "text-decoration: underline;"
        }
      }
    ]
  },
  {
    "title": "Blocks",
    "items": [
      {
        "title": "Blockquote",
        "block": "blockquote"
      },
      {
        "title": "pre",
        "block": "pre"
      }
    ]
  }
]
```
## License
Copyright © 2023 Huw Reddick, and other contributors.

Licensed under the [MIT License](https://github.com/huwred/RTEStyleFormatter/blob/master/LICENSE.txt).

