using System;
using StringBetween;
using NUnit.Framework;
// ReSharper disable StringLiteralTypo

namespace Test
{
    public class LexSortTests
    {
        private Interpolator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = Interpolator.SingleCaseAlpha;
        }



        [TestCase("abcde", "abchi", "abcf")]
        [TestCase("abc", "abchi", "abcd")]
        [TestCase("abhs", "abit", "abhw")]
        [TestCase("abh", "abit", "abhm")]
        [TestCase("abhz", "abit", "abhzm")]
        [TestCase("abhzs", "abit", "abhzw")]
        [TestCase("abhzz", "abit", "abhzzm")]
        [TestCase("abc", "abcah", "abcad")]
        [TestCase("abc", "abcab", "abcaam")]
        [TestCase("abc", "abcaah", "abcaad")]
        [TestCase("abc", "abcb", "abcam")]
        public void MidString(string a, string b, string expected)
        {

            var mid = _sut.GetStringBetween(a, b);

            Assert.That(mid, Is.EqualTo(expected));
        }


        [Test]
        public void MidString_BothEmpty_ProducesN()
        {
            var mid = _sut.GetStringBetween(String.Empty, String.Empty);

            Assert.That(mid, Is.EqualTo("m"));
        }

        [Test]
        public void MidString_LeftEmpty()
        {
            var mid = _sut.GetStringBetween(String.Empty, "n");

            Assert.That(mid, Is.EqualTo("g"));
        }


        [Test]
        public void MidString_RightEmpty()
        {
            var mid = _sut.GetStringBetween("n", String.Empty);

            Assert.That(mid, Is.EqualTo("t"));
        }


        [Test]
        public void MidString_IdenticalInputs()
        {
            var mid = _sut.GetStringBetween("abc", "abc");

            Assert.That(mid, Is.EqualTo("abcm"));
        }

        [Test]
        public void MidString_InputsCommutative()
        {
            var a = "abca";
            var b = "abcd";

            Assert.That(_sut.GetStringBetween(a, b), Is.EqualTo(_sut.GetStringBetween(b, a)));
        }

    }
}