using System.Globalization;
using SharpDX;
using System.IO;

namespace GCode3D.Models.Program
{
    public static class Parser
    {
        // TODO: Scritta così fa cagare
        public static List<Instruction> From(FileInfo? from = null)
        {
            // TODO: Handle with exceptions (ArgumentNullException)
            if(from == null)
                return [];

            using StreamReader sr = from.OpenText();
            List<Instruction> commands = [];
            
            Instruction previousInstruction = new();
            string? line;

            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(' ');

                if(parts.Length < 1)
                    continue;
                
                Vector3 to = new();
                Instruction? instruction = null;
                string commandName = parts[0].ToUpper();

                if (parts[0].Equals("G01"))
                    instruction = new DiscreteInstruction()
                    {
                        OriginalValue = line,
                        From = previousInstruction.To,
                        To = new Vector3(
                            ParseCoordinate(parts.First(part => part.StartsWith('X')), 0),
                            ParseCoordinate(parts.First(part => part.StartsWith('Y')), 0),
                            ParseCoordinate(parts.First(part => part.StartsWith('Z')), 0)
                        ),
                        Name = commandName,
                    };

                else if(parts[0].StartsWith("HOLE"))
                    instruction = new MacroInstruction()
                    {
                        OriginalValue = line,
                        From = previousInstruction.To,
                        To = previousInstruction.To,
                        Name = commandName,
                    };
                  
                if(instruction == null)
                    continue;

                commands.Add(instruction);
                previousInstruction = instruction;
            }

            return commands;
        }

        public static float ParseCoordinate(string value, float defaultValue) =>
            float.TryParse(value[1..], CultureInfo.InvariantCulture, out float result) ?
                result : defaultValue;
    }
}
