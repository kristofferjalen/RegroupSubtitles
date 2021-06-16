using System.IO;

namespace RegroupSubtitles
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var paths = Directory.GetFiles(args[0], args[1]);

            foreach (var path in paths)
            {
                var lines = File.ReadAllLines(path);

                var output = Regrouper.Regroup(lines);

                File.Copy(path, Path.ChangeExtension(path, ".srt.bak"), true);

                File.WriteAllLines(path, output);
            }
        }
    }
}