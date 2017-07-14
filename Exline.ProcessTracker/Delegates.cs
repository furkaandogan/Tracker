using System;
using System.Collections.Generic;
using System.Text;

namespace Exline.ProcessTracker
{
    public delegate void TrackingDelegate(IPrey prey, ITracker tracker, EventArgs e);
    public delegate void TrackeStopDelegate(ITracker tracker, EventArgs e);
    public delegate void TrackePauseDelegate(ITracker tracker, EventArgs e);
    public delegate void TrackeTrackDeletage(ITracker tracket, EventArgs e);
}
