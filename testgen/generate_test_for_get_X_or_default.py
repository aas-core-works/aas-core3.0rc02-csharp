"""Generate the test code for the ``XOrDefault`` methods."""

import io
import os
import pathlib
import sys
import textwrap
from typing import List, Optional

import aas_core_codegen
import aas_core_codegen.common
import aas_core_codegen.csharp.naming
import aas_core_codegen.naming
import aas_core_codegen.parse
import aas_core_codegen.run
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

    blocks.append(
        Stripped(
            """\
private static void CompareOrRerecordValue(
    object value,
    string expectedPath)
{
    Nodes.JsonNode got = Aas.Tests.CommonJson.ToJson(
        value);
    
    if (Aas.Tests.Common.RecordMode)
    {
        string? parent = Path.GetDirectoryName(expectedPath);
        if (parent != null)
        {
            if (!Directory.Exists(parent))
            {
                Directory.CreateDirectory(parent);
            }
        }

        System.IO.File.WriteAllText(
            expectedPath, got.ToJsonString());
    }
    else
    {
        if (!System.IO.File.Exists(expectedPath))
        {
            throw new System.IO.FileNotFoundException(
                $"The file with the recorded value does not exist: {expectedPath}");
        }

        Nodes.JsonNode expected = Aas.Tests.CommonJson.ReadFromFile(
            expectedPath);

        Aas.Tests.CommonJson.CheckJsonNodesEqual(
            expected, got, out Aas.Reporting.Error? error);

        if (error != null)
        {
            Assert.Fail(
                $"The original value from {expectedPath} is unequal the obtain value " +
                "when serialized to JSON: " +
                $"{Reporting.GenerateJsonPath(error.PathSegments)}: " +
                error.Cause
            );
        }
    }
}"""
        )
    )

    for symbol in symbol_table.symbols:
        if not isinstance(symbol, intermediate.ConcreteClass):
            continue

        cls_name_csharp = aas_core_codegen.csharp.naming.class_name(symbol.name)
        cls_name_json = aas_core_codegen.naming.json_model_type(symbol.name)

        x_or_default_methods = []  # type: List[intermediate.Method]
        for method in symbol.methods:
            if method.name.endswith("_or_default"):
                x_or_default_methods.append(method)

        for method in x_or_default_methods:
            method_name_csharp = aas_core_codegen.csharp.naming.method_name(method.name)

            result_enum = None  # type: Optional[intermediate.Enumeration]
            assert method.returns is not None, (
                f"Expected all X_or_default to return something, "
                f"but got None for {symbol}.{method.name}"
            )

            if (
                    isinstance(method.returns, intermediate.OurTypeAnnotation)
                    and isinstance(method.returns.symbol, intermediate.Enumeration)
            ):
                result_enum = method.returns.symbol

            if result_enum is None:
                value_assignment_snippet = Stripped(
                    f"var value = instance.{method_name_csharp}();")
            else:
                value_assignment_snippet = Stripped(
                    f"""\
string value = Aas.Stringification.ToString(
    instance.{method_name_csharp}())
        ?? throw new System.InvalidOperationException(
            "Failed to stringify the enum");"""
                )

            indent = "    "

            # noinspection SpellCheckingInspection
            blocks.append(
                Stripped(
                    f"""\
[Test]
public void Test_{cls_name_csharp}_{method_name_csharp}_non_default()
{{
    Aas.{cls_name_csharp} instance = (
        Aas.Tests.CommonJsonization.LoadComplete{cls_name_csharp}());
    
    {aas_core_codegen.common.indent_but_first_line(value_assignment_snippet, indent)}

    CompareOrRerecordValue(
        value, 
        Path.Combine(
            Aas.Tests.Common.TestDataDir,
            "XOrDefault",
            {csharp_common.string_literal(cls_name_json)},
            "{method_name_csharp}.non-default.json"));
}}  // public void Test_{cls_name_csharp}_{method_name_csharp}_non_default

[Test]
public void Test_{cls_name_csharp}_{method_name_csharp}_default()
{{
    Aas.{cls_name_csharp} instance = (
        Aas.Tests.CommonJsonization.LoadMinimal{cls_name_csharp}());

    {aas_core_codegen.common.indent_but_first_line(value_assignment_snippet, indent)}

    CompareOrRerecordValue(
        value, 
        Path.Combine(
            Aas.Tests.Common.TestDataDir,
            "XOrDefault",
            {csharp_common.string_literal(cls_name_json)},
            "{method_name_csharp}.default.json"));
}}  // public void Test_{cls_name_csharp}_{method_name_csharp}_default"""
                )
            )

    writer = io.StringIO()
    writer.write(
        """\
/*
 * This code has been automatically generated by testgen.
 * Do NOT edit or append.
 */

using Directory = System.IO.Directory;
using Nodes = System.Text.Json.Nodes;
using Path = System.IO.Path;

using NUnit.Framework;  // can't alias

using Aas = AasCore.Aas3_0_RC02;  // renamed

namespace AasCore.Aas3_0_RC02.Tests
{
    public class TestXOrDefault
    {
"""
    )

    for i, block in enumerate(blocks):
        if i > 0:
            writer.write("\n\n")

        writer.write(textwrap.indent(block, "        "))

    writer.write(
        """
    }  // class TestXOrDefault
}  // namespace AasCore.Aas3_0_RC02.Tests

/*
 * This code has been automatically generated by testgen.
 * Do NOT edit or append.
 */
"""
    )

    this_path = pathlib.Path(os.path.realpath(__file__))
    repo_root = this_path.parent.parent

    target_pth = repo_root / "src/AasCore.Aas3_0_RC02.Tests/TestXOrDefault.cs"
    target_pth.write_text(writer.getvalue(), encoding='utf-8')

    return 0


if __name__ == "__main__":
    sys.exit(main())
