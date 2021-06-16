using System.Collections.Generic;
using System.Linq;

namespace RegroupSubtitles
{
    internal static class Regrouper
    {
        public static IEnumerable<string> Regroup(string[] lines)
        {
            var groups = new List<Group>();

            var i = 0;

            var lastString = "";

            while (i < lines.Length)
            {
                var start = StartTime(lines[i + 1]);

                var end = EndTime(lines[i + 1]);

                var texts = Texts(lines, i);

                var uniques = DistinctNonEmpty(texts).ToArray();

                var text = Text(uniques, lastString);

                var id = NextId(groups);

                var group = new Group(id, start, end, text);

                groups.Add(group);

                lastString = groups[^1].Text;

                i += 5;
            }

            var output = ToStrings(groups);

            return output;
        }

        internal static string Text(string[] uniques, string lastString) =>
            uniques.Length switch
            {
                1 when uniques[0] != lastString => uniques[0],
                2 => uniques[1],
                _ => " "
            };

        internal static string[] Texts(string[] lines, int i) =>
            new[]
            {
                lines[i + 2],
                lines[i + 3],
                lines[i + 4]
            };

        internal static IEnumerable<string> DistinctNonEmpty(string[] strings) =>
            strings.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct();

        internal static string EndTime(string line) => line[17..];

        internal static string StartTime(string line) => line[..12];

        internal static int NextId(List<Group> groups) => groups.Any() ? groups[^1].Id + 1 : 1;

        internal static IEnumerable<string> ToStrings(IEnumerable<Group> groups) =>
            groups.SelectMany(x => new[]
            {
                x.Id.ToString(),
                $"{x.Start} --> {x.End}",
                x.Text,
                "",
                ""
            });
    }
}