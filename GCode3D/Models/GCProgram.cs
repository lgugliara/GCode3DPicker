using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCode3D.Models.Interfaces;
using HelixToolkit.SharpDX.Core;

namespace GCode3D.Models
{
    public class GCProgram : IGCRunnable
    {
        public List<StatelessCommand> Commands { get; set; } = [];
        
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

                        v.command.IsRunning = true;
                        callback.DynamicInvoke(v.command);
                        Task.Delay(1000).Wait();

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
