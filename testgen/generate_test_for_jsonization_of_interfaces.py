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
            cls_name_csharp = aas_core_codegen.csharp.naming.class_name(cls.name)
            cls_name_json = aas_core_codegen.naming.json_model_type(cls.name)

            blocks.append(
                Stripped(
                    f"""\
[Test]
public void Test_round_trip_{interface_name_csharp}_from_{cls_name_csharp}()
{{
    var instance = Aas.Tests.CommonJsonization.LoadComplete{cls_name_csharp}();

    var jsonObject = Aas.Jsonization.Serialize.ToJsonObject(instance);

    var anotherInstance = Aas.Jsonization.Deserialize.{interface_name_csharp}From(
        jsonObject);

    var anotherJsonObject = Aas.Jsonization.Serialize.ToJsonObject(
        anotherInstance);
            
    Aas.Tests.CommonJson.CheckJsonNodesEqual(
        jsonObject,
        anotherJsonObject,
        out Aas.Reporting.Error? error);

    if (error != null)
    {{
        Assert.Fail(
            "When we serialize the complete instance of {cls_name_csharp} " +
            "as {interface_name_csharp}, we get an error in the round trip: " +
            $"{{Reporting.GenerateJsonPath(error.PathSegments)}}: " +
            error.Cause
        );
    }}
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

using NUnit.Framework;  // can't alias

using Aas = AasCore.Aas3_0_RC02;  // renamed

namespace AasCore.Aas3_0_RC02.Tests
{
    public class TestJsonizationOfInterfaces
    {
"""
    )

    for i, block in enumerate(blocks):
        if i > 0:
            writer.write("\n\n")

        writer.write(textwrap.indent(block, "        "))

    writer.write(
        """
    }  // class TestJsonizationOfInterfaces
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
            repo_root / "src/AasCore.Aas3_0_RC02.Tests/TestJsonizationOfInterfaces.cs"
    )
    target_pth.write_text(writer.getvalue(), encoding='utf-8')

    return 0


if __name__ == "__main__":
    sys.exit(main())
