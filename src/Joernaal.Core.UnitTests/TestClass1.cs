using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joernaal.Tasks;

namespace Joernaal
{
    [TestFixture]
    public class AnalyzeTaskTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestMethod()
        {
            // Arrange
            var target = new AnalyzeTask();
            var context = new Context();

            // Act
            target.Init(context);

            //Assert
        }
    }
}
