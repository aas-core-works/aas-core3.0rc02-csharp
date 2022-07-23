using NUnit.Framework;  // can't alias

namespace AasCore.Aas3_0_RC02.Tests
{
    /// <summary>
    /// Play a bit with XmlReader so that this tests are kind of
    /// documented REPL.
    /// </summary>
    public class XmlPlayground
    {
        [Test]
        public void Test_none_at_the_beginning()
        {
            using var tmpDir = new TemporaryDirectory();
            var path = System.IO.Path.Join(tmpDir.Path, "something.xml");

            System.IO.File.WriteAllText(
                path,
                "<environment xmlns=\"https://example.com/1/2\">hello</environment>");

            using var reader = System.Xml.XmlReader.Create(path);

            Assert.AreEqual(
                System.Xml.XmlNodeType.None,
                reader.NodeType);
        }

        [Test]
        public void Test_reading_the_first_element()
        {
            using var tmpDir = new TemporaryDirectory();
            var path = System.IO.Path.Join(tmpDir.Path, "something.xml");

            System.IO.File.WriteAllText(
                path,
                "<environment xmlns=\"https://example.com/1/2\">hello</environment>");

            using var reader = System.Xml.XmlReader.Create(path);

            reader.Read();

            Assert.AreEqual(
                System.Xml.XmlNodeType.Element,
                reader.NodeType);
        }

        [Test]
        public void Test_reading_an_element_without_end_node()
        {
            using var tmpDir = new TemporaryDirectory();
            var path = System.IO.Path.Join(tmpDir.Path, "something.xml");

            System.IO.File.WriteAllText(
                path,
                "<environment><something /></environment>");

            using var reader = System.Xml.XmlReader.Create(path);

            reader.Read();
            Assert.AreEqual(
                System.Xml.XmlNodeType.Element,
                reader.NodeType);
            Assert.AreEqual("environment", reader.Name);

            reader.Read();
            Assert.AreEqual(
                System.Xml.XmlNodeType.Element,
                reader.NodeType);
            Assert.AreEqual("something", reader.Name);

            reader.Read();
            Assert.AreEqual(
                System.Xml.XmlNodeType.EndElement,
                reader.NodeType);
            Assert.AreEqual("environment", reader.Name);

            reader.Read();
            Assert.AreEqual(
                System.Xml.XmlNodeType.None,
                reader.NodeType);

            Assert.IsTrue(reader.EOF);
        }

        [Test]
        public void Test_can_not_read_content_of_a_self_closing_element()
        {
            using var tmpDir = new TemporaryDirectory();
            var path = System.IO.Path.Join(tmpDir.Path, "something.xml");

            System.IO.File.WriteAllText(
                path,
                "<environment />");

            using var reader = System.Xml.XmlReader.Create(path);

            reader.Read();
            Assert.AreEqual(
                System.Xml.XmlNodeType.Element,
                reader.NodeType);
            Assert.AreEqual("environment", reader.Name);

            Assert.Throws<System.InvalidOperationException>(
                () =>
            {
                string _ = reader.ReadContentAsString();
            });
        }

        [Test]
        public void Test_write_something()
        {
            var outputBuilder = new System.Text.StringBuilder();

            {
                using var writer = System.Xml.XmlWriter.Create(
                    outputBuilder,
                    new System.Xml.XmlWriterSettings()
                    {
                        OmitXmlDeclaration = true,
                        Indent = false
                    });

                writer.WriteStartElement("something");
                writer.WriteString("hello");
                writer.WriteEndElement();
            }

            Assert.AreEqual(
                "<something>hello</something>",
                outputBuilder.ToString());
        }

        [Test]
        public void Test_write_with_prefix_and_namespace()
        {
            var outputBuilder = new System.Text.StringBuilder();

            {
                using var writer = System.Xml.XmlWriter.Create(
                    outputBuilder,
                    new System.Xml.XmlWriterSettings()
                    {
                        OmitXmlDeclaration = true,
                        Indent = false
                    });

                writer.WriteStartElement(
                    "aas", "something", "https://example.com");

                writer.WriteStartElement(
                    "aas", "child", "https://example.com");

                writer.WriteEndElement();
            }

            Assert.AreEqual(
                "<aas:something xmlns:aas=\"https://example.com\"><aas:child /></aas:something>",
                outputBuilder.ToString());
        }
    }
}