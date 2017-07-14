using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Exline.ProcessTracker
{
    public class ProcessPrey : IPrey
    {
        internal Process Process { get; private set; }

        public bool IsTrack
        {
            get
            {

                return Process != null;
            }
        }

        public ProcessPrey(int processId)
        {
            Process = Process.GetProcessById(processId);
        }
        public ProcessPrey(string processName)
        {
            Process[] process = Process.GetProcessesByName(processName);
            if (process != null && process.Length > 0)
            {
                Process = process[0];
            }
        }

        public string GetProcessName()
        {
            return Process.ProcessName;
        }
        public string GetMachineName()
        {
            return Process.MachineName;
        }
        public TimeSpan GetTotalStartTime()
        {
            return (DateTime.Now - Process.StartTime);
        }
        public int GetUseCpu()
        {
            return 0;
        }

        public long GetUsePhysicalMemory()
        {
            return Process.WorkingSet64;
        }
    }
}
