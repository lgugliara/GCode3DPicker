using GCode3D.Models;
using System.Globalization;
using SharpDX;
using System.IO;
using System.Windows;

namespace GCode3D
{
    public class GCodeParser
    {
        public static GCProgram ParseFile(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            List<StatelessCommand> splinePoints = [];
            var line = sr.ReadLine();
            Vector3 currentPoint = new Vector3(0, 0, 0);

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

                        splinePoints.Add(new StatelessCommand {
                            From = new Vector3(currentPoint.X, currentPoint.Y, currentPoint.Z),
                            To = newPoint
                        });
                        currentPoint += newPoint;
                    }
                }

                line = sr.ReadLine();
            }

            return new GCProgram { Commands = splinePoints };
        }

        private static float ParseCoordinate(string value, float defaultValue)
        {
            if (float.TryParse(value.Substring(2), CultureInfo.InvariantCulture, out float result))
                return result;
            return defaultValue;
        }

    }
}
