/*
 * This code has been automatically generated by testgen.
 * Do NOT edit or append.
 */

using Directory = System.IO.Directory;
using Path = System.IO.Path;

using NUnit.Framework; // can't alias

using Aas = AasCore.Aas3_0_RC02;  // renamed

namespace AasCore.Aas3_0_RC02.Tests
{
    public class TestDescendOnce
    {
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
                    $"The expected trace from {expectedPath} does not match the actual one");
            }
        }

        [Test]
        public void Test_Extension()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "Extension",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.Extension>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "Extension",
                    "complete.json.trace"));
        }  // public void Test_Extension

        [Test]
        public void Test_AdministrativeInformation()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "AdministrativeInformation",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.AdministrativeInformation>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "AdministrativeInformation",
                    "complete.json.trace"));
        }  // public void Test_AdministrativeInformation

        [Test]
        public void Test_Qualifier()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "Qualifier",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.Qualifier>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "Qualifier",
                    "complete.json.trace"));
        }  // public void Test_Qualifier

        [Test]
        public void Test_AssetAdministrationShell()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "AssetAdministrationShell",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.AssetAdministrationShell>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "AssetAdministrationShell",
                    "complete.json.trace"));
        }  // public void Test_AssetAdministrationShell

        [Test]
        public void Test_AssetInformation()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "AssetInformation",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.AssetInformation>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "AssetInformation",
                    "complete.json.trace"));
        }  // public void Test_AssetInformation

        [Test]
        public void Test_Resource()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "Resource",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.Resource>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "Resource",
                    "complete.json.trace"));
        }  // public void Test_Resource

        [Test]
        public void Test_SpecificAssetId()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "SpecificAssetId",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.SpecificAssetId>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "SpecificAssetId",
                    "complete.json.trace"));
        }  // public void Test_SpecificAssetId

        [Test]
        public void Test_Submodel()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "Submodel",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.Submodel>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "Submodel",
                    "complete.json.trace"));
        }  // public void Test_Submodel

        [Test]
        public void Test_RelationshipElement()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "RelationshipElement",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.RelationshipElement>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "RelationshipElement",
                    "complete.json.trace"));
        }  // public void Test_RelationshipElement

        [Test]
        public void Test_SubmodelElementList()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "SubmodelElementList",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.SubmodelElementList>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "SubmodelElementList",
                    "complete.json.trace"));
        }  // public void Test_SubmodelElementList

        [Test]
        public void Test_SubmodelElementCollection()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "SubmodelElementCollection",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.SubmodelElementCollection>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "SubmodelElementCollection",
                    "complete.json.trace"));
        }  // public void Test_SubmodelElementCollection

        [Test]
        public void Test_Property()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "Property",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.Property>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "Property",
                    "complete.json.trace"));
        }  // public void Test_Property

        [Test]
        public void Test_MultiLanguageProperty()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "MultiLanguageProperty",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.MultiLanguageProperty>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "MultiLanguageProperty",
                    "complete.json.trace"));
        }  // public void Test_MultiLanguageProperty

        [Test]
        public void Test_Range()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "Range",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.Range>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "Range",
                    "complete.json.trace"));
        }  // public void Test_Range

        [Test]
        public void Test_ReferenceElement()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "ReferenceElement",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.ReferenceElement>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "ReferenceElement",
                    "complete.json.trace"));
        }  // public void Test_ReferenceElement

        [Test]
        public void Test_Blob()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "Blob",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.Blob>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "Blob",
                    "complete.json.trace"));
        }  // public void Test_Blob

        [Test]
        public void Test_File()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "File",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.File>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "File",
                    "complete.json.trace"));
        }  // public void Test_File

        [Test]
        public void Test_AnnotatedRelationshipElement()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "AnnotatedRelationshipElement",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.AnnotatedRelationshipElement>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "AnnotatedRelationshipElement",
                    "complete.json.trace"));
        }  // public void Test_AnnotatedRelationshipElement

        [Test]
        public void Test_Entity()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "Entity",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.Entity>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "Entity",
                    "complete.json.trace"));
        }  // public void Test_Entity

        [Test]
        public void Test_BasicEventElement()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "BasicEventElement",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.BasicEventElement>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "BasicEventElement",
                    "complete.json.trace"));
        }  // public void Test_BasicEventElement

        [Test]
        public void Test_Operation()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "Operation",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.Operation>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "Operation",
                    "complete.json.trace"));
        }  // public void Test_Operation

        [Test]
        public void Test_OperationVariable()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "OperationVariable",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.OperationVariable>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "OperationVariable",
                    "complete.json.trace"));
        }  // public void Test_OperationVariable

        [Test]
        public void Test_Capability()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "Capability",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.Capability>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "Capability",
                    "complete.json.trace"));
        }  // public void Test_Capability

        [Test]
        public void Test_ConceptDescription()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "ConceptDescription",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.ConceptDescription>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "ConceptDescription",
                    "complete.json.trace"));
        }  // public void Test_ConceptDescription

        [Test]
        public void Test_Reference()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "Reference",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.Reference>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "Reference",
                    "complete.json.trace"));
        }  // public void Test_Reference

        [Test]
        public void Test_Key()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "Key",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.Key>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "Key",
                    "complete.json.trace"));
        }  // public void Test_Key

        [Test]
        public void Test_LangString()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "LangString",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.LangString>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "LangString",
                    "complete.json.trace"));
        }  // public void Test_LangString

        [Test]
        public void Test_LangStringSet()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "LangStringSet",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.LangStringSet>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "LangStringSet",
                    "complete.json.trace"));
        }  // public void Test_LangStringSet

        [Test]
        public void Test_Environment()
        {
            string pathToCompleteExample = Path.Combine(
                Aas.Tests.Common.OurTestResourceDir,
                "Json",
                "Expected",
                "Environment",
                "complete.json");

            var container = Aas.Tests.CommonJson.LoadInstance(
                pathToCompleteExample);

            var instance = Aas.Tests.Common.MustFind<Aas.Environment>(
                container);

            CompareOrRerecordTrace(
                instance,
                Path.Combine(
                    Aas.Tests.Common.OurTestResourceDir,
                    "DescendOnce",
                    "Environment",
                    "complete.json.trace"));
        }  // public void Test_Environment
    }  // class TestDescendOnce
}  // namespace AasCore.Aas3_0_RC02.Tests

/*
 * This code has been automatically generated by testgen.
 * Do NOT edit or append.
 */
