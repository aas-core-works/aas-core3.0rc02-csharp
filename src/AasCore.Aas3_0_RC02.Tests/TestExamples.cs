using Aas = AasCore.Aas3_0_RC02; // renamed
using AasXmlization = AasCore.Aas3_0_RC02.Xmlization; // renamed

using NUnit.Framework; // can't alias
using System.Collections.Generic;  // can't alias

namespace AasCore.Aas3_0_RC02.Tests
{
    public class TestExamples
    {
        /// <summary>
        /// Correspond to: https://dotnetfiddle.net/VEL2jU#
        /// </summary>
        [Test]
        public void Test_XML_serialization()
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

            writer.Flush();

            Assert.AreEqual(
                "<environment xmlns=\"https://admin-shell.io/aas/3/0/RC02\">" +
                "<submodels><submodel><id>some-unique-global-identifier</id>" +
                "<submodelElements><property><idShort>someProperty</idShort>" +
                "<valueType>xs:boolean</valueType></property></submodelElements>" +
                "</submodel></submodels></environment>",
                outputBuilder.ToString());
        }
    }
}