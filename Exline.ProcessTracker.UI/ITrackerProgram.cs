using System;
using System.Collections.Generic;
using System.Text;

namespace Exline.ProcessTracker.UI
{

    public interface ITrackerProgram
    {
        ITracker Tracker { get; }
        void ExecuteCommand(params string[] args);

    }
}
