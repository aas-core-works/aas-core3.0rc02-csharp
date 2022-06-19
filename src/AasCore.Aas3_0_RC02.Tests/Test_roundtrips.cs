using CodeAnalysis = System.Diagnostics.CodeAnalysis;
using Directory = System.IO.Directory;
using FileMode = System.IO.FileMode;
using FileStream = System.IO.FileStream;
using Nodes = System.Text.Json.Nodes;
using JsonException = System.Text.Json.JsonException;
using Path = System.IO.Path;
using StringBuilder = System.Text.StringBuilder;
using System.Collections.Generic;
using System.IO; // can't alias
using System.Linq; // can't alias
using NUnit.Framework; // can't alias


namespace AasCore.Aas3_0_RC02.Tests
{
    [CodeAnalysis.SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Test_round_trips
    {
        private static Nodes.JsonNode ReadFromFile(string path)
        {
            using var stream = new FileStream(path, FileMode.Open);
            Nodes.JsonNode? node;
            try
            {
                node = Nodes.JsonNode.Parse(stream);
            }
            catch (JsonException exception)
            {
                throw new System.InvalidOperationException(
                    $"Expected the file to be a valid JSON, but it was not: {path}; exception was: {exception}"
                );
            }

            if (node is null)
            {
                throw new System.InvalidOperationException(
                    $"Expected the file to be a non-null JSON value, but it was null: {path}"
                );
            }

            return node;
        }

        /// <summary>
        /// List the JSON files in the "Expected" directory. 
        /// </summary>
        private static IEnumerable<string> ExpectedPaths()
        {
            string expectedDir = Path.Combine(
                TestContext.CurrentContext.TestDirectory,
                Path.Join(
                    "TestResources"),
                $"{nameof(AasCore)}.{nameof(Aas3_0_RC02)}.{nameof(Tests)}",
                "Json",
                "Expected");

            if (!Directory.Exists(expectedDir))
            {
                throw new System.InvalidOperationException(
                    "The directory with the expected files does not exist " +
                    $"or is not a directory: {expectedDir}"
                );
            }

            var paths = Directory.GetFiles(
                expectedDir,
                "*.json",
                System.IO.SearchOption.AllDirectories).ToList();
            paths.Sort();

            return paths;
        }

        /// <summary>
        /// Infer the node kind of the JSON node.
        /// </summary>
        /// <remarks>
        /// This function is necessary since NET6 does not fully support node kinds yet.
        /// See:
        /// <ul>
        /// <li>https://github.com/dotnet/runtime/issues/53406</li>
        /// <li>https://github.com/dotnet/runtime/issues/55827</li>
        /// <li>https://github.com/dotnet/runtime/issues/56592</li>
        /// </ul>
        /// </remarks>
        private static string GetNodeKind(Nodes.JsonNode node)
        {
            switch (node)
            {
                case Nodes.JsonArray _:
                    return "array";
                case Nodes.JsonObject _:
                    return "object";
                case Nodes.JsonValue _:
                    return "value";
                default:
                    throw new System.InvalidOperationException(
                        $"Unhandled JsonNode: {node.GetType()}");
            }
        }

        private static void CheckJsonNodesEqual(
            Nodes.JsonNode that,
            Nodes.JsonNode other,
            out Reporting.Error? error)
        {
            error = null;

            var thatNodeKind = GetNodeKind(that);
            var otherNodeKind = GetNodeKind(other);

            if (thatNodeKind != otherNodeKind)
            {
                error = new Reporting.Error(
                    $"Mismatch in node kinds : {thatNodeKind} != {otherNodeKind}"
                );
                return;
            }

            switch (that)
            {
                case Nodes.JsonArray thatArray:
                    {
                        var otherArray = (other as Nodes.JsonArray)!;
                        if (thatArray.Count != otherArray.Count)
                        {
                            error = new Reporting.Error(
                                $"Unequal array lengths: {thatArray.Count} != {otherArray.Count}"
                            );
                            return;
                        }

                        for (int i = 0; i < thatArray.Count; i++)
                        {
                            CheckJsonNodesEqual(thatArray[i]!, otherArray[i]!, out error);
                            if (error != null)
                            {
                                error.PrependSegment(new Reporting.IndexSegment(i));
                                return;
                            }
                        }

                        break;
                    }
                case Nodes.JsonObject thatObject:
                    {
                        var thatDictionary = thatObject as IDictionary<string, Nodes.JsonNode>;
                        var otherDictionary = (other as IDictionary<string, Nodes.JsonNode>)!;

                        var thatKeys = thatDictionary.Keys.ToList();
                        thatKeys.Sort();

                        var otherKeys = otherDictionary.Keys.ToList();
                        otherKeys.Sort();

                        if (!thatKeys.SequenceEqual(otherKeys))
                        {
                            error = new Reporting.Error(
                                "Objects with different properties: " +
                                $"{string.Join(", ", thatKeys)} != " +
                                $"{string.Join(", ", otherKeys)}"
                            );
                            return;
                        }

                        foreach (var key in thatKeys)
                        {
                            CheckJsonNodesEqual(thatDictionary[key], otherDictionary[key], out error);
                            if (error != null)
                            {
                                error.PrependSegment(new Reporting.NameSegment(key));
                                return;
                            }
                        }

                        break;
                    }
                case Nodes.JsonValue thatValue:
                    {
                        string thatAsJsonString = thatValue.ToJsonString();

                        // NOTE (mristin, 2022-05-13):
                        // This is slow, but there is no way around it at the moment with NET6.
                        // See:
                        // * https://github.com/dotnet/runtime/issues/56592
                        // * https://github.com/dotnet/runtime/issues/55827
                        // * https://github.com/dotnet/runtime/issues/53406
                        var otherValue = (other as Nodes.JsonValue)!;
                        string otherAsJsonString = otherValue.ToJsonString();

                        if (thatAsJsonString != otherAsJsonString)
                        {
                            error = new Reporting.Error(
                                $"Unequal values: {thatAsJsonString} != {otherAsJsonString}"
                            );
                            // ReSharper disable once RedundantJumpStatement
                            return;
                        }

                        break;
                    }
                default:
                    throw new System.InvalidOperationException(
                        $"Unhandled JSON node: {that.GetType()}"
                    );
            }
        }

        [Test]
        public void Test_json_deserialize_verify_json_serialize_equal()
        {
            foreach (string path in ExpectedPaths())
            {
                var originalNode = ReadFromFile(path);
                AasCore.Aas3_0_RC02.Environment? instance = null;
                try
                {
                    instance = AasCore.Aas3_0_RC02.Jsonization.Deserialize.EnvironmentFrom(
                        originalNode);
                }
                catch (AasCore.Aas3_0_RC02.Jsonization.Exception exception)
                {
                    Assert.Fail(
                        "Expected no exception upon de-serialization of an environment " +
                        $"from {path}, " +
                        $"but got: {exception}"
                    );
                }

                if (instance is null)
                {
                    throw new System.InvalidOperationException(
                        "Expected the instance to be set"
                    );
                }

                var errors = AasCore.Aas3_0_RC02.Verification.Verify(instance).ToList();
                if (errors.Count > 0)
                {
                    var builder = new StringBuilder();
                    builder.Append(
                        $"Expected no errors when verifying the instance de-serialized from {path}, " +
                        $"but got {errors.Count} error(s):\n");
                    for (var i = 0; i < errors.Count; i++)
                    {
                        builder.Append(
                            $"Error {i + 1}:\n" +
                            $"{Reporting.GenerateJsonPath(errors[i].PathSegments)}: {errors[i].Cause}\n");
                    }

                    Assert.Fail(builder.ToString());
                }

                Nodes.JsonObject? serialized = null;
                try
                {
                    serialized = AasCore.Aas3_0_RC02.Jsonization.Serialize.ToJsonObject(instance);
                }
                catch (System.Exception exception)
                {
                    Assert.Fail(
                        "Expected no exception upon serialization of an instance " +
                        $"de-serialized from {path}, but got: {exception}"
                    );
                }

                if (serialized == null)
                {
                    Assert.Fail(
                        $"Unexpected null serialization of an instance from {path}"
                    );
                }
                else
                {
                    CheckJsonNodesEqual(
                        originalNode,
                        serialized,
                        out Reporting.Error? inequalityError);
                    if (inequalityError != null)
                    {
                        Assert.Fail(
                            $"The original JSON from {path} is unequal the serialized JSON: " +
                            $"{Reporting.GenerateJsonPath(inequalityError.PathSegments)}: " +
                            inequalityError.Cause
                        );
                    }
                }
            }
        }

        // NOTE (mristin, 2022-05-15):
        // It is tedious to record manually all the expected error messages. Therefore we include this variable
        // to steer the automatic recording. We intentionally inter-twine the recording code with the test code
        // to keep them close to each other so that they are easier to maintain.
        private static readonly bool RecordMode = (
            System.Environment.GetEnvironmentVariable("AAS_CORE_AAS3_0_RC02_TESTS_RECORD_MODE")?.ToLower() == "true"
        ) || true; // TODO (mristin, 2022-05-15): undo

        /// <summary>
        /// List the JSON files in the "Unexpected" directory for which
        /// the deserialization must fail. 
        /// </summary>
        private static IEnumerable<string> PathsWithFailedDeserializations()
        {
            var dirNames = new List<string>()
            {
                "TypeViolation",
                "RequiredViolation"
            };

            var paths = new List<string>();

            foreach (string dirName in dirNames)
            {
                string unexpectedDir = Path.Combine(
                    TestContext.CurrentContext.TestDirectory,
                    Path.Join(
                        "TestResources"),
                    $"{nameof(AasCore)}.{nameof(Aas3_0_RC02)}.{nameof(Tests)}",
                    "Json",
                    "Unexpected",
                    dirName);

                if (!Directory.Exists(unexpectedDir))
                {
                    throw new System.InvalidOperationException(
                        "The directory with the unexpected files does not exist " +
                        $"or is not a directory: {unexpectedDir}"
                    );
                }

                paths.AddRange(Directory.GetFiles(
                        unexpectedDir,
                        "*.json",
                        System.IO.SearchOption.AllDirectories).ToList()
                );
            }

            paths.Sort();

            return paths;
        }

        [Test]
        public void Test_json_deserialize_fail()
        {
            foreach (string path in PathsWithFailedDeserializations())
            {
                var node = ReadFromFile(path);

                AasCore.Aas3_0_RC02.Jsonization.Exception? exception = null;

                try
                {
                    var _ = AasCore.Aas3_0_RC02.Jsonization.Deserialize.EnvironmentFrom(
                        node);
                }
                catch (AasCore.Aas3_0_RC02.Jsonization.Exception observedException)
                {
                    exception = observedException;
                }

                if (exception == null)
                {
                    Assert.Fail(
                        $"Expected a Jsonization exception when de-serializing {path}, but got none."
                    );
                }
                else
                {
                    string exceptionPath = path + ".exception";
                    string got = exception.Message;
                    if (RecordMode)
                    {
                        System.IO.File.WriteAllText(exceptionPath, got);
                    }
                    else
                    {
                        if (!System.IO.File.Exists(exceptionPath))
                        {
                            throw new FileNotFoundException(
                                $"The file with the recorded exception does not exist: {exceptionPath}");
                        }

                        string expected = System.IO.File.ReadAllText(exceptionPath);
                        Assert.AreEqual(
                            expected,
                            got,
                            $"The expected exception does not match the actual one for the file {path}");
                    }
                }
            }
        }

        /// <summary>
        /// List the JSON files in the "Unexpected" directory for which
        /// the deserialization must fail. 
        /// </summary>
        private static IEnumerable<string> PathsWithFailedVerifications()
        {
            var dirNames = new List<string>()
            {
                "DateTimeStampUtcViolationOnFebruary29th",
                "MaxLengthViolation",
                "MinLengthViolation",
                "PatternViolation",
                "Property",
                "Range"
            };

            var paths = new List<string>();

            foreach (string dirName in dirNames)
            {
                string unexpectedDir = Path.Combine(
                    TestContext.CurrentContext.TestDirectory,
                    Path.Join(
                        "TestResources"),
                    $"{nameof(AasCore)}.{nameof(Aas3_0_RC02)}.{nameof(Tests)}",
                    "Json",
                    "Unexpected",
                    dirName);

                if (!Directory.Exists(unexpectedDir))
                {
                    throw new System.InvalidOperationException(
                        "The directory with the unexpected files does not exist " +
                        $"or is not a directory: {unexpectedDir}"
                    );
                }

                paths.AddRange(Directory.GetFiles(
                        unexpectedDir,
                        "*.json",
                        System.IO.SearchOption.AllDirectories).ToList()
                );
            }

            paths.Sort();

            return paths;
        }

        [Test]
        public void Test_json_deserialize_verify_fail()
        {
            foreach (string path in PathsWithFailedVerifications())
            {
                var node = ReadFromFile(path);
                AasCore.Aas3_0_RC02.Environment? instance = null;
                try
                {
                    instance = AasCore.Aas3_0_RC02.Jsonization.Deserialize.EnvironmentFrom(
                        node);
                }
                catch (AasCore.Aas3_0_RC02.Jsonization.Exception exception)
                {
                    Assert.Fail(
                        "Expected no exception upon de-serialization of an environment " +
                        $"from {path}, " +
                        $"but got: {exception}"
                    );
                }

                if (instance is null)
                {
                    throw new System.InvalidOperationException(
                        "Expected the instance to be set"
                    );
                }

                var errors = AasCore.Aas3_0_RC02.Verification.Verify(instance).ToList();
                if (errors.Count == 0)
                {
                    Assert.Fail(
                        $"Expected at least one verification error when verifying {path}, but got none");
                }

                string got = string.Join(
                    ";\n",
                    errors.Select(
                        error => $"{Reporting.GenerateJsonPath(error.PathSegments)}: {error.Cause}"));

                string errorsPath = path + ".errors";
                if (RecordMode)
                {
                    System.IO.File.WriteAllText(errorsPath, got);
                }
                else
                {
                    if (!System.IO.File.Exists(errorsPath))
                    {
                        throw new FileNotFoundException(
                            $"The file with the recorded errors does not exist: {errorsPath}");
                    }

                    string expected = System.IO.File.ReadAllText(errorsPath);
                    Assert.AreEqual(
                        expected,
                        got,
                        $"The expected verification errors do not match the actual ones for the file {path}");
                }
            }
        }

        // Continue here:
        // TODO (mristin, 2022-05-15): test descendants in a separate test file

        // TODO (mristin, 2022-05-06): serialize to XML, deserialize from XML, serialize roundTrip to JSON, compare

        // TODO (mristin, 2022-05-06): one-off: convert expected to XML, manually inspect XML, copy/paste this file for XML  


    }
}