using System.Globalization;
using SharpDX;
using System.IO;

namespace GCode3D.Models
{
    public static class Parser
    {
        // TODO: Scritta così fa cagare
        public static List<StatelessCommand> From(FileInfo? from = null)
        {
            // TODO: Handle with exceptions (ArgumentNullException)
            if(from == null)
                return [];

            using StreamReader sr = from.OpenText();
            List<StatelessCommand> commands = [];
            var line = sr.ReadLine();
            Vector3 currentPoint = new();

            while (line != null)
            {
                if (line.StartsWith("G01"))
                {
                    // Parse each G-code command
                    string[] parts = line.Split(' ');

                    if (parts.Length > 1)
                    {
                        // Extract command and values
                        string gCommand = parts[0].ToUpper();

                        // Add the new point to the spline
                        var newPoint = new Vector3(
                            ParseCoordinate(parts.First(part => part.StartsWith('X')), 0),
                            ParseCoordinate(parts.First(part => part.StartsWith('Y')), 0),
                            ParseCoordinate(parts.First(part => part.StartsWith('Z')), 0)
                        );

                        commands.Add(new StatelessCommand
                        {
                            From = new Vector3(currentPoint.X, currentPoint.Y, currentPoint.Z),
                            To = newPoint,
                            Code = line,
                            CommandCode = gCommand
                        });
                        currentPoint = newPoint;
                    }
                }

                line = sr.ReadLine();
            }

            return commands;
        }

        private static float ParseCoordinate(string value, float defaultValue) =>
            float.TryParse(value[1..], CultureInfo.InvariantCulture, out float result) ?
                result : defaultValue;
    }
}
