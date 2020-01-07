using NUnit.Framework;
using Texas_hold_em;

namespace Tests
{
    public class TexasHoldEmServiceTests
    {
        [TestCase("4cKs4h8s7s Ad4s Ac4d As9s KhKd 5d6d", "Ac4d=Ad4s 5d6d As9s KhKd")]
        [TestCase("4cKs4h8s7s Ac4d Ad4s As9s KhKd 5d6d", "Ac4d=Ad4s 5d6d As9s KhKd")]
        [TestCase("2h3h4h5d8d KdKs 9hJh", "KdKs 9hJh")]
        public void Test1(string input, string expectedOutput)
        {
            var actualResult = TexasHoldEmService.Evaluate(input);
            Assert.AreEqual(expectedOutput, actualResult);
        }
    }
}