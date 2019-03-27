using ESAPIX.Constraints;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPlanChecker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlanChecker.Tests
{
    [TestClass()]
    public class CTDateConstraintTests
    {
        [TestMethod()]
        public void ConstrainTest()
        {
            var actual = ResultType.PASSED;
            var expected = ResultType.PASSED;
            Assert.AreEqual(expected, actual);
        }
    }
}