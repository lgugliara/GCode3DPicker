using System.Diagnostics;
using GCode3D.Models.Interfaces;
using HelixToolkit.SharpDX.Core;
using SharpDX;

namespace GCode3D.Models
{
    public class GCProgram : IGCRunnable
    {
        public List<StatelessCommand> Commands { get; set; } = [];
        public StatelessCommand CurrentCommand { get; set; } = new();
        public int CurrentIndex { get; set; } = 0;
        public Stopwatch Stopwatch { get; set; } = new();
        public Vector3 CurrentPosition
        {
            get {
                var from = CurrentCommand.From;
                var to = CurrentCommand.To;
                Vector3.Lerp(ref from, ref to, (float)Stopwatch.Elapsed.TotalSeconds, out Vector3 position);
                return position;
            }
        }

        public string Progress
        {
            get 
            {
                var factor = Commands.Count > 0 ?
                    CurrentIndex * 100f / Commands.Count :
                    float.NaN;
                return factor.ToString("n2") ?? "-";
            }
        }
        public string Description
        {
            get => string.Join("\t", new List<string> {
                $"[{CurrentIndex}/{Commands.Count - 1}] {Progress}%",
                $"{CurrentCommand.Code}",
            });
        }
        
        public LineBuilder ToLineBuilder()
        {
            var g = new LineBuilder();
            Commands.ForEach(c => g.AddLine(c.From, c.To));
            return g;
        }

        #region IGCRunnable
        public bool IsRunning
        {
            get => CurrentCommand.IsRunning;    
        }
        public void Start(Delegate callback)
        {
            Task.Run(() => 
            {
                bool hasStopped = Commands
                    .Select((command, i) => new { command, i })
                    .Any(v => {
                        CurrentCommand = v.command;
                        CurrentIndex = v.i;
                        Stopwatch.Restart();

                        v.command.IsRunning = true;
                        callback.DynamicInvoke(v.command);
                        Task.Delay(1000).Wait();

                        Stopwatch.Stop();
                        if(!IsRunning)
                            return true;

                        v.command.IsRunning = false;
                        v.command.IsCompleted = true;
                        return false;
                    });
            });
        }
        public void Stop() => CurrentCommand.IsRunning = false;
        #endregion
    }
}
