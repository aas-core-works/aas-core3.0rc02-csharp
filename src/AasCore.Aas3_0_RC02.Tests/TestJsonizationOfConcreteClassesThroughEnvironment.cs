using Directory = System.IO.Directory;
using Nodes = System.Text.Json.Nodes;
using Path = System.IO.Path;
using System.Collections.Generic;
using System.IO; // can't alias
using System.Linq; // can't alias
using NUnit.Framework; // can't alias

namespace AasCore.Aas3_0_RC02.Tests
{
    // ReSharper disable once InconsistentNaming
    public class TestJsonizationOfConcreteClassesThroughEnvironment
    {
        /// <summary>
        /// List the JSON files in the "Expected" directory. 
        /// </summary>
        private static IEnumerable<string> ExpectedPaths()
        {
            string expectedDir = Path.Combine(
                AasCore.Aas3_0_RC02.Tests.Common.OurTestResourceDir,
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

        [Test]
        public void Test_json_deserialize_verify_json_serialize_equal()
        {
            foreach (string path in ExpectedPaths())
            {
                var originalNode = Aas3_0_RC02.Tests.CommonJson.ReadFromFile(path);
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
                    var builder = new System.Text.StringBuilder();
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
                    Aas3_0_RC02.Tests.CommonJson.CheckJsonNodesEqual(
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

        /// <summary>
        /// List the JSON files in the "Unexpected" directory for which
        /// the deserialization must fail. 
        /// </summary>
        private static IEnumerable<string> PathsWithFailedDeserializations()
        {
            var dirNames = new List<string>()
            {
                "TypeViolation",
                "RequiredViolation",
                "EnumViolation"
            };

            var paths = new List<string>();

            foreach (string dirName in dirNames)
            {
                string unexpectedDir = Path.Combine(
                    AasCore.Aas3_0_RC02.Tests.Common.OurTestResourceDir,
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
                var node = Aas3_0_RC02.Tests.CommonJson.ReadFromFile(path);

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
                    if (AasCore.Aas3_0_RC02.Tests.Common.RecordMode)
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
        /// the verification must fail. 
        /// </summary>
        private static IEnumerable<string> PathsWithFailedVerifications()
        {
            var dirNames = new List<string>()
            {
                "DateTimeStampUtcViolationOnFebruary29th",
                "MaxLengthViolation",
                "MinLengthViolation",
                "PatternViolation",
                "InvalidValueExamples",
                "InvalidMinMaxExamples",
                "ConstraintViolation"
            };

            var paths = new List<string>();

            foreach (string dirName in dirNames)
            {
                string unexpectedDir = Path.Combine(
                    AasCore.Aas3_0_RC02.Tests.Common.OurTestResourceDir,
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

            if (paths.Count == 0)
            {
                throw new System.IO.FileNotFoundException(
                    "No files were found in the test resources");
            }

            return paths;
        }

        [Test]
        public void Test_json_deserialize_verify_fail()
        {
            foreach (string path in PathsWithFailedVerifications())
            {
                var node = Aas3_0_RC02.Tests.CommonJson.ReadFromFile(path);
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
                if (AasCore.Aas3_0_RC02.Tests.Common.RecordMode)
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
    }
}