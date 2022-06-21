"""Generate the test code for the ``DescendOnce`` methods."""

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


def load_symbol_table() -> intermediate.SymbolTable:
    """Load the symbol table from the meta-model."""
    model_path = pathlib.Path(aas_core_meta.v3rc2.__file__)
    assert model_path.exists() and model_path.is_file(), model_path

    text = model_path.read_text(encoding="utf-8")

    atok, parse_exception = aas_core_codegen.parse.source_to_atok(source=text)
    if parse_exception:
        if isinstance(parse_exception, SyntaxError):
            raise RuntimeError(
                f"Failed to parse the meta-model {model_path}: "
                f"invalid syntax at line {parse_exception.lineno}\n"
            )
        else:
            raise RuntimeError(
                f"Failed to parse the meta-model {model_path}: " f"{parse_exception}\n"
            )

    import_errors = aas_core_codegen.parse.check_expected_imports(atok=atok)
    if import_errors:
        writer = io.StringIO()
        aas_core_codegen.run.write_error_report(
            message="One or more unexpected imports in the meta-model",
            errors=import_errors,
            stderr=writer,
        )

        raise RuntimeError(writer.getvalue())

    lineno_columner = aas_core_codegen.common.LinenoColumner(atok=atok)

    parsed_symbol_table, error = aas_core_codegen.parse.atok_to_symbol_table(atok=atok)
    if error is not None:
        writer = io.StringIO()
        aas_core_codegen.run.write_error_report(
            message=f"Failed to construct the symbol table from {model_path}",
            errors=[lineno_columner.error_message(error)],
            stderr=writer,
        )

        raise RuntimeError(writer.getvalue())

    assert parsed_symbol_table is not None

    ir_symbol_table, error = intermediate.translate(
        parsed_symbol_table=parsed_symbol_table,
        atok=atok,
    )
    if error is not None:
        writer = io.StringIO()
        aas_core_codegen.run.write_error_report(
            message=f"Failed to translate the parsed symbol table "
                    f"to intermediate symbol table "
                    f"based on {model_path}",
            errors=[lineno_columner.error_message(error)],
            stderr=writer,
        )

        raise RuntimeError(writer.getvalue())

    assert ir_symbol_table is not None

    return ir_symbol_table


def main() -> int:
    """Execute the main routine."""
    symbol_table = load_symbol_table()

    # noinspection PyListCreation
    blocks = []  # type: List[str]

    blocks.append(
        Stripped(
            """\
private static Environment LoadEnvironment(
    string path)
{
    Environment? environment;

    {
        using var stream = new FileStream(path, FileMode.Open);
        var node = Nodes.JsonNode.Parse(stream)
                   ?? throw new System.InvalidOperationException(
                       "node unexpectedly null");
    
        environment = AasCore.Aas3_0_RC02.Jsonization.Deserialize.EnvironmentFrom(
            node);
    }
    
    if (environment == null)
    {
        throw new System.InvalidOperationException(
            "environment unexpectedly null");
    }
    
    return environment;
}"""
        )
    )

    blocks.append(
        Stripped(
            """\
private static void CompareOrRerecordTrace(
    IClass instance,
    string expectedPath)
{
    var writer = new System.IO.StringWriter();
    foreach (var descendant in instance.DescendOnce())
    {
        switch (descendant)
        {
            case IIdentifiable identifiable:
                {
                    writer.WriteLine(
                        $"{identifiable.GetType()} with ID {identifiable.Id}");
                    break;
                }
            case IReferable referable:
                {
                    writer.WriteLine(
                        $"{referable.GetType()} with ID-short {referable.IdShort}");
                    break;
                }
            default:
                {
                    writer.WriteLine(descendant.GetType().Name);
                    break;
                }
        }
    }

    string got = writer.ToString();

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

        System.IO.File.WriteAllText(expectedPath, got);
    }
    else
    {
        if (!System.IO.File.Exists(expectedPath))
        {
            throw new System.IO.FileNotFoundException(
                $"The file with the recorded trace does not exist: {expectedPath}");
        }

        string expected = System.IO.File.ReadAllText(expectedPath);
        Assert.AreEqual(
            expected,
            got,
            $"The expected trace from {expectedPath} does not match the actual one");
    }
}"""
        )
    )

    environment_cls = symbol_table.must_find(
        aas_core_codegen.common.Identifier("Environment"))
    assert isinstance(environment_cls, intermediate.ConcreteClass)

    for symbol in symbol_table.symbols:
        if not isinstance(symbol, intermediate.ConcreteClass):
            continue

        if symbol.name == aas_core_codegen.common.Identifier("Submodel_element_list"):
            # NOTE (mristin, 2022-06-21):
            # Submodel element list is currently a blind spot in the test data.
            # We have to manually generate it, but just didn't find the time so far.
            continue

        if symbol.name == aas_core_codegen.common.Identifier("Event_payload"):
            # NOTE (mristin, 2022-06-21):
            # Event payload is a dangling class and can not be reached from
            # the environment. Hence, we skip it.
            continue

        cls_name_csharp = aas_core_codegen.csharp.naming.class_name(symbol.name)
        cls_name_json = aas_core_codegen.naming.json_model_type(symbol.name)

        if symbol is environment_cls:
            blocks.append(
                Stripped(
                    f"""\
[Test]
public void Test_{cls_name_csharp}()
{{
    string pathToCompleteExample = Path.Combine(
        AasCore.Aas3_0_RC02.Tests.Common.OurTestResourceDir,
        "Json",
        "Expected",
        {csharp_common.string_literal(cls_name_json)},
        "complete.json");

    var environment = LoadEnvironment(
        pathToCompleteExample);

    CompareOrRerecordTrace(
        environment,
        Path.Combine(
            AasCore.Aas3_0_RC02.Tests.Common.OurTestResourceDir,
            "DescendOnce",
            {csharp_common.string_literal(cls_name_json)},
            "complete.json.trace"));
}}  // public void Test_{cls_name_csharp}"""
                )
            )
        else:
            blocks.append(
                Stripped(
                    f"""\
[Test]
public void Test_{cls_name_csharp}()
{{
    string pathToCompleteExample = Path.Combine(
        AasCore.Aas3_0_RC02.Tests.Common.OurTestResourceDir,
        "Json",
        "Expected",
        {csharp_common.string_literal(cls_name_json)},
        "complete.json");
    
    var environment = LoadEnvironment(
        pathToCompleteExample);

    var instance = (
        environment
                .Descend()
                .First(something => something is {cls_name_csharp})
        ?? throw new System.InvalidOperationException(
            "No instance of {cls_name_csharp} could be found") 
    );
    
    CompareOrRerecordTrace(
        instance,
        Path.Combine(
            AasCore.Aas3_0_RC02.Tests.Common.OurTestResourceDir,
            "DescendOnce",
            {csharp_common.string_literal(cls_name_json)},
            "complete.json.trace"));
}}  // public void Test_{cls_name_csharp}"""
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
using FileMode = System.IO.FileMode;
using FileStream = System.IO.FileStream;
using Nodes = System.Text.Json.Nodes;
using Path = System.IO.Path;

using System.Linq; // can't alias
using NUnit.Framework; // can't alias

namespace AasCore.Aas3_0_RC02.Tests
{
    public class TestDescendOnce
    {
"""
    )

    for i, block in enumerate(blocks):
        if i > 0:
            writer.write("\n\n")

        writer.write(textwrap.indent(block, "        "))

    writer.write(
        """
    }  // class TestDescendOnce
}  // namespace AasCore.Aas3_0_RC02.Tests

/*
 * This code has been automatically generated by testgen.
 * Do NOT edit or append.
 */
"""
    )

    this_path = pathlib.Path(os.path.realpath(__file__))
    repo_root = this_path.parent.parent

    target_pth = repo_root / "src/AasCore.Aas3_0_RC02.Tests/TestDescendOnce.cs"
    target_pth.write_text(writer.getvalue(), encoding='utf-8')

    return 0


if __name__ == "__main__":
    sys.exit(main())
