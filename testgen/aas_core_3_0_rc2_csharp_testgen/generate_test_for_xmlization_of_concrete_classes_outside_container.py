"""Generate the test code for the xmlization of classes outside a container."""

import io
import os
import pathlib
import sys
import textwrap
from typing import List

import aas_core_codegen
import aas_core_codegen.common
import aas_core_codegen.csharp.naming
import aas_core_codegen.naming
import aas_core_codegen.parse
import aas_core_codegen.run
from aas_core_codegen import intermediate
from aas_core_codegen.common import Stripped

import aas_core_3_0_rc2_csharp_testgen.common


def main() -> int:
    """Execute the main routine."""
    symbol_table = aas_core_3_0_rc2_csharp_testgen.common.load_symbol_table()

    this_path = pathlib.Path(os.path.realpath(__file__))
    repo_root = this_path.parent.parent.parent

    test_data_dir = repo_root / "test_data"

    environment_cls = symbol_table.must_find_concrete_class(
        aas_core_codegen.common.Identifier("Environment"))

    # noinspection PyListCreation
    blocks = []  # type: List[str]

    for our_type in symbol_table.our_types:
        if not isinstance(our_type, intermediate.ConcreteClass):
            continue

        container_cls = aas_core_3_0_rc2_csharp_testgen.common.determine_container_class(
            cls=our_type, test_data_dir=test_data_dir,
            environment_cls=environment_cls)

        if container_cls is our_type:
            # NOTE (mristin, 2022-06-27):
            # These classes are tested already in TestXmlizationOfConcreteClasses.
            # We only need to test for class instances contained in a container.
            continue

        cls_name_csharp = aas_core_codegen.csharp.naming.class_name(our_type.name)

        blocks.append(
            Stripped(
                f"""\
[Test]
public void Test_round_trip_{cls_name_csharp}()
{{
    // We load from JSON here just to jump-start the round trip.
    // The round-trip goes then over XML.
    var instance = Aas.Tests.CommonJsonization.LoadComplete{cls_name_csharp}();
    
    // The round-trip starts here.
    var outputBuilder = new System.Text.StringBuilder();
    
    // Serialize to XML
    {{
        using var xmlWriter = System.Xml.XmlWriter.Create(
            outputBuilder,
            new System.Xml.XmlWriterSettings()
            {{
                Encoding = System.Text.Encoding.UTF8,
                OmitXmlDeclaration = true
            }});

        Aas.Xmlization.Serialize.To(
            instance,
            xmlWriter);
    }}
    
    // De-serialize from XML
    string outputText = outputBuilder.ToString();

    using var outputReader = new System.IO.StringReader(outputText);

    using var xmlReader = System.Xml.XmlReader.Create(
        outputReader,
        new System.Xml.XmlReaderSettings());

    var anotherInstance = Aas.Xmlization.Deserialize.{cls_name_csharp}From(
        xmlReader);

    // Serialize back to XML
    var anotherOutputBuilder = new System.Text.StringBuilder();

    {{
        using var anotherXmlWriter = System.Xml.XmlWriter.Create(
            anotherOutputBuilder,
            new System.Xml.XmlWriterSettings()
            {{
                Encoding = System.Text.Encoding.UTF8,
                OmitXmlDeclaration = true
            }});

        Aas.Xmlization.Serialize.To(
            anotherInstance,
            anotherXmlWriter);
    }}
    
    // Compare
    Assert.AreEqual(outputText, anotherOutputBuilder.ToString());
}}  // public void Test_round_trip_{cls_name_csharp}"""
            )
        )

    writer = io.StringIO()
    writer.write(
        """\
/*
 * This code has been automatically generated by testgen.
 * Do NOT edit or append.
 */

using Aas = AasCore.Aas3_0_RC02;  // renamed

using NUnit.Framework; // can't alias

namespace AasCore.Aas3_0_RC02.Tests
{
    /// <summary>
    /// Test de/serialization of classes contained in a container <i>outside</i>
    /// of that container.
    /// </summary>
    /// <remarks>
    /// This is necessary so that we also test the methods that directly de/serialize
    /// an instance in rare use cases where it does not reside within a container such
    /// as <see cref="Aas.Environment" />.
    /// </remarks>
    public class TestXmlizationOfConcreteClassesOutsideContainer
    {
"""
    )

    for i, block in enumerate(blocks):
        if i > 0:
            writer.write("\n\n")

        writer.write(textwrap.indent(block, "        "))

    writer.write(
        """
    }  // class TestXmlizationOfConcreteClassesOutsideContainer
}  // namespace AasCore.Aas3_0_RC02.Tests

/*
 * This code has been automatically generated by testgen.
 * Do NOT edit or append.
 */
"""
    )

    target_pth = (
            repo_root /
            "src/AasCore.Aas3_0_RC02.Tests" /
            "TestXmlizationOfConcreteClassesOutsideContainer.cs"
    )
    target_pth.write_text(writer.getvalue(), encoding='utf-8')

    return 0


if __name__ == "__main__":
    sys.exit(main())
