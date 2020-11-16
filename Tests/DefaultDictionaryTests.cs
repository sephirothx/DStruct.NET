using DStruct;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class DefaultDictionaryTests
    {
        [TestMethod]
        public void DefaultDictTest()
        {
            var dd = new DefaultDictionary<int, int>();
            dd[0] = 1;
            dd[1]++;
            
            Assert.AreEqual(1, dd[0]);
            Assert.AreEqual(1, dd[1]);
            Assert.AreEqual(0, dd[2]);
            Assert.AreEqual(3, dd.Count);
        }
    }
}
