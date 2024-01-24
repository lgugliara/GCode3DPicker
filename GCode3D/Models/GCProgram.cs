using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCode3D.Models.Interfaces;
using HelixToolkit.SharpDX.Core;

namespace GCode3D.Models
{
    public class GCProgram : IGCRunnable, IDisposable
    {
        public List<StatelessCommand> Commands { get; set; } = [];
        
        public LineBuilder ToLineBuilder()
        {
            var g = new LineBuilder();
            Commands.ForEach(c => g.AddLine(c.From, c.To));
            return g;
        }

        #region IGCRunnable
        public bool IsRunning { get; set; }
        public void Start()
        {
            Task.Run(() => 
            {
                bool hasStopped = Commands.Any(command => {
                    if(!IsRunning)
                        return true;

                    command.IsRunning = true;
                    Task.Delay(1000).Wait();
                    command.IsRunning = false;
                    command.IsCompleted = true;
                    return false;
                });
            });
        }
        public void Stop()
        {
            IsRunning = false;
        }
        #endregion
        #region IDisposable
        public void Dispose()
        {
            IsRunning = false;
        }
        #endregion
    }
}
