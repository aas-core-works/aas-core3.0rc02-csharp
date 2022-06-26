using System.Linq;
using Path = System.IO.Path;
using TestContext = NUnit.Framework.TestContext;

using Aas = AasCore.Aas3_0_RC02;

namespace AasCore.Aas3_0_RC02.Tests
{
    /// <summary>
    /// Provide common functionality to be re-used across different tests
    /// such as reading of environment variables. 
    /// </summary>
    public static class Common
    {
        // NOTE (mristin, 2022-05-15):
        // It is tedious to record manually all the expected error messages. Therefore we include this variable
        // to steer the automatic recording. We intentionally inter-twine the recording code with the test code
        // to keep them close to each other so that they are easier to maintain.
        public static readonly bool RecordMode = (
            System.Environment.GetEnvironmentVariable("AAS_CORE_AAS3_0_RC02_TESTS_RECORD_MODE")?.ToLower() == "true"
        ) || true; // TODO (mristin, 2022-05-15): undo

        public static readonly string OurTestResourceDir = Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            Path.Join(
                "TestResources"),
            $"{nameof(AasCore)}.{nameof(Aas3_0_RC02)}.{nameof(Tests)}");

        /// <summary>
        /// Find the first instance of <typeparamref name="T"/> in the <paramref name="container" />,
        /// including the <paramref name="container" /> itself.
        /// </summary>
        public static T MustFind<T>(Aas.IClass container) where T : Aas.IClass
        {
            var instance = (
                (container is T)
                    ? container
                    : container
                          .Descend()
                          .First(something => something is T)
                      ?? throw new System.InvalidOperationException(
                          $"No instance of {nameof(T)} could be found")
            );

            return (T)instance;
        }
    }
}