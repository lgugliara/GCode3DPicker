using System.Globalization;
using SharpDX;
using System.IO;
using GCode3D.Models.Picker;

namespace GCode3D.Models
{
    public static class Parser
    {
        // TODO: Scritta così fa cagare
        public static IEnumerable<StatelessCommand> From(IPickable? from = null)
        {
            // TODO: Handle with exceptions (ArgumentNullException)
            if(from == null)
                return [];

            StreamReader sr = new(from.Path);
            List<StatelessCommand> commands = [];
            var line = sr.ReadLine();
            Vector3 currentPoint = new();

            while (line != null)
            {
                if(line.StartsWith("G01"))
                {
                    // Parse each G-code command
                    string[] parts = line.Split(' ');
                
                    if (parts.Length > 1)
                    {
                        // Extract command and values
                        string gCommand = parts[1].ToUpper(); // Assumes command is at index 1

                        // Add the new point to the spline
                        var newPoint = new Vector3(
                            ParseCoordinate(parts.First(part => part.StartsWith('X')), 0),
                            ParseCoordinate(parts.First(part => part.StartsWith('Y')), 0),
                            ParseCoordinate(parts.First(part => part.StartsWith('Z')), 0)
                        );

                        commands.Add(new StatelessCommand {
                            From = new Vector3(currentPoint.X, currentPoint.Y, currentPoint.Z),
                            To = newPoint,
                            Code = line
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
