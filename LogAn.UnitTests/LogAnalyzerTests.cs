using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LogAn.UnitTests
{
    [TestFixture]
    class LogAnalyzerTests
    {
        private LogAnalyzer m_analyzer = null;

        [SetUp]
        public void Setup()
        {
            m_analyzer = new LogAnalyzer();
        }

        [TestCase("filewithgoodextensions.slf", true)]
        [TestCase("filewithgoodextensions.SLF", true)]
        [TestCase("filewithbadextensions.foo", false)]
        public void IsValidLogFileName_VariousExtensions_ReturnsTrue(string file, bool expected)
        {
            bool result = m_analyzer.IsValidLogFileName(file);

            Assert.AreEqual(result, expected);
        }

        [Test]
        public void IsValidFileName_EmptyFileName_Throws()
        {
            var ex = Assert.Catch<Exception>(() => m_analyzer.IsValidLogFileName(""));

            StringAssert.Contains("имя файла должно быть задано", ex.Message);
        }

        [TestCase("badfile.foo", false)]
        [TestCase("goodfile.slf", true)]
        public void IsValidFileName_WhenCalled_ChangesWasLastFileNameValid(string file, bool expected)
        {
            LogAnalyzer la = new LogAnalyzer();

            la.IsValidLogFileName(file);

            Assert.AreEqual(expected, la.WasLastFileNameValid);
        }

    }
}
