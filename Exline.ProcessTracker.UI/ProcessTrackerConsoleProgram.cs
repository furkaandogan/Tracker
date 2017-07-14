using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

namespace Exline.ProcessTracker.UI
{
    public class ProcessTrackerConsoleProgram : BaseTrackerProgram, ITrackerProgram
    {
        private List<Command> _commands;
        private ProcessTracker _tracker;
        public override List<Command> Commands
        {
            get
            {
                return _commands;
            }
        }

        public override Command DefaultCommand { get; set; }

        public ITracker Tracker
        {
            get
            {
                return _tracker;
            }
        }

        public ProcessTrackerConsoleProgram()
        {
            _tracker = new ProcessTracker();
            _tracker.OnTracking += _tracker_OnTracking;

            DefaultCommand = new Command((args) =>
            {
                Console.WriteLine("-pid     : process id ile process'i takip etmenizi sağlar ");
                Console.WriteLine("           örnek -pid {process id}");
                Console.WriteLine("-pname   : process name ile eşleşen ilk process'i takip etmenizi sağlar");
                Console.WriteLine("           örnek -pname {process name}");
            }, "help", "?");
            _commands = new List<Command>()
            {
                DefaultCommand,
                new Command((args)=>{
                    int cmdIndex=args.ToList().IndexOf("-pid");
                    if (cmdIndex>-1 && args.Length>=cmdIndex+1)
                    {
                        int processId=-1;
                        int.TryParse(args[cmdIndex+1],out processId);
                        TrackByProcessId(processId);
                        Track();
                    }
                    else
                    {

                    }
                },"-pid"),
                new Command((args)=>{
                    int cmdIndex=args.ToList().IndexOf("-pname");
                    if (cmdIndex>-1 && args.Length>=cmdIndex+1)
                    {
                        TrackByProcessName(args[cmdIndex+1]);
                        Track();
                    }
                },"-pname"),

            };
        }

        private void _tracker_OnTracking(IPrey prey, ITracker tracker, EventArgs e)
        {
            ProcessPrey processPrey = prey as ProcessPrey;
            if (processPrey != null)
            {
                Console.WriteLine(string.Format("process name: {0}", processPrey.GetProcessName()));
                Console.WriteLine(string.Format("machine name: {0}", processPrey.GetMachineName()));
                Console.WriteLine(string.Format("total start time(min): {0}", processPrey.GetTotalStartTime().TotalMinutes));
                Console.WriteLine(string.Format("total cpu usage: {0}%", processPrey.GetUseCpu()));
                Console.WriteLine(string.Format("total physical memory usage: {0}", processPrey.GetUsePhysicalMemory()));
                Console.WriteLine("cikis icin bir tusa basiniz.");
            }
            Thread.Sleep(800);
            Console.Clear();
        }

        public void ExecuteCommand(params string[] args)
        {
            Command cmd = FilterCommand(args);
            if (cmd != null)
            {
                cmd.Execute(args);
            }
        }

        private void Track()
        {
            if (_tracker!=null)
            {
                _tracker.Track();
            }
        }

        private void TrackByProcessId(int processId)
        {
            if (processId > 0)
            {
                _tracker.SetProcess(processId);
            }
        }
        private void TrackByProcessName(string processName)
        {
            if (!string.IsNullOrEmpty(processName))
            {
                _tracker.SetProcess(processName);
            }
        }
    }
}
