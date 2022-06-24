using Directory = System.IO.Directory;
using Path = System.IO.Path;

using System.Collections.Generic; // can't alias
using System.Linq;  // can't alias
using System.Xml.Linq; // can't alias
using NUnit.Framework; // can't alias

namespace AasCore.Aas3_0_RC02.Tests
{
    public class TestXmlRoundTrips
    {
        /// <summary>
        /// List the XML files in the "Expected" directory. 
        /// </summary>
        private static IEnumerable<string> ExpectedPaths()
        {
            string expectedDir = Path.Combine(
                AasCore.Aas3_0_RC02.Tests.Common.OurTestResourceDir,
                "Xml",
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
                "*.xml",
                System.IO.SearchOption.AllDirectories).ToList();
            paths.Sort();

            return paths;
        }

        private static void CheckElementsEqual(
            XElement expected,
            XElement got,
            out Reporting.Error? error)
        {
            error = null;

            if (expected.Name.LocalName != got.Name.LocalName)
            {
                error = new Reporting.Error(
                    "Mismatch in element names: " +
                    $"{expected} != {got}"
                );
                return;
            }

            string? expectedContent = (expected.FirstNode as XText)?.Value;
            string? gotContent = (got.FirstNode as XText)?.Value;

            if (expectedContent != gotContent)
            {
                error = new Reporting.Error(
                    $"Mismatch in element contents: {expected} != {got}"
                );
                return;
            }

            var expectedChildren = expected.Elements().ToList();
            var gotChildren = got.Elements().ToList();

            if (expectedChildren.Count != gotChildren.Count)
            {
                error = new Reporting.Error(
                    $"Mismatch in child elements: {expected} != {got}"
                );
                return;
            }

            for (int i = 0; i < expectedChildren.Count; i++)
            {
                CheckElementsEqual(
                    expectedChildren[i],
                    gotChildren[i],
                    out error);

                if (error != null)
                {
                    error.PrependSegment(
                        new Reporting.IndexSegment(i));

                    error.PrependSegment(
                        new Reporting.NameSegment(
                            expected.Name.ToString()));
                }
            }
        }

        [Test]
        public void Test_xml_deserialize_verify_xml_serialize_equal()
        {
            foreach (string path in ExpectedPaths())
            {
                using var reader = System.Xml.XmlReader.Create(path);

                AasCore.Aas3_0_RC02.Environment? instance = null;
                try
                {
                    instance = AasCore.Aas3_0_RC02.Xmlization.Deserialize.EnvironmentFrom(
                        reader);
                }
                catch (AasCore.Aas3_0_RC02.Xmlization.Exception exception)
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
                    var errorBuilder = new System.Text.StringBuilder();
                    errorBuilder.Append(
                        $"Expected no errors when verifying the instance de-serialized from {path}, " +
                        $"but got {errors.Count} error(s):\n");
                    for (var i = 0; i < errors.Count; i++)
                    {
                        errorBuilder.Append(
                            $"Error {i + 1}:\n" +
                            $"{Reporting.GenerateJsonPath(errors[i].PathSegments)}: {errors[i].Cause}\n");
                    }

                    Assert.Fail(errorBuilder.ToString());
                }

                // Serialize back
                var outputBuilder = new System.Text.StringBuilder();

                {
                    using var writer = System.Xml.XmlWriter.Create(
                        outputBuilder,
                        new System.Xml.XmlWriterSettings()
                        {
                            Encoding = System.Text.Encoding.UTF8
                        }
                    );
                    AasCore.Aas3_0_RC02.Xmlization.Serialize.To(
                        instance,
                        writer,
                        "aas",
                        "https://www.admin-shell.io/aas/3/0/RC02");
                }

                string outputText = outputBuilder.ToString();
                using var outputReader = new System.IO.StringReader(outputText);
                var gotDoc = XDocument.Load(outputReader);

                Assert.AreEqual(
                    gotDoc.Root?.Name.Namespace.ToString(),
                    "https://www.admin-shell.io/aas/3/0/RC02");

                foreach (var child in gotDoc.Descendants())
                {
                    Assert.AreEqual(
                        child.GetPrefixOfNamespace(child.Name.Namespace),
                        "aas");
                }

                // Check that the input equals output
                var expectedDoc = XDocument.Load(path);

                CheckElementsEqual(
                    expectedDoc.Root!,
                    gotDoc.Root!,
                    out Reporting.Error? inequalityError);

                if (inequalityError != null)
                {
                    Assert.Fail(
                        $"The original XML from {path} is unequal the serialized XML: " +
                        $"#/{Reporting.GenerateRelativeXPath(inequalityError.PathSegments)}: " +
                        inequalityError.Cause
                    );
                }
            }
        }

        /// <summary>
        /// List the XML files in the "Unexpected" directory for which
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
                    "Xml",
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
                        "*.xml",
                        System.IO.SearchOption.AllDirectories).ToList()
                );
            }

            paths.Sort();

            return paths;
        }

        [Test]
        public void Test_xml_deserialize_fail()
        {
            foreach (string path in PathsWithFailedDeserializations())
            {
                using var reader = System.Xml.XmlReader.Create(path);

                AasCore.Aas3_0_RC02.Xmlization.Exception? exception = null;

                try
                {
                    _ = AasCore.Aas3_0_RC02.Xmlization.Deserialize.EnvironmentFrom(
                        reader);
                }
                catch (AasCore.Aas3_0_RC02.Xmlization.Exception observedException)
                {
                    exception = observedException;
                }
                catch (System.Exception unexpectedException)
                {
                    throw new System.InvalidOperationException(
                        $"Unexpected exception when de-serializing the file {path}",
                        unexpectedException);
                }

                if (exception == null)
                {
                    Assert.Fail(
                        $"Expected a Xmlization exception when de-serializing {path}, but got none."
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
                            throw new System.IO.FileNotFoundException(
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
        /// List the XML files in the "Unexpected" directory for which
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
                    "Xml",
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
                        "*.xml",
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
        public void Test_xml_deserialize_verify_fail()
        {
            foreach (string path in PathsWithFailedVerifications())
            {
                using var reader = System.Xml.XmlReader.Create(path);

                AasCore.Aas3_0_RC02.Environment? instance = null;
                try
                {
                    instance = AasCore.Aas3_0_RC02.Xmlization.Deserialize.EnvironmentFrom(
                        reader);
                }
                catch (System.Exception exception)
                {
                    Assert.Fail(
                        "Expected no exception upon de-serialization of an environment " +
                        $"from {path}, but got: {exception}"
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
                        error => $"{Reporting.GenerateRelativeXPath(error.PathSegments)}: {error.Cause}"));

                string errorsPath = path + ".errors";
                if (AasCore.Aas3_0_RC02.Tests.Common.RecordMode)
                {
                    System.IO.File.WriteAllText(errorsPath, got);
                }
                else
                {
                    if (!System.IO.File.Exists(errorsPath))
                    {
                        throw new System.IO.FileNotFoundException(
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