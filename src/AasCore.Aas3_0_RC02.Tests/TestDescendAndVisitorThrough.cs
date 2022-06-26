using System.Collections.Generic;
using Directory = System.IO.Directory;
using Path = System.IO.Path;

using System.Linq; // can't alias
using NUnit.Framework; // can't alias

using Aas = AasCore.Aas3_0_RC02;  // renamed

namespace AasCore.Aas3_0_RC02.Tests
{
    public class TestDescendAndVisitorThrough
    {
        private static readonly string JsonExpectedDir = Path.Combine(
            Aas.Tests.Common.OurTestResourceDir,
            "Json",
            "Expected");

        /// <summary>
        /// Find all the complete examples in the <see cref="JsonExpectedDir"/>.
        /// </summary>
        private static List<string> PathsToCompleteExamples()
        {
            if (!Directory.Exists(JsonExpectedDir))
            {
                throw new System.InvalidOperationException(
                    "The directory with the complete example files does not exist " +
                    $"or is not a directory: {JsonExpectedDir}"
                );
            }

            var result = Directory.GetFiles(
                JsonExpectedDir,
                "complete.json",
                System.IO.SearchOption.AllDirectories).ToList();
            result.Sort();

            return result;
        }

        private static string Trace(Aas.IClass instance)
        {
            switch (instance)
            {
                case IIdentifiable identifiable:
                    {
                        return $"{identifiable.GetType()} with ID {identifiable.Id}";
                    }
                case IReferable referable:
                    {
                        return $"{referable.GetType()} with ID-short {referable.IdShort}";
                    }
                default:
                    {
                        return instance.GetType().Name;
                    }
            }
        }

        class TracingVisitorThrough : Aas.Visitation.VisitorThrough
        {
            public readonly List<string> Log = new List<string>();

            public override void Visit(IClass that)
            {
                Log.Add(Trace(that));
                base.Visit(that);
            }
        }

        [Test]
        public void Test_Descend_against_visitor_through()
        {
            foreach (string pathToCompleteExample in PathsToCompleteExamples())
            {
                IClass? environment = Aas.Jsonization.Deserialize.EnvironmentFrom(
                    Aas.Tests.CommonJson.ReadFromFile(
                        pathToCompleteExample));

                if (environment == null)
                {
                    throw new System.InvalidOperationException(
                        "environment unexpectedly null");
                }

                var logFromDescend = new List<string>();

                foreach (var instance in environment.Descend())
                {
                    logFromDescend.Add(Trace(instance));
                }

                var visitor = new TracingVisitorThrough();
                visitor.Visit(environment);
                var traceFromVisitor = visitor.Log;

                Assert.IsNotEmpty(traceFromVisitor);

                Assert.AreEqual(
                    Trace(environment),
                    traceFromVisitor[0]);

                traceFromVisitor.RemoveAt(0);

                Assert.That(traceFromVisitor, Is.EquivalentTo(logFromDescend));
            }
        }

        [Test]
        public void Test_against_the_recorded()
        {
            string recordingBaseDir = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Descend");

            foreach (string pathToCompleteExample in PathsToCompleteExamples())
            {
                IClass? environment = Aas.Jsonization.Deserialize.EnvironmentFrom(
                    Aas.Tests.CommonJson.ReadFromFile(
                        pathToCompleteExample));

                if (environment == null)
                {
                    throw new System.InvalidOperationException(
                        "environment unexpectedly null");
                }

                var writer = new System.IO.StringWriter();
                foreach (var instance in environment.Descend())
                {
                    writer.WriteLine(Trace(instance));
                }

                string got = writer.ToString();

                var relativePath = Path.GetRelativePath(
                    JsonExpectedDir, pathToCompleteExample);

                var expectedPath = Path.Join(
                    recordingBaseDir, relativePath + ".trace");

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