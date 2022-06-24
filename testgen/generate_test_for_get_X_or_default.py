"""Generate the test code for the ``DescendOnce`` methods."""

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

    blocks.append(
        Stripped(
            """\
private static void CompareOrRerecordValue(
    object value,
    string expectedPath)
{
    Nodes.JsonNode got = AasCore.Aas3_0_RC02.Tests.CommonJson.ToJson(
        value);
    
    if (AasCore.Aas3_0_RC02.Tests.Common.RecordMode)
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

        Nodes.JsonNode expected = AasCore.Aas3_0_RC02.Tests.CommonJson.ReadFromFile(
            expectedPath);

        AasCore.Aas3_0_RC02.Tests.CommonJson.CheckJsonNodesEqual(
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

        if symbol.name == aas_core_codegen.common.Identifier("Event_payload"):
            # NOTE (mristin, 2022-06-21):
            # Event payload is a dangling class and can not be reached from
            # the environment. Hence, we skip it.
            continue

        cls_name_csharp = aas_core_codegen.csharp.naming.class_name(symbol.name)
        cls_name_json = aas_core_codegen.naming.json_model_type(symbol.name)

        var_name_csharp = aas_core_codegen.csharp.naming.variable_name(
            aas_core_codegen.common.Identifier("the_" + symbol.name))

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
                    f"var value = {var_name_csharp}.{method_name_csharp}();")
            else:
                value_assignment_snippet = Stripped(
                    f"""\
string value = Aas.Stringification.ToString(
    {var_name_csharp}.{method_name_csharp}())
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
    string pathToCompleteExample = Path.Combine(
        AasCore.Aas3_0_RC02.Tests.Common.OurTestResourceDir,
        "Json",
        "Expected",
        {csharp_common.string_literal(cls_name_json)},
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
    
    var {var_name_csharp} = (instance as Aas.{cls_name_csharp})
        ?? throw new System.InvalidOperationException(
            "Expected an instance of {cls_name_csharp} " +
            $"in {{pathToCompleteExample}}, " +
            $"but got a {{instance.GetType()}}");

    {aas_core_codegen.common.indent_but_first_line(value_assignment_snippet, indent)}

    CompareOrRerecordValue(
        value, 
        Path.Combine(
            AasCore.Aas3_0_RC02.Tests.Common.OurTestResourceDir,
            "XOrDefault",
            {csharp_common.string_literal(cls_name_json)},
            "{method_name_csharp}.non-default.json"));
}}  // public void Test_{cls_name_csharp}_{method_name_csharp}_non_default

[Test]
public void Test_{cls_name_csharp}_{method_name_csharp}_default()
{{
    string pathToMinimalExample = Path.Combine(
        AasCore.Aas3_0_RC02.Tests.Common.OurTestResourceDir,
        "Json",
        "Expected",
        {csharp_common.string_literal(cls_name_json)},
        "minimal.json");
    
    var container = AasCore.Aas3_0_RC02.Tests.CommonJson.LoadInstance(
        pathToMinimalExample);

    var instance = (
        (container is {cls_name_csharp})
        ? container
        : container
            .Descend()
            .First(something => something is {cls_name_csharp})
                ?? throw new System.InvalidOperationException(
                    "No instance of {cls_name_csharp} could be found") 
    );
    
    var {var_name_csharp} = (instance as Aas.{cls_name_csharp})
        ?? throw new System.InvalidOperationException(
            "Expected an instance of {cls_name_csharp} " +
            $"in {{pathToMinimalExample}}, " +
            $"but got a {{instance.GetType()}}");

    {aas_core_codegen.common.indent_but_first_line(value_assignment_snippet, indent)}

    CompareOrRerecordValue(
        value, 
        Path.Combine(
            AasCore.Aas3_0_RC02.Tests.Common.OurTestResourceDir,
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
using Aas = AasCore.Aas3_0_RC02;

using NUnit.Framework;  // can't alias
using System.Linq;  // can't alias

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
