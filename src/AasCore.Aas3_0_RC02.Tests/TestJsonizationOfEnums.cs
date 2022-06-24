/*
 * This code has been automatically generated by testgen.
 * Do NOT edit or append.
 */

using Nodes = System.Text.Json.Nodes;

using NUnit.Framework;  // can't alias

using Aas = AasCore.Aas3_0_RC02;

namespace AasCore.Aas3_0_RC02.Tests
{
    public class TestJsonizationOfEnums
    {
        [Test]
        public void Test_round_trip_ModelingKind()
        {
            var node = Nodes.JsonValue.Create(
                "Template")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.ModelingKindFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.ModelingKindToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"Template\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_ModelingKind

        [Test]
        public void Test_round_trip_QualifierKind()
        {
            var node = Nodes.JsonValue.Create(
                "ValueQualifier")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.QualifierKindFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.QualifierKindToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"ValueQualifier\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_QualifierKind

        [Test]
        public void Test_round_trip_AssetKind()
        {
            var node = Nodes.JsonValue.Create(
                "Type")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.AssetKindFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.AssetKindToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"Type\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_AssetKind

        [Test]
        public void Test_round_trip_EntityType()
        {
            var node = Nodes.JsonValue.Create(
                "CoManagedEntity")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.EntityTypeFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.EntityTypeToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"CoManagedEntity\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_EntityType

        [Test]
        public void Test_round_trip_Direction()
        {
            var node = Nodes.JsonValue.Create(
                "INPUT")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.DirectionFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.DirectionToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"INPUT\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_Direction

        [Test]
        public void Test_round_trip_StateOfEvent()
        {
            var node = Nodes.JsonValue.Create(
                "ON")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.StateOfEventFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.StateOfEventToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"ON\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_StateOfEvent

        [Test]
        public void Test_round_trip_ReferenceTypes()
        {
            var node = Nodes.JsonValue.Create(
                "GlobalReference")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.ReferenceTypesFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.ReferenceTypesToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"GlobalReference\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_ReferenceTypes

        [Test]
        public void Test_round_trip_GenericFragmentKeys()
        {
            var node = Nodes.JsonValue.Create(
                "FragmentReference")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.GenericFragmentKeysFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.GenericFragmentKeysToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"FragmentReference\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_GenericFragmentKeys

        [Test]
        public void Test_round_trip_GenericGloballyIdentifiables()
        {
            var node = Nodes.JsonValue.Create(
                "GlobalReference")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.GenericGloballyIdentifiablesFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.GenericGloballyIdentifiablesToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"GlobalReference\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_GenericGloballyIdentifiables

        [Test]
        public void Test_round_trip_AasIdentifiables()
        {
            var node = Nodes.JsonValue.Create(
                "AssetAdministrationShell")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.AasIdentifiablesFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.AasIdentifiablesToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"AssetAdministrationShell\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_AasIdentifiables

        [Test]
        public void Test_round_trip_AasSubmodelElements()
        {
            var node = Nodes.JsonValue.Create(
                "AnnotatedRelationshipElement")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.AasSubmodelElementsFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.AasSubmodelElementsToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"AnnotatedRelationshipElement\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_AasSubmodelElements

        [Test]
        public void Test_round_trip_AasReferableNonIdentifiables()
        {
            var node = Nodes.JsonValue.Create(
                "AnnotatedRelationshipElement")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.AasReferableNonIdentifiablesFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.AasReferableNonIdentifiablesToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"AnnotatedRelationshipElement\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_AasReferableNonIdentifiables

        [Test]
        public void Test_round_trip_GloballyIdentifiables()
        {
            var node = Nodes.JsonValue.Create(
                "GlobalReference")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.GloballyIdentifiablesFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.GloballyIdentifiablesToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"GlobalReference\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_GloballyIdentifiables

        [Test]
        public void Test_round_trip_FragmentKeys()
        {
            var node = Nodes.JsonValue.Create(
                "FragmentReference")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.FragmentKeysFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.FragmentKeysToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"FragmentReference\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_FragmentKeys

        [Test]
        public void Test_round_trip_KeyTypes()
        {
            var node = Nodes.JsonValue.Create(
                "FragmentReference")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.KeyTypesFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.KeyTypesToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"FragmentReference\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_KeyTypes

        [Test]
        public void Test_round_trip_DataTypeDefXsd()
        {
            var node = Nodes.JsonValue.Create(
                "xs:anyURI")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.DataTypeDefXsdFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.DataTypeDefXsdToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"xs:anyURI\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_DataTypeDefXsd

        [Test]
        public void Test_round_trip_DataTypeDefRdf()
        {
            var node = Nodes.JsonValue.Create(
                "rdf:langString")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.DataTypeDefRdfFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.DataTypeDefRdfToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"rdf:langString\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_DataTypeDefRdf

        [Test]
        public void Test_round_trip_DataTypeDef()
        {
            var node = Nodes.JsonValue.Create(
                "xs:anyURI")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.DataTypeDefFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.DataTypeDefToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"xs:anyURI\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_DataTypeDef
    }  // class TestJsonizationOfEnums
}  // namespace AasCore.Aas3_0_RC02.Tests

/*
 * This code has been automatically generated by testgen.
 * Do NOT edit or append.
 */
