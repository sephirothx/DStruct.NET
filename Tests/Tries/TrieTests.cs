using DStruct.Tries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Tries
{
    [TestClass]
    public class TrieTests
    {
        [TestMethod]
        public void AddTest()
        {
            var trie = new Trie {"asd"};
            Assert.AreEqual(1, trie.Count);
            Assert.IsTrue(trie.Contains("asd"));

            trie.Add("asdf");
            Assert.AreEqual(2, trie.Count);
            Assert.IsTrue(trie.Contains("asdf"));

            trie.Add("asd");
            Assert.AreEqual(2, trie.Count);

            trie.Add("as");
            Assert.AreEqual(3, trie.Count);
            Assert.IsTrue(trie.Contains("as"));
        }

        [TestMethod]
        public void ClearTest()
        {
            var list = new[]
            {
                "asdf",
                "qwer",
                "qwerty",
                "asd",
                "asfghj"
            };
            var trie = new Trie(list);
            Assert.AreEqual(5, trie.Count);

            trie.Clear();
            Assert.AreEqual(0, trie.Count);

            trie.Add("a");
            Assert.AreEqual(1, trie.Count);

            trie.Add("asd");
            Assert.AreEqual(2, trie.Count);
        }

        [TestMethod]
        public void AddAllTest()
        {
            var list = new[]
            {
                "asdf",
                "qwer",
                "qwerty",
                "asd",
                "asfghj"
            };
            var trie = new Trie();
            trie.AddAll(list);

            Assert.AreEqual(5, trie.Count);

            Assert.IsTrue(trie.Contains("asdf"));
            Assert.IsTrue(trie.Contains("qwer"));
            Assert.IsTrue(trie.Contains("qwerty"));
            Assert.IsTrue(trie.Contains("asd"));
            Assert.IsTrue(trie.Contains("asfghj"));

            Assert.IsFalse(trie.Contains("as"));
            Assert.IsFalse(trie.Contains("asth"));
            Assert.IsFalse(trie.Contains("asfg"));
        }

        [TestMethod]
        public void RemoveTest()
        {
            var list = new[]
            {
                "asdf",
                "qwer",
                "qwerty",
                "asd",
                "asfghj"
            };
            var trie = new Trie(list);

            Assert.AreEqual(5, trie.Count);

            bool ret = trie.Remove("qwer");
            Assert.AreEqual(4, trie.Count);
            Assert.IsTrue(ret);
            Assert.IsFalse(trie.Contains("qwer"));
            Assert.IsTrue(trie.ContainsPrefix("qwer"));

            ret = trie.Remove("qwerty");
            Assert.AreEqual(3, trie.Count);
            Assert.IsTrue(ret);
            Assert.IsFalse(trie.Contains("qwerty"));
            Assert.IsFalse(trie.ContainsPrefix("qwer"));

            ret = trie.Remove("as");
            Assert.AreEqual(3, trie.Count);
            Assert.IsFalse(ret);

            ret = trie.Remove("qwer");
            Assert.AreEqual(3, trie.Count);
            Assert.IsFalse(ret);
        }

        [TestMethod]
        public void RemoveAllTest()
        {
            var list = new[]
            {
                "asdf",
                "qwer",
                "qwerty",
                "asd",
                "asfghj"
            };
            var trie = new Trie(list);

            var list2 = new[]
            {
                "asdf",
                "qwer"
            };
            trie.RemoveAll(list2);
            Assert.AreEqual(3, trie.Count);

            var list3 = new[]
            {
                "qwer",
                "qwerty",
                "asd"
            };

            trie.RemoveAll(list3);
            Assert.AreEqual(1, trie.Count);
        }

        [TestMethod]
        public void ContainsTest()
        {
            var list = new[]
            {
                "asdf",
                "qwer",
                "qwerty",
                "asd",
                "asfghj"
            };
            var trie = new Trie(list);

            Assert.IsTrue(trie.Contains("asdf"));
            Assert.IsFalse(trie.Contains("as"));
        }

        [TestMethod]
        public void ContainsAllTest()
        {
            var list = new[]
            {
                "asdf",
                "qwer",
                "qwerty",
                "asd",
                "asfghj"
            };
            var trie = new Trie(list);

            var list2 = new[]
            {
                "asdf",
                "qwer"
            };
            Assert.IsTrue(trie.ContainsAll(list2));

            var list3 = new[]
            {
                "qwer",
                "qwerty",
                "as"
            };
            Assert.IsFalse(trie.ContainsAll(list3));
        }

        [TestMethod]
        public void ContainsPrefixTest()
        {
            var list = new[]
            {
                "asdf",
                "qwer",
                "qwerty",
                "asd",
                "asfghj"
            };
            var trie = new Trie(list);

            Assert.IsTrue(trie.ContainsPrefix("as"));
            Assert.IsTrue(trie.ContainsPrefix("asd"));
            Assert.IsFalse(trie.ContainsPrefix("asdfg"));
        }

        [TestMethod]
        public void GetWithPrefixTest()
        {
            var list = new[]
            {
                "asdf",
                "qwer",
                "qwerty",
                "asd",
                "asfghj"
            };
            var trie = new Trie(list);

            var ret = trie.GetWithPrefix("as");
            Assert.AreEqual(3, ret.Length);
            Assert.AreEqual("asd", ret[0]);
            Assert.AreEqual("asdf", ret[1]);
            Assert.AreEqual("asfghj", ret[2]);

            var ret2 = trie.GetWithPrefix("qa");
            Assert.AreEqual(0, ret2.Length);
        }
    }
}
