using System;
using System.Collections.Generic;
using System.Text;

namespace Exline.ProcessTracker
{
    public interface ITracker
    {
        event TrackingDelegate OnTracking;
        event TrackeTrackDeletage OnTrack;
        event TrackePauseDelegate OnPause;
        event TrackeStopDelegate OnStop;

        IPrey Prey { get; }

        void Track();
        void Stop();
        void Pause();

    }
}
