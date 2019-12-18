namespace Tests
{
    using Vimata.Common;
    using Xunit;

    public class HasherTests
    {
        [Theory]
        [InlineData("asd", "asd", true)]
        [InlineData("asd", "dsa", false)]
        [InlineData("ASD", "ASD", true)]
        [InlineData("123", "123", true)]
        [InlineData("", "", true)]
        public void SameStringsShouldHaveTheSameHashAndDifferentStringsShouldHaveDifferent(string s1, string s2, bool shouldBeTheSame)
        {
            string hash1 = Hasher.GetHashString(s1);
            string hash2 = Hasher.GetHashString(s2);

            Assert.Equal(shouldBeTheSame, hash1 == hash2);
        }

        [Fact]
        public void HasherShouldBeCaseSensitive()
        {
            string a = "asd";
            string b = "ASD";

            string aHash = Hasher.GetHashString(a);
            string bHash = Hasher.GetHashString(b);

            Assert.NotEqual(aHash, bHash);
        }
    }
}