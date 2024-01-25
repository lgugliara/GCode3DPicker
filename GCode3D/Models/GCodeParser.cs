using GCode3D.Models;
using System.Globalization;
using SharpDX;
using System.IO;

namespace GCode3D
{
    public class GCodeParser
    {
        public static GCProgram ParseFile(ExplorerElement file)
        {
            StreamReader sr = new(file.Path);
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
                            ParseCoordinate(parts.First(part => part.StartsWith("X")), currentPoint.X),
                            ParseCoordinate(parts.First(part => part.StartsWith("Y")), currentPoint.Y),
                            ParseCoordinate(parts.First(part => part.StartsWith("Z")), currentPoint.Z)
                        );

                        commands.Add(new StatelessCommand {
                            From = new Vector3(currentPoint.X, currentPoint.Y, currentPoint.Z),
                            To = newPoint,
                            Code = line
                        });
                        currentPoint += newPoint;
                    }
                }

                line = sr.ReadLine();
            }

            return new GCProgram { Commands = commands };
        }

        private static float ParseCoordinate(string value, float defaultValue)
        {
            if (float.TryParse(value.Substring(2), CultureInfo.InvariantCulture, out float result))
                return result;
            return defaultValue;
        }

    }
}
