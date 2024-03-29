"""Generate the test code for the ``OverXOrEmpty`` methods."""

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

from aas_core_3_0_rc2_csharp_testgen.common import load_symbol_table


def main() -> int:
    """Execute the main routine."""
    symbol_table = load_symbol_table()

    # noinspection PyListCreation
    blocks = []  # type: List[str]

    for our_type in symbol_table.our_types:
        if not isinstance(our_type, intermediate.ConcreteClass):
            continue

        cls_name_csharp = aas_core_codegen.csharp.naming.class_name(our_type.name)

        for prop in our_type.properties:
            if (
                    isinstance(
                        prop.type_annotation, intermediate.OptionalTypeAnnotation)
                    and isinstance(
                prop.type_annotation.value, intermediate.ListTypeAnnotation)
            ):
                method_name_csharp = aas_core_codegen.csharp.naming.method_name(
                    aas_core_codegen.common.Identifier(
                        f"Over_{prop.name}_or_empty"))

                # noinspection SpellCheckingInspection
                blocks.append(
                    Stripped(
                        f"""\
[Test]
public void Test_{cls_name_csharp}_{method_name_csharp}_non_default()
{{
    Aas.{cls_name_csharp} instance = (
        Aas.Tests.CommonJsonization.LoadComplete{cls_name_csharp}());
    
    int count = 0;
    foreach (var _ in instance.{method_name_csharp}())
    {{
        count++;
    }} 

    Assert.Greater(count, 0);
}}  // public void Test_{cls_name_csharp}_{method_name_csharp}_non_default

[Test]
public void Test_{cls_name_csharp}_{method_name_csharp}_default()
{{
    Aas.{cls_name_csharp} instance = (
        Aas.Tests.CommonJsonization.LoadMinimal{cls_name_csharp}());

    int count = 0;
    foreach (var _ in instance.{method_name_csharp}())
    {{
        count++;
    }} 

    Assert.AreEqual(0, count);
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

using Aas = AasCore.Aas3_0_RC02;  // renamed

using NUnit.Framework;  // can't alias

namespace AasCore.Aas3_0_RC02.Tests
{
    public class TestOverXOrEmpty
    {
"""
    )

    for i, block in enumerate(blocks):
        if i > 0:
            writer.write("\n\n")

        writer.write(textwrap.indent(block, "        "))

    writer.write(
        """
    }  // class TestOverXOrEmpty
}  // namespace AasCore.Aas3_0_RC02.Tests

/*
 * This code has been automatically generated by testgen.
 * Do NOT edit or append.
 */
"""
    )

    this_path = pathlib.Path(os.path.realpath(__file__))
    repo_root = this_path.parent.parent.parent

    target_pth = repo_root / "src/AasCore.Aas3_0_RC02.Tests/TestOverXOrEmpty.cs"
    target_pth.write_text(writer.getvalue(), encoding='utf-8')

    return 0


if __name__ == "__main__":
    sys.exit(main())
