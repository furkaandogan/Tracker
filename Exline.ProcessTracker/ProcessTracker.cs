using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Exline.ProcessTracker
{
    public class ProcessTracker : ITracker
    {
        private ProcessPrey _prey;
        private Thread _trackerThread;
        private bool isStop;

        public ProcessTracker():base()
        {

        }
        public ProcessTracker(int processId) : this(new ProcessPrey(processId))
        {
        }

        public ProcessTracker(string processName) : this(new ProcessPrey(processName))
        {
        }

        public ProcessTracker(ProcessPrey prey) : this()
        {
            _prey = prey;
            Init();
        }

        public void Init()
        {

            isStop = false;
            _trackerThread = new Thread((x) =>
            {
                OnTrackerThread((ProcessTracker)x);
            });
            _trackerThread.IsBackground = true;
        }

        public IPrey Prey
        {
            get
            {
                return _prey;
            }
        }

        public event TrackingDelegate OnTracking;
        public event TrackeTrackDeletage OnTrack;
        public event TrackePauseDelegate OnPause;
        public event TrackeStopDelegate OnStop;

        public void Pause()
        {
            isStop = true;
            if (OnPause != null)
            {
                OnPause(this, new EventArgs());
            }
        }
        public void Stop()
        {
            if (OnStop != null)
            {
                OnStop(this, new EventArgs());
            }
        }
        public void Track()
        {
            _trackerThread.Start(this);
            if (OnTrack != null)
            {
                OnTrack(this, new EventArgs());
            }
        }

        public void SetProcess(int processId)
        {
            SetProcess(new ProcessPrey(processId));
        }
        public void SetProcess(string processName)
        {
            SetProcess(new ProcessPrey(processName));
        }
        public void SetProcess(ProcessPrey prey)
        {
            _prey = prey;
            Init();
        }

        private void OnTrackerThread(ProcessTracker tracker)
        {
            if (tracker != null)
            {
                if (tracker.Prey != null && tracker.Prey.IsTrack)
                {
                    ProcessPrey processPrey = (ProcessPrey)tracker.Prey;
                    if (processPrey != null)
                    {
                        while (tracker.isStop == false)
                        {
                            if (tracker.OnTracking != null)
                            {
                                processPrey.Process.Refresh();
                                tracker.OnTracking(tracker.Prey, tracker, new EventArgs());
                            }
                        }
                    }
                }
            }
        }
    }
}
