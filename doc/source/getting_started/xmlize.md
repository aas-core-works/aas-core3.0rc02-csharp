# XML De/serialization

The code that de/serializes AAS models from and to XML documents lives in the static class [Xmlization].

[Xmlization]: ../api/AasCore.Aas3_0_RC02.Xmlization.yml

## Serialize

You serialize a model using the static class [Xmlization.Serialize] by calling its `To` method on an [Environment].
The `To` method writes to a [System.Xml.XmlWriter].

[Xmlization.Serialize]: ../api/AasCore.Aas3_0_RC02.Xmlization.Serialize.yml
[Environment]: ../api/AasCore.Aas3_0_RC02.Environment.yml
[System.Xml.XmlWriter]: https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlwriter

Here is an example snippet:

```cs
using System.Collections.Generic;

using Aas = AasCore.Aas3_0_RC02;
using AasXmlization = AasCore.Aas3_0_RC02.Xmlization;

public class Program
{
    public static void Main()
    {
        // Prepare the environment
        var someProperty = new Aas.Property(
            Aas.DataTypeDefXsd.Boolean)
        {
            IdShort = "someProperty",
        };

        var submodel = new Aas.Submodel(
            "some-unique-global-identifier")
        {
            SubmodelElements = new List<Aas.ISubmodelElement>()
            {
                someProperty
            }
        };

        var environment = new Aas.Environment()
        {
            Submodels = new List<Aas.Submodel>()
            {
                submodel
            }
        };

        // Serialize to an XML writer
        var outputBuilder = new System.Text.StringBuilder();

        using var writer = System.Xml.XmlWriter.Create(
            outputBuilder,
            new System.Xml.XmlWriterSettings()
            {
                Encoding = System.Text.Encoding.UTF8,
                OmitXmlDeclaration = true
            }
        );

        AasXmlization.Serialize.To(
            environment,
            writer
        );

        // Print the output
        System.Console.WriteLine(
            outputBuilder.ToString()
        );

        // Outputs (all on a single line):
        // <environment xmlns="http://www.admin-shell.io/aas/3/0/RC02">
        // <submodels><submodel><id>some-unique-global-identifier</id>
        // <submodelElements><property><idShort>someProperty</idShort>
        // <valueType>xs:boolean</valueType></property></submodelElements>
        // </submodel></submodels></environment>
    }
}
```

(You can run the snippet at: https://dotnetfiddle.net/VEL2jU)

## De-serialize

The de-serialization is encapsulated in [Xmlization.Deserialize] static class.
The crucial method is `EnvironmentFrom` which reads from an [System.Xml.XmlReader] and re-creates back an instance of [Environment].

[Xmlization.Deserialize]: ../api/AasCore.Aas3_0_RC02.Xmlization.Deserialize.yml
[System.Xml.XmlReader]: https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlreader

Here is a snippet which parses XML as text and then de-serializes it into an instance of [Environment]:

```cs
using System.Collections.Generic;

using Aas = AasCore.Aas3_0_RC02;
using AasXmlization = AasCore.Aas3_0_RC02.Xmlization;

public class Program
{
    public static void Main()
    {
        var text = (
            "<environment xmlns=\"http://www.admin-shell.io/aas/3/0/RC02\">" +
            "<submodels><submodel><id>some-unique-global-identifier</id>" +
            "<submodelElements><property><idShort>someProperty</idShort>" +
            "<valueType>xs:boolean</valueType></property></submodelElements>" +
            "</submodel></submodels></environment>"
        );

        using var stringReader = new System.IO.StringReader(
            text);

        using var xmlReader = System.Xml.XmlReader.Create(
            stringReader);

        var environment = AasXmlization.Deserialize.EnvironmentFrom(
            xmlReader);

        // Print the types of the model elements contained
        // in the environment
        foreach (var something in environment.Descend())
        {
            System.Console.WriteLine(something.GetType());
        }

        // Outputs:
        // AasCore.Aas3_0_RC02.Submodel
        // AasCore.Aas3_0_RC02.Property
    }
}
```

(You can run the snippet at: https://dotnetfiddle.net/TD0Ro8)

### Errors

If the XML document from [System.Xml.XmlReader] comes in an unexpected form, our SDK throws a [Xmlization.Exception].
This can happen, for example, if unexpected XML elements or XML attributes are encountered, or an expected XML element is missing.

[System.Xml.XmlReader]: https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlreader
[Xmlization.Exception]: ../api/AasCore.Aas3_0_RC02.Xmlization.Exception.yml
