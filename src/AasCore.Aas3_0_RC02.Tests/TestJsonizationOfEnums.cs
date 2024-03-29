/*
 * This code has been automatically generated by testgen.
 * Do NOT edit or append.
 */

using Aas = AasCore.Aas3_0_RC02;  // renamed
using Nodes = System.Text.Json.Nodes;

using NUnit.Framework;  // can't alias

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
                "input")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.DirectionFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.DirectionToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"input\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_Direction

        [Test]
        public void Test_round_trip_StateOfEvent()
        {
            var node = Nodes.JsonValue.Create(
                "on")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.StateOfEventFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.StateOfEventToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"on\"",
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
        public void Test_round_trip_DataTypeIec61360()
        {
            var node = Nodes.JsonValue.Create(
                "DATE")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.DataTypeIec61360From(
                node);

            var serialized = Aas.Jsonization.Serialize.DataTypeIec61360ToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"DATE\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_DataTypeIec61360

        [Test]
        public void Test_round_trip_LevelType()
        {
            var node = Nodes.JsonValue.Create(
                "Min")
                    ?? throw new System.InvalidOperationException(
                        "Unexpected null node");

            var parsed = Aas.Jsonization.Deserialize.LevelTypeFrom(
                node);

            var serialized = Aas.Jsonization.Serialize.LevelTypeToJsonValue(
                parsed);

            Assert.AreEqual(
                "\"Min\"",
                serialized.ToJsonString());
        }  // void Test_round_trip_LevelType
    }  // class TestJsonizationOfEnums
}  // namespace AasCore.Aas3_0_RC02.Tests

/*
 * This code has been automatically generated by testgen.
 * Do NOT edit or append.
 */
