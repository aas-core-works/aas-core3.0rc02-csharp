import io
import pathlib

import aas_core_codegen
import aas_core_codegen.common
import aas_core_codegen.naming
import aas_core_codegen.parse
import aas_core_codegen.run
import aas_core_meta.v3rc2
from aas_core_codegen import intermediate
from icontract import require


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


@require(lambda test_data_dir: test_data_dir.is_dir())
@require(lambda environment_cls: environment_cls.name == "Environment")
def determine_container_class(
        cls: intermediate.ConcreteClass,
        test_data_dir: pathlib.Path,
        environment_cls: intermediate.ConcreteClass
) -> intermediate.ConcreteClass:
    """Determine the container class based on the test data directory."""
    cls_name_json = aas_core_codegen.naming.json_model_type(cls.name)

    contained_in_environment_path = (
            test_data_dir
            / "Json"
            / "ContainedInEnvironment"
            / "Expected"
            / cls_name_json
    )

    self_contained_path = (
            test_data_dir
            / "Json"
            / "SelfContained"
            / "Expected"
            / cls_name_json
    )

    if contained_in_environment_path.exists():
        return environment_cls
    elif self_contained_path.exists():
        return cls
    else:
        raise RuntimeError(
            f"Neither {contained_in_environment_path} nor {self_contained_path} "
            f"exist beneath {test_data_dir}. We do not know how to infer "
            f"the kind of how the instance of {cls_name_json} is contained."
        )
