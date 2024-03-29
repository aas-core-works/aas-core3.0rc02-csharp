/*
 * This code has been automatically generated by testgen.
 * Do NOT edit or append.
 */

using Aas = AasCore.Aas3_0_RC02; // renamed
using AasEnhancing = AasCore.Aas3_0_RC02.Enhancing; // renamed

using System.Collections.Generic; // can't alias
using System.Linq; // can't alias

using NUnit.Framework; // can't alias

namespace AasCore.Aas3_0_RC02.Tests
{
    public class TestEnhancing
    {
        class Enhancement
        {
            public readonly long SomeCustomId;

            public Enhancement(long someCustomId)
            {
                SomeCustomId = someCustomId;
            }
        }

        private static AasEnhancing.Enhancer<Enhancement> CreateEnhancer()
        {
            long lastCustomId = 0;

            var enhancementFactory = new System.Func<IClass, Enhancement>(
                delegate
                {
                    lastCustomId++;
                    return new Enhancement(lastCustomId);
                }
            );

            return new AasEnhancing.Enhancer<Enhancement>(enhancementFactory);
        }

        [Test]
        public void Test_Extension()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteExtension()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_Extension

        [Test]
        public void Test_AdministrativeInformation()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteAdministrativeInformation()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_AdministrativeInformation

        [Test]
        public void Test_Qualifier()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteQualifier()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_Qualifier

        [Test]
        public void Test_AssetAdministrationShell()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteAssetAdministrationShell()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_AssetAdministrationShell

        [Test]
        public void Test_AssetInformation()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteAssetInformation()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_AssetInformation

        [Test]
        public void Test_Resource()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteResource()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_Resource

        [Test]
        public void Test_SpecificAssetId()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteSpecificAssetId()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_SpecificAssetId

        [Test]
        public void Test_Submodel()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteSubmodel()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_Submodel

        [Test]
        public void Test_RelationshipElement()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteRelationshipElement()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_RelationshipElement

        [Test]
        public void Test_SubmodelElementList()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteSubmodelElementList()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_SubmodelElementList

        [Test]
        public void Test_SubmodelElementCollection()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteSubmodelElementCollection()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_SubmodelElementCollection

        [Test]
        public void Test_Property()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteProperty()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_Property

        [Test]
        public void Test_MultiLanguageProperty()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteMultiLanguageProperty()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_MultiLanguageProperty

        [Test]
        public void Test_Range()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteRange()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_Range

        [Test]
        public void Test_ReferenceElement()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteReferenceElement()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_ReferenceElement

        [Test]
        public void Test_Blob()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteBlob()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_Blob

        [Test]
        public void Test_File()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteFile()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_File

        [Test]
        public void Test_AnnotatedRelationshipElement()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteAnnotatedRelationshipElement()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_AnnotatedRelationshipElement

        [Test]
        public void Test_Entity()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteEntity()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_Entity

        [Test]
        public void Test_EventPayload()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteEventPayload()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_EventPayload

        [Test]
        public void Test_BasicEventElement()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteBasicEventElement()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_BasicEventElement

        [Test]
        public void Test_Operation()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteOperation()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_Operation

        [Test]
        public void Test_OperationVariable()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteOperationVariable()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_OperationVariable

        [Test]
        public void Test_Capability()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteCapability()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_Capability

        [Test]
        public void Test_ConceptDescription()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteConceptDescription()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_ConceptDescription

        [Test]
        public void Test_Reference()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteReference()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_Reference

        [Test]
        public void Test_Key()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteKey()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_Key

        [Test]
        public void Test_LangString()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteLangString()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_LangString

        [Test]
        public void Test_Environment()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteEnvironment()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_Environment

        [Test]
        public void Test_EmbeddedDataSpecification()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteEmbeddedDataSpecification()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_EmbeddedDataSpecification

        [Test]
        public void Test_ValueReferencePair()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteValueReferencePair()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_ValueReferencePair

        [Test]
        public void Test_ValueList()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteValueList()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_ValueList

        [Test]
        public void Test_DataSpecificationIec61360()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteDataSpecificationIec61360()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_DataSpecificationIec61360

        [Test]
        public void Test_DataSpecificationPhysicalUnit()
        {
            var instance = (
                Aas.Tests.CommonJsonization.LoadCompleteDataSpecificationPhysicalUnit()
            );

            var enhancer = CreateEnhancer();

            Assert.IsNull(enhancer.Unwrap(instance));

            var wrapped = enhancer.Wrap(instance);
            Assert.IsNotNull(wrapped);

            var idSet = new HashSet<long>();

            idSet.Add(enhancer.MustUnwrap(wrapped).SomeCustomId);
            idSet.UnionWith(
                wrapped
                    .Descend()
                    .Select(
                        (descendant) =>
                            enhancer.MustUnwrap(descendant).SomeCustomId
                        )
            );

            Assert.AreEqual(1, idSet.Min());
            Assert.AreEqual(idSet.Count, idSet.Max());
        }  // public void Test_DataSpecificationPhysicalUnit
    }  // class TestEnhancing
}  // namespace AasCore.Aas3_0_RC02.Tests

/*
 * This code has been automatically generated by testgen.
 * Do NOT edit or append.
 */
