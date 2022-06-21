/*
 * This code has been automatically generated by aas-core-codegen.
 * Do NOT edit or append.
 */

namespace AasCore.Aas3_0_RC02
{
    public static class Visitation
    {
        /// <summary>
        /// Define the interface for a visitor which visits the instances of the model.
        /// </summary>
        public interface IVisitor
        {
            public void Visit(IClass that);
            public void Visit(Extension that);
            public void Visit(AdministrativeInformation that);
            public void Visit(Qualifier that);
            public void Visit(AssetAdministrationShell that);
            public void Visit(AssetInformation that);
            public void Visit(Resource that);
            public void Visit(SpecificAssetId that);
            public void Visit(Submodel that);
            public void Visit(RelationshipElement that);
            public void Visit(SubmodelElementList that);
            public void Visit(SubmodelElementCollection that);
            public void Visit(Property that);
            public void Visit(MultiLanguageProperty that);
            public void Visit(Range that);
            public void Visit(ReferenceElement that);
            public void Visit(Blob that);
            public void Visit(File that);
            public void Visit(AnnotatedRelationshipElement that);
            public void Visit(Entity that);
            public void Visit(EventPayload that);
            public void Visit(BasicEventElement that);
            public void Visit(Operation that);
            public void Visit(OperationVariable that);
            public void Visit(Capability that);
            public void Visit(ConceptDescription that);
            public void Visit(Reference that);
            public void Visit(Key that);
            public void Visit(LangString that);
            public void Visit(LangStringSet that);
            public void Visit(DataSpecificationContent that);
            public void Visit(DataSpecification that);
            public void Visit(Environment that);
        }  // public interface IVisitor

        /// <summary>
        /// Just descend through the instances without any action.
        /// </summary>
        /// <remarks>
        /// This class is meaningless for itself. However, it is a good base if you
        /// want to descend through instances and apply actions only on a subset of
        /// classes.
        /// </remarks>
        public class VisitorThrough : IVisitor
        {
            public void Visit(IClass that)
            {
                that.Accept(this);
            }

            public void Visit(Extension that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(AdministrativeInformation that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(Qualifier that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(AssetAdministrationShell that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(AssetInformation that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(Resource that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(SpecificAssetId that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(Submodel that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(RelationshipElement that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(SubmodelElementList that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(SubmodelElementCollection that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(Property that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(MultiLanguageProperty that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(Range that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(ReferenceElement that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(Blob that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(File that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(AnnotatedRelationshipElement that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(Entity that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(EventPayload that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(BasicEventElement that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(Operation that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(OperationVariable that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(Capability that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(ConceptDescription that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(Reference that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(Key that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(LangString that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(LangStringSet that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(DataSpecificationContent that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(DataSpecification that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }

            public void Visit(Environment that)
            {
                // Just descend through, do nothing with <c>that</c>
                foreach (var something in that.DescendOnce())
                {
                    Visit(something);
                }
            }
        }  // public class VisitorThrough

        /// <summary>
        /// Perform double-dispatch to visit the concrete instances.
        /// </summary>
        public abstract class AbstractVisitor : IVisitor
        {
            public void Visit(IClass that)
            {
                that.Accept(this);
            }
            public abstract void Visit(Extension that);
            public abstract void Visit(AdministrativeInformation that);
            public abstract void Visit(Qualifier that);
            public abstract void Visit(AssetAdministrationShell that);
            public abstract void Visit(AssetInformation that);
            public abstract void Visit(Resource that);
            public abstract void Visit(SpecificAssetId that);
            public abstract void Visit(Submodel that);
            public abstract void Visit(RelationshipElement that);
            public abstract void Visit(SubmodelElementList that);
            public abstract void Visit(SubmodelElementCollection that);
            public abstract void Visit(Property that);
            public abstract void Visit(MultiLanguageProperty that);
            public abstract void Visit(Range that);
            public abstract void Visit(ReferenceElement that);
            public abstract void Visit(Blob that);
            public abstract void Visit(File that);
            public abstract void Visit(AnnotatedRelationshipElement that);
            public abstract void Visit(Entity that);
            public abstract void Visit(EventPayload that);
            public abstract void Visit(BasicEventElement that);
            public abstract void Visit(Operation that);
            public abstract void Visit(OperationVariable that);
            public abstract void Visit(Capability that);
            public abstract void Visit(ConceptDescription that);
            public abstract void Visit(Reference that);
            public abstract void Visit(Key that);
            public abstract void Visit(LangString that);
            public abstract void Visit(LangStringSet that);
            public abstract void Visit(DataSpecificationContent that);
            public abstract void Visit(DataSpecification that);
            public abstract void Visit(Environment that);
        }  // public abstract class AbstractVisitor

        /// <summary>
        /// Define the interface for a visitor which visits the instances of the model.
        /// </summary>
        /// <typeparam name="TContext">Context type</typeparam>
        public interface IVisitorWithContext<in TContext>
        {
            public void Visit(IClass that, TContext context);
            public void Visit(Extension that, TContext context);
            public void Visit(AdministrativeInformation that, TContext context);
            public void Visit(Qualifier that, TContext context);
            public void Visit(AssetAdministrationShell that, TContext context);
            public void Visit(AssetInformation that, TContext context);
            public void Visit(Resource that, TContext context);
            public void Visit(SpecificAssetId that, TContext context);
            public void Visit(Submodel that, TContext context);
            public void Visit(RelationshipElement that, TContext context);
            public void Visit(SubmodelElementList that, TContext context);
            public void Visit(SubmodelElementCollection that, TContext context);
            public void Visit(Property that, TContext context);
            public void Visit(MultiLanguageProperty that, TContext context);
            public void Visit(Range that, TContext context);
            public void Visit(ReferenceElement that, TContext context);
            public void Visit(Blob that, TContext context);
            public void Visit(File that, TContext context);
            public void Visit(AnnotatedRelationshipElement that, TContext context);
            public void Visit(Entity that, TContext context);
            public void Visit(EventPayload that, TContext context);
            public void Visit(BasicEventElement that, TContext context);
            public void Visit(Operation that, TContext context);
            public void Visit(OperationVariable that, TContext context);
            public void Visit(Capability that, TContext context);
            public void Visit(ConceptDescription that, TContext context);
            public void Visit(Reference that, TContext context);
            public void Visit(Key that, TContext context);
            public void Visit(LangString that, TContext context);
            public void Visit(LangStringSet that, TContext context);
            public void Visit(DataSpecificationContent that, TContext context);
            public void Visit(DataSpecification that, TContext context);
            public void Visit(Environment that, TContext context);
        }  // public interface IVisitorWithContext

        /// <summary>
        /// Perform double-dispatch to visit the concrete instances
        /// with context.
        /// </summary>
        /// <typeparam name="TContext">Context type</typeparam>
        public abstract class AbstractVisitorWithContext<TContext>
            : IVisitorWithContext<TContext>
        {
            public void Visit(IClass that, TContext context)
            {
                that.Accept(this, context);
            }
            public abstract void Visit(Extension that, TContext context);
            public abstract void Visit(AdministrativeInformation that, TContext context);
            public abstract void Visit(Qualifier that, TContext context);
            public abstract void Visit(AssetAdministrationShell that, TContext context);
            public abstract void Visit(AssetInformation that, TContext context);
            public abstract void Visit(Resource that, TContext context);
            public abstract void Visit(SpecificAssetId that, TContext context);
            public abstract void Visit(Submodel that, TContext context);
            public abstract void Visit(RelationshipElement that, TContext context);
            public abstract void Visit(SubmodelElementList that, TContext context);
            public abstract void Visit(SubmodelElementCollection that, TContext context);
            public abstract void Visit(Property that, TContext context);
            public abstract void Visit(MultiLanguageProperty that, TContext context);
            public abstract void Visit(Range that, TContext context);
            public abstract void Visit(ReferenceElement that, TContext context);
            public abstract void Visit(Blob that, TContext context);
            public abstract void Visit(File that, TContext context);
            public abstract void Visit(AnnotatedRelationshipElement that, TContext context);
            public abstract void Visit(Entity that, TContext context);
            public abstract void Visit(EventPayload that, TContext context);
            public abstract void Visit(BasicEventElement that, TContext context);
            public abstract void Visit(Operation that, TContext context);
            public abstract void Visit(OperationVariable that, TContext context);
            public abstract void Visit(Capability that, TContext context);
            public abstract void Visit(ConceptDescription that, TContext context);
            public abstract void Visit(Reference that, TContext context);
            public abstract void Visit(Key that, TContext context);
            public abstract void Visit(LangString that, TContext context);
            public abstract void Visit(LangStringSet that, TContext context);
            public abstract void Visit(DataSpecificationContent that, TContext context);
            public abstract void Visit(DataSpecification that, TContext context);
            public abstract void Visit(Environment that, TContext context);
        }  // public abstract class AbstractVisitorWithContext

        /// <summary>
        /// Define the interface for a transformer which transforms recursively
        /// the instances into something else.
        /// </summary>
        /// <typeparam name="T">The type of the transformation result</typeparam>
        public interface ITransformer<out T>
        {
            public T Transform(IClass that);
            public T Transform(Extension that);
            public T Transform(AdministrativeInformation that);
            public T Transform(Qualifier that);
            public T Transform(AssetAdministrationShell that);
            public T Transform(AssetInformation that);
            public T Transform(Resource that);
            public T Transform(SpecificAssetId that);
            public T Transform(Submodel that);
            public T Transform(RelationshipElement that);
            public T Transform(SubmodelElementList that);
            public T Transform(SubmodelElementCollection that);
            public T Transform(Property that);
            public T Transform(MultiLanguageProperty that);
            public T Transform(Range that);
            public T Transform(ReferenceElement that);
            public T Transform(Blob that);
            public T Transform(File that);
            public T Transform(AnnotatedRelationshipElement that);
            public T Transform(Entity that);
            public T Transform(EventPayload that);
            public T Transform(BasicEventElement that);
            public T Transform(Operation that);
            public T Transform(OperationVariable that);
            public T Transform(Capability that);
            public T Transform(ConceptDescription that);
            public T Transform(Reference that);
            public T Transform(Key that);
            public T Transform(LangString that);
            public T Transform(LangStringSet that);
            public T Transform(DataSpecificationContent that);
            public T Transform(DataSpecification that);
            public T Transform(Environment that);
        }  // public interface ITransformer

        /// <summary>
        /// Perform double-dispatch to transform recursively
        /// the instances into something else.
        /// </summary>
        /// <typeparam name="T">The type of the transformation result</typeparam>
        public abstract class AbstractTransformer<T> : ITransformer<T>
        {
            public T Transform(IClass that)
            {
                return that.Transform(this);
            }

            public abstract T Transform(Extension that);

            public abstract T Transform(AdministrativeInformation that);

            public abstract T Transform(Qualifier that);

            public abstract T Transform(AssetAdministrationShell that);

            public abstract T Transform(AssetInformation that);

            public abstract T Transform(Resource that);

            public abstract T Transform(SpecificAssetId that);

            public abstract T Transform(Submodel that);

            public abstract T Transform(RelationshipElement that);

            public abstract T Transform(SubmodelElementList that);

            public abstract T Transform(SubmodelElementCollection that);

            public abstract T Transform(Property that);

            public abstract T Transform(MultiLanguageProperty that);

            public abstract T Transform(Range that);

            public abstract T Transform(ReferenceElement that);

            public abstract T Transform(Blob that);

            public abstract T Transform(File that);

            public abstract T Transform(AnnotatedRelationshipElement that);

            public abstract T Transform(Entity that);

            public abstract T Transform(EventPayload that);

            public abstract T Transform(BasicEventElement that);

            public abstract T Transform(Operation that);

            public abstract T Transform(OperationVariable that);

            public abstract T Transform(Capability that);

            public abstract T Transform(ConceptDescription that);

            public abstract T Transform(Reference that);

            public abstract T Transform(Key that);

            public abstract T Transform(LangString that);

            public abstract T Transform(LangStringSet that);

            public abstract T Transform(DataSpecificationContent that);

            public abstract T Transform(DataSpecification that);

            public abstract T Transform(Environment that);
        }  // public abstract class AbstractTransformer

        /// <summary>
        /// Define the interface for a transformer which recursively transforms
        /// the instances into something else while the context is passed along.
        /// </summary>
        /// <typeparam name="TContext">Type of the transformation context</typeparam>
        /// <typeparam name="T">The type of the transformation result</typeparam>
        public interface ITransformerWithContext<in TContext, out T>
        {
            public T Transform(IClass that, TContext context);
            public T Transform(Extension that, TContext context);
            public T Transform(AdministrativeInformation that, TContext context);
            public T Transform(Qualifier that, TContext context);
            public T Transform(AssetAdministrationShell that, TContext context);
            public T Transform(AssetInformation that, TContext context);
            public T Transform(Resource that, TContext context);
            public T Transform(SpecificAssetId that, TContext context);
            public T Transform(Submodel that, TContext context);
            public T Transform(RelationshipElement that, TContext context);
            public T Transform(SubmodelElementList that, TContext context);
            public T Transform(SubmodelElementCollection that, TContext context);
            public T Transform(Property that, TContext context);
            public T Transform(MultiLanguageProperty that, TContext context);
            public T Transform(Range that, TContext context);
            public T Transform(ReferenceElement that, TContext context);
            public T Transform(Blob that, TContext context);
            public T Transform(File that, TContext context);
            public T Transform(AnnotatedRelationshipElement that, TContext context);
            public T Transform(Entity that, TContext context);
            public T Transform(EventPayload that, TContext context);
            public T Transform(BasicEventElement that, TContext context);
            public T Transform(Operation that, TContext context);
            public T Transform(OperationVariable that, TContext context);
            public T Transform(Capability that, TContext context);
            public T Transform(ConceptDescription that, TContext context);
            public T Transform(Reference that, TContext context);
            public T Transform(Key that, TContext context);
            public T Transform(LangString that, TContext context);
            public T Transform(LangStringSet that, TContext context);
            public T Transform(DataSpecificationContent that, TContext context);
            public T Transform(DataSpecification that, TContext context);
            public T Transform(Environment that, TContext context);
        }  // public interface ITransformerWithContext

        /// <summary>
        /// Perform double-dispatch to transform recursively
        /// the instances into something else.
        /// </summary>
        /// <typeparam name="TContext">The type of the transformation context</typeparam>
        /// <typeparam name="T">The type of the transformation result</typeparam>
        public abstract class AbstractTransformerWithContext<TContext, T>
            : ITransformerWithContext<TContext, T>
        {
            public T Transform(IClass that, TContext context)
            {
                return that.Transform(this, context);
            }

            public abstract T Transform(Extension that, TContext context);

            public abstract T Transform(AdministrativeInformation that, TContext context);

            public abstract T Transform(Qualifier that, TContext context);

            public abstract T Transform(AssetAdministrationShell that, TContext context);

            public abstract T Transform(AssetInformation that, TContext context);

            public abstract T Transform(Resource that, TContext context);

            public abstract T Transform(SpecificAssetId that, TContext context);

            public abstract T Transform(Submodel that, TContext context);

            public abstract T Transform(RelationshipElement that, TContext context);

            public abstract T Transform(SubmodelElementList that, TContext context);

            public abstract T Transform(SubmodelElementCollection that, TContext context);

            public abstract T Transform(Property that, TContext context);

            public abstract T Transform(MultiLanguageProperty that, TContext context);

            public abstract T Transform(Range that, TContext context);

            public abstract T Transform(ReferenceElement that, TContext context);

            public abstract T Transform(Blob that, TContext context);

            public abstract T Transform(File that, TContext context);

            public abstract T Transform(AnnotatedRelationshipElement that, TContext context);

            public abstract T Transform(Entity that, TContext context);

            public abstract T Transform(EventPayload that, TContext context);

            public abstract T Transform(BasicEventElement that, TContext context);

            public abstract T Transform(Operation that, TContext context);

            public abstract T Transform(OperationVariable that, TContext context);

            public abstract T Transform(Capability that, TContext context);

            public abstract T Transform(ConceptDescription that, TContext context);

            public abstract T Transform(Reference that, TContext context);

            public abstract T Transform(Key that, TContext context);

            public abstract T Transform(LangString that, TContext context);

            public abstract T Transform(LangStringSet that, TContext context);

            public abstract T Transform(DataSpecificationContent that, TContext context);

            public abstract T Transform(DataSpecification that, TContext context);

            public abstract T Transform(Environment that, TContext context);
        }  // public abstract class AbstractTransformerWithContext
    }  // public static class Visitation
}  // namespace AasCore.Aas3_0_RC02

/*
 * This code has been automatically generated by aas-core-codegen.
 * Do NOT edit or append.
 */
