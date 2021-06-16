using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegroupSubtitles.Tests
{
    [TestClass]
    public class RegrouperTests
    {
        [TestMethod]
        public void Regroup_Should_Return_List()
        {
            var strings = new[]
            {
                "1",
                "00:00:00,560 --> 00:00:05,220",
                "",
                "foo bar",
                "",
                "2",
                "00:00:05,220 --> 00:00:05,230",
                "foo bar",
                " ",
                "",
                "3",
                "00:00:05,230 --> 00:00:08,310",
                "foo bar",
                "baz foo bar",
                "",
                "4",
                "00:00:08,310 --> 00:00:08,320",
                "baz foo bar",
                " ",
                "",
                "5",
                "00:00:08,320 --> 00:00:12,150",
                "baz foo bar",
                "baz",
                "",
                "6",
                "00:00:12,150 --> 00:00:12,160",
                "",
                " ",
                ""
            };

            var actual = Regrouper.Regroup(strings).ToArray();

            var expected = new[]
            {
                "1",
                "00:00:00,560 --> 00:00:05,220",
                "foo bar",
                "",
                "",
                "2",
                "00:00:05,220 --> 00:00:05,230",
                " ",
                "",
                "",
                "3",
                "00:00:05,230 --> 00:00:08,310",
                "baz foo bar",
                "",
                "",
                "4",
                "00:00:08,310 --> 00:00:08,320",
                " ",
                "",
                "",
                "5",
                "00:00:08,320 --> 00:00:12,150",
                "baz",
                "",
                "",
                "6",
                "00:00:12,150 --> 00:00:12,160",
                " ",
                "",
                ""
            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Text_With_One_Unique_New_String_Should_Return_String()
        {
            var strings = new[] {"foo"};

            var actual = Regrouper.Text(strings, "");

            Assert.AreEqual("foo", actual);
        }

        [TestMethod]
        public void Text_With_Two_Unique_Strings_Should_Return_Second_String()
        {
            var strings = new[] {"foo", "bar"};

            var actual = Regrouper.Text(strings, "");

            Assert.AreEqual("bar", actual);
        }

        [TestMethod]
        public void Text_With_No_Unique_Strings_Should_Return_String_With_Space()
        {
            var strings = Array.Empty<string>();

            var actual = Regrouper.Text(strings, "");

            Assert.AreEqual(" ", actual);
        }

        [TestMethod]
        public void Texts_Should_Return_List()
        {
            var strings = new[]
            {
                "1",
                "00:00:00,560 --> 00:00:05,220",
                "",
                "foo bar",
                ""
            };

            var actual = Regrouper.Texts(strings, 0).ToArray();

            var expected = new[] {"", "foo bar", ""};

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DistinctNonEmpty_Should_Return_List()
        {
            var strings = new[] {"a", "", " ", "f"};

            var actual = Regrouper.DistinctNonEmpty(strings).ToArray();

            var expected = new[] {"a", "f"};

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StartTime_Should_Return_String()
        {
            const string line = "00:00:00,560 --> 00:00:05,220";

            var actual = Regrouper.StartTime(line);

            const string expected = "00:00:00,560";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EndTime_Should_Return_String()
        {
            const string line = "00:00:00,560 --> 00:00:05,220";

            var actual = Regrouper.EndTime(line);

            const string expected = "00:00:05,220";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NextId_Should_Return_Integer()
        {
            var groups = new List<Group>
            {
                new(1, "", "", "")
            };

            var actual = Regrouper.NextId(groups);

            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void ToStrings_Should_Return_Strings()
        {
            var groups = new List<Group>
            {
                new(1,
                    "00:00:00,560", "00:00:05,220",
                    "foo"),
                new(2,
                    "00:00:00,560", "00:00:05,220",
                    "bar")
            };

            var actual = Regrouper.ToStrings(groups).ToArray();

            var expected = new[]
            {
                "1", "00:00:00,560 --> 00:00:05,220", "foo", "", "",
                "2", "00:00:00,560 --> 00:00:05,220", "bar", "", ""
            };

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}