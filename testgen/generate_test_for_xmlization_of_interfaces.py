"""Generate the test code for the JSON de/serialization of interfaces."""

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
import aas_core_meta.v3rc2

from aas_core_codegen import intermediate
from aas_core_codegen.common import Stripped
from aas_core_codegen.csharp import (
    common as csharp_common
)

from testgen.common import load_symbol_table


def main() -> int:
    """Execute the main routine."""
    symbol_table = load_symbol_table()

    # noinspection PyListCreation
    blocks = []  # type: List[str]

    for symbol in symbol_table.symbols:
        if not isinstance(symbol, intermediate.Class):
            continue

        if symbol.interface is None or len(symbol.interface.implementers) == 0:
            continue

        if symbol.name == aas_core_codegen.common.Identifier("Event_payload"):
            # NOTE (mristin, 2022-06-21):
            # Event payload is a dangling class and can not be reached from
            # the environment. Hence, we skip it.
            continue

        for cls in symbol.interface.implementers:
            if cls.serialization is None or not cls.serialization.with_model_type:
                continue

            interface_name_csharp = aas_core_codegen.csharp.naming.interface_name(
                symbol.interface.name
            )

            assert interface_name_csharp.startswith("I")
            var_name = aas_core_codegen.csharp.naming.variable_name(
                aas_core_codegen.common.Identifier(
                    "the_" + interface_name_csharp[1:]
                )
            )
            another_var_name = aas_core_codegen.csharp.naming.variable_name(
                aas_core_codegen.common.Identifier(
                    "another_" + interface_name_csharp[1:]
                )
            )

            cls_name_csharp = aas_core_codegen.csharp.naming.class_name(cls.name)
            cls_name_json = aas_core_codegen.naming.json_model_type(cls.name)

            blocks.append(
                Stripped(
                    f"""\
[Test]
public void Test_round_trip_{interface_name_csharp}_from_{cls_name_csharp}()
{{
    // We load from JSON here just to jump-start the round trip.
    // The round-trip goes then over XML.
    string pathToCompleteExample = Path.Combine(
        AasCore.Aas3_0_RC02.Tests.Common.OurTestResourceDir,
        "Json",
        "Expected",
        "{cls_name_json}",
        "complete.json");

    var container = AasCore.Aas3_0_RC02.Tests.CommonJson.LoadInstance(
        pathToCompleteExample);

    var instance = (
        (container is {cls_name_csharp})
            ? container
            : container
                  .Descend()
                  .First(something => something is {cls_name_csharp})
              ?? throw new System.InvalidOperationException(
                  "No instance of {cls_name_csharp} could be found")
    );

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

        var {var_name} = (Aas.{cls_name_csharp})instance;

        AasCore.Aas3_0_RC02.Xmlization.Serialize.To(
            {var_name},
            xmlWriter,
            "aas",
            "https://www.admin-shell.io/aas/3/0/RC02");
    }}

    // De-serialize from XML
    string outputText = outputBuilder.ToString();

    using var outputReader = new System.IO.StringReader(outputText);

    using var xmlReader = System.Xml.XmlReader.Create(
        outputReader,
        new System.Xml.XmlReaderSettings());

    var {another_var_name} = Aas.Xmlization.Deserialize.{interface_name_csharp}From(
        xmlReader,
        "https://www.admin-shell.io/aas/3/0/RC02");

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

        AasCore.Aas3_0_RC02.Xmlization.Serialize.To(
            {another_var_name},
            anotherXmlWriter,
            "aas",
            "https://www.admin-shell.io/aas/3/0/RC02");
    }}


    // Compare
    Assert.AreEqual(outputText, anotherOutputBuilder.ToString());
}}  // void Test_round_trip_{interface_name_csharp}_from_{cls_name_csharp}"""
                )
            )

    writer = io.StringIO()
    writer.write(
        """\
/*
 * This code has been automatically generated by testgen.
 * Do NOT edit or append.
 */

using Path = System.IO.Path;

using NUnit.Framework;  // can't alias
using System.Linq; // can't alias

using Aas = AasCore.Aas3_0_RC02;

namespace AasCore.Aas3_0_RC02.Tests
{
    public class TestXmlizationOfInterfaces
    {
"""
    )

    for i, block in enumerate(blocks):
        if i > 0:
            writer.write("\n\n")

        writer.write(textwrap.indent(block, "        "))

    writer.write(
        """
    }  // class TestXmlizationOfInterfaces
}  // namespace AasCore.Aas3_0_RC02.Tests

/*
 * This code has been automatically generated by testgen.
 * Do NOT edit or append.
 */
"""
    )

    this_path = pathlib.Path(os.path.realpath(__file__))
    repo_root = this_path.parent.parent

    target_pth = (
            repo_root / "src/AasCore.Aas3_0_RC02.Tests/TestXmlizationOfInterfaces.cs"
    )
    target_pth.write_text(writer.getvalue(), encoding='utf-8')

    return 0


if __name__ == "__main__":
    sys.exit(main())
