using Path = System.IO.Path;
using TestContext = NUnit.Framework.TestContext;

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
    }
}