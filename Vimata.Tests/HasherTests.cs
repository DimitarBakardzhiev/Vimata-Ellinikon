namespace Tests
{
    using NUnit.Framework;
    using Vimata.Common;

    public class HasherTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestEqualStringsHash()
        {
            string a = "asd";
            string b = "asd";

            string aHash = Hasher.GetHashString(a);
            string bHash = Hasher.GetHashString(b);

            Assert.AreEqual(aHash, bHash);
        }

        [Test]
        public void TestDifferentStringsHash()
        {
            string a = "asd";
            string b = "dsa";

            string aHash = Hasher.GetHashString(a);
            string bHash = Hasher.GetHashString(b);

            Assert.AreNotEqual(aHash, bHash);
        }

        [Test]
        public void ShouldBeCaseSensitive()
        {
            string a = "asd";
            string b = "ASD";

            string aHash = Hasher.GetHashString(a);
            string bHash = Hasher.GetHashString(b);

            Assert.AreNotEqual(aHash, bHash);
        }
    }
}