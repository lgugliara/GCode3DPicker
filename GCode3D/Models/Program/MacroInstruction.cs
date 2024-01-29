using System.Globalization;
using SharpDX;

namespace GCode3D.Models.Program
{
    public class MacroInstruction : Instruction
    {
        public override object OriginalValue
        {
            get => base.OriginalValue;
            set
            {
                base.OriginalValue = value;

                // TODO: Parse value into Commands
                string[] parts = value.ToString()?.Split(' ') ?? [];
                var origin = new Vector3(
                    Parser.ParseCoordinate(parts.First(part => part.StartsWith('X')), 0),
                    Parser.ParseCoordinate(parts.First(part => part.StartsWith('Y')), 0),
                    Parser.ParseCoordinate(parts.First(part => part.StartsWith('Z')), 0)
                );
                var radius = Parser.ParseCoordinate(parts.First(part => part.StartsWith('R')), 0);

                Commands = Enumerable.Range(0, 20).Select(x => 
                {
                    var X = origin.X + radius * MathF.Cos(x * MathF.PI / 10);
                    var Y = origin.Y + radius * MathF.Sin(x * MathF.PI / 10);
                    var Z = origin.Z;

                    var nextX = origin.X + radius * MathF.Cos((x + 1) * MathF.PI / 10);
                    var nextY = origin.Y + radius * MathF.Sin((x + 1) * MathF.PI / 10);
                    var nextZ = origin.Z;

                    return new DiscreteInstruction()
                    {
                        OriginalValue = $"G01 X{X.ToString(CultureInfo.GetCultureInfo("en-US"))} Y{Y.ToString(CultureInfo.GetCultureInfo("en-US"))} Z{Z.ToString(CultureInfo.GetCultureInfo("en-US"))}",
                        From = new Vector3(X, Y, Z),
                        To = new Vector3(nextX, nextY, nextZ),
                        Name = "G01",
                    };
                }).ToList();
            }
        }

        private List<DiscreteInstruction> _Commands = [];
        public List<DiscreteInstruction> Commands
        {
            get => _Commands;
            set => Set(ref _Commands, value);
        }
    }
}
