using Directory = System.IO.Directory;
using FileMode = System.IO.FileMode;
using FileStream = System.IO.FileStream;
using Nodes = System.Text.Json.Nodes;
using Path = System.IO.Path;

using System.Linq; // can't alias
using NUnit.Framework; // can't alias

namespace AasCore.Aas3_0_RC02.Tests
{
    public class TestDescend
    {
        [Test]
        public void Test_against_the_recorded()
        {
            string jsonExpectedDir = Path.Combine(
                AasCore.Aas3_0_RC02.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected");

            if (!Directory.Exists(jsonExpectedDir))
            {
                throw new System.InvalidOperationException(
                    "The directory with the complete example files does not exist " +
                    $"or is not a directory: {jsonExpectedDir}"
                );
            }

            var pathsToCompleteExamples = Directory.GetFiles(
                jsonExpectedDir,
                "complete.json",
                System.IO.SearchOption.AllDirectories).ToList();
            pathsToCompleteExamples.Sort();

            string recordingBaseDir = Path.Combine(
                AasCore.Aas3_0_RC02.Tests.Common.OurTestResourceDir,
                "Descend");

            foreach (string pathToCompleteExample in pathsToCompleteExamples)
            {
                Environment? environment;

                {
                    using var stream = new FileStream(pathToCompleteExample, FileMode.Open);
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

                var writer = new System.IO.StringWriter();
                foreach (var instance in environment.Descend())
                {
                    switch (instance)
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
                                writer.WriteLine(instance.GetType().Name);
                                break;
                            }
                    }
                }

                string got = writer.ToString();

                var relativePath = Path.GetRelativePath(
                    jsonExpectedDir, pathToCompleteExample);

                var expectedPath = Path.Join(
                    recordingBaseDir, relativePath + ".trace");

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
                        $"The expected trace from {expectedPath} does not match the actual one " +
                        $"for the file {pathToCompleteExample}");
                }
            }
        }
    }
}