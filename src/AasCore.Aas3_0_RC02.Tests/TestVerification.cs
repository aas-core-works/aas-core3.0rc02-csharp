using NUnit.Framework;

namespace AasCore.Aas3_0_RC02.Tests
{
    public class TestVerification
    {
        [Test]
        public void Test_ucshar()
        {
            // ReSharper disable once CommentTypo
            // uschar is part of the regular expression for xs:AnyURI.

            var ucscharRe = new System.Text.RegularExpressions.Regex(
                "([\\xa0-\\ud7ff\\uf900-\\ufdcf\\ufdf0-\\uffef]|\\ud800[\\udc00-\\udfff]|[\\ud801-\\ud83e][\\udc00-\\udfff]|\\ud83f[\\udc00-\\udffd]|\\ud840[\\udc00-\\udfff]|[\\ud841-\\ud87e][\\udc00-\\udfff]|\\ud87f[\\udc00-\\udffd]|\\ud880[\\udc00-\\udfff]|[\\ud881-\\ud8be][\\udc00-\\udfff]|\\ud8bf[\\udc00-\\udffd]|\\ud8c0[\\udc00-\\udfff]|[\\ud8c1-\\ud8fe][\\udc00-\\udfff]|\\ud8ff[\\udc00-\\udffd]|\\ud900[\\udc00-\\udfff]|[\\ud901-\\ud93e][\\udc00-\\udfff]|\\ud93f[\\udc00-\\udffd]|\\ud940[\\udc00-\\udfff]|[\\ud941-\\ud97e][\\udc00-\\udfff]|\\ud97f[\\udc00-\\udffd]|\\ud980[\\udc00-\\udfff]|[\\ud981-\\ud9be][\\udc00-\\udfff]|\\ud9bf[\\udc00-\\udffd]|\\ud9c0[\\udc00-\\udfff]|[\\ud9c1-\\ud9fe][\\udc00-\\udfff]|\\ud9ff[\\udc00-\\udffd]|\\uda00[\\udc00-\\udfff]|[\\uda01-\\uda3e][\\udc00-\\udfff]|\\uda3f[\\udc00-\\udffd]|\\uda40[\\udc00-\\udfff]|[\\uda41-\\uda7e][\\udc00-\\udfff]|\\uda7f[\\udc00-\\udffd]|\\uda80[\\udc00-\\udfff]|[\\uda81-\\udabe][\\udc00-\\udfff]|\\udabf[\\udc00-\\udffd]|\\udac0[\\udc00-\\udfff]|[\\udac1-\\udafe][\\udc00-\\udfff]|\\udaff[\\udc00-\\udffd]|\\udb00[\\udc00-\\udfff]|[\\udb01-\\udb3e][\\udc00-\\udfff]|\\udb3f[\\udc00-\\udffd]|\\udb44[\\udc00-\\udfff]|[\\udb45-\\udb7e][\\udc00-\\udfff]|\\udb7f[\\udc00-\\udffd])");

            const string text = "\ud8e8\ude54";
            Assert.IsTrue(ucscharRe.IsMatch(text));
        }

        [Test]
        public void Test_MatchesXsAnyUri()
        {
            // NOTE (mristin, 2022-06-21):
            // This string has been obtained by fuzzing.
            const string text = "\ud8e8\ude54\x05\xbf\x2e";

            Assert.IsFalse(Verification.MatchesXsAnyUri(text));
        }
    }
}