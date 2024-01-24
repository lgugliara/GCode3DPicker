using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCode3D.Models.Interfaces
{
    public interface IGCRunnable
    {
        abstract public bool IsRunning { get; set; }
        public void Start();
        public void Stop();
    }
}
